using FluentValidation;
using GCT.Contracts.Data;
using GCT.Contracts.Data.Entities;
using GCT.Contracts.DTO;
using GCT.Core.Exceptions;
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

        public DepositAccountCommandHandler(IUnitOfWork repository, IValidator<DepositAccountDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(DepositAccountCommand request, CancellationToken cancellationToken)
        {
            DepositAccountDTO model = request.Model;

            //Fluent Validation
            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var accountFrom = _repository.Accounts.Get(model.AccountFrom);
            var accountTo = _repository.Accounts.Get(model.AccountTo);          

            //Check Available Balance of Sender & Recipient
            if (accountFrom == null)
                throw new EntityNotFoundException($"No Account found Id {model.AccountFrom}");

            if (accountFrom.Balance < model.Amount)
                throw new EntityNotFoundException($"Not Enought Balance on Account Id {model.AccountFrom}");

            if (accountTo == null)
                throw new EntityNotFoundException($"No Account found Id {model.AccountTo}");

            //Begin BASIC SAGA Transaction          

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

            return entityTo.Id;
        }

    }
}
