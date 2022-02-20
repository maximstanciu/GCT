using FluentValidation;
using GCT.Contracts.Data;
using GCT.Contracts.Data.Entities;
using GCT.Contracts.DTO;
using GCT.Core.Exceptions;
using GCT.Core.StateMachine;
using MediatR;

namespace GCT.Core.Handlers.Commands
{
    public class DepositAccountCommand : IRequest<int>
    {
        public DepositAccountDTO Model { get; }
        public DepositAccountCommand(DepositAccountDTO model)
        {
            this.Model = model;
        }
    }

    public class DepositAccountCommandHandler : IRequestHandler<DepositAccountCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<DepositAccountDTO> _validator;
        private readonly IProcessSM _process;

        public DepositAccountCommandHandler(IUnitOfWork repository, IValidator<DepositAccountDTO> validator, IProcessSM process)
        {
            _repository = repository;
            _validator = validator;
            _process = process;
        }

        public async Task<int> Handle(DepositAccountCommand request, CancellationToken cancellationToken)
        {
            DepositAccountDTO model = request.Model;
            IEnumerable<string> errors;

            //Fluent Validation
            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                errors = result.Errors.Select(x => x.ErrorMessage);
                throw new InvalidRequestBodyException
                {
                    Errors = errors.ToArray()
                };
            }

            var accountFrom = _repository.Accounts.Get(model.AccountFrom);
            var accountTo = _repository.Accounts.Get(model.AccountTo);

            //TODO: This can be moved under Fluent Validator 

            //Check Available Balance of Sender & Recipient
            if (accountFrom == null)
            {
                errors = new List<string>() { $"No Account found Id {model.AccountFrom}" };
                throw new InvalidRequestBodyException { Errors = errors.ToArray() };
            }

            if (accountFrom.Balance < model.Amount)
            {
                errors = new List<string>() { $"Not Enought Balance on Account Id {model.AccountFrom}" };
                throw new InvalidRequestBodyException { Errors = errors.ToArray() };
            }

            if (accountTo == null)
            {
                errors = new List<string>() { $"No Account found Id {model.AccountTo}" };
                throw new InvalidRequestBodyException { Errors = errors.ToArray() };
            }

            //Begin BASIC SAGA Transaction          

            //Start FSM
            _process.MoveNext(Enums.Command.Begin);

            // Update Sender Balance
            accountFrom.Balance -= model.Amount;
            _repository.Accounts.Update(accountFrom);

            //Get Accounts Owners
            var userSender = _repository.Users.Get(accountFrom.UserId);

            //Write to the Ledger
            var entityFrom = new Transaction
            {
                AccountId = model.AccountFrom,
                Amount = model.Amount,
                Type = "CR",
                UserId = userSender.Id
            };

            // Sender - Write to the Ledger (Dual Entry)
            _repository.Transactions.Add(entityFrom);
            

            // Update Recipient Balance
            accountTo.Balance += model.Amount;
            _repository.Accounts.Update(accountTo);

            //Get Accounts Owners
            var userRecipient = _repository.Users.Get(accountTo.UserId);

            //Write to the Ledger
            var entityTo = new Transaction
            {
                AccountId = model.AccountTo,
                Amount = model.Amount,
                Type = "DR",
                UserId = userRecipient.Id
            };

            //Recipient - Write to the Ledger (Dual Entry)
            _repository.Transactions.Add(entityTo);

            //Commit Changes
            await _repository.CommitAsync();

            _process.MoveNext(Enums.Command.End);

            return entityTo.Id;
        }

    }
}
