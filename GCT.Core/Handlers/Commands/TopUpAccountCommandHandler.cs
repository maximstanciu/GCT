using FluentValidation;
using GCT.Contracts.Data;
using GCT.Contracts.Data.Entities;
using GCT.Contracts.DTO;
using GCT.Core.Exceptions;
using MediatR;

namespace GCT.Core.Handlers.Commands
{
    public class TopUpRecipientCommand : IRequest<int>
    {
        public TopUpAccountDTO Model { get; }
        public TopUpRecipientCommand(TopUpAccountDTO model)
        {
            this.Model = model;
        }
    }

    public class TopUpAccountCommandHandler : IRequestHandler<TopUpRecipientCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<TopUpAccountDTO> _validator;

        public TopUpAccountCommandHandler(IUnitOfWork repository, IValidator<TopUpAccountDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(TopUpRecipientCommand request, CancellationToken cancellationToken)
        {
            TopUpAccountDTO model = request.Model;
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

            //Check Available Balance of Recipient and Update Balance
            var account = _repository.Accounts.Get(model.AccountTo);

            if (account == null)
            {
                errors = new List<string>() { $"No Account found Id {model.AccountTo}" };
                throw new InvalidRequestBodyException { Errors = errors.ToArray() };
            }

            account.Balance += model.Amount;
             _repository.Accounts.Update(account);

            //Get Account Owner
            var user = _repository.Users.Get(account.UserId);

            //Write to the Ledger
            var entity = new Transaction
            {
                AccountId = model.AccountTo,
                Amount = model.Amount,
                UserId = user.Id,
                Type = "DR"
            };
            _repository.Transactions.Add(entity);

            //Commit Changes
            await _repository.CommitAsync();

            return entity.Id;
        }

    }
}
