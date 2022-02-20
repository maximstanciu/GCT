using FluentValidation;
using GCT.Contracts.Data;
using GCT.Contracts.Data.Entities;
using GCT.Contracts.DTO;
using GCT.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCT.Core.Handlers.Commands
{
    public class WithrawalAccountCommand : IRequest<int>
    {
        public WithdrawalAccountDTO Model { get; }
        public WithrawalAccountCommand(WithdrawalAccountDTO model)
        {
            this.Model = model;
        }
    }

    public class WithrawalAccountCommandHandler : IRequestHandler<WithrawalAccountCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<WithdrawalAccountDTO> _validator;

        public WithrawalAccountCommandHandler(IUnitOfWork repository, IValidator<WithdrawalAccountDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(WithrawalAccountCommand request, CancellationToken cancellationToken)
        {
            WithdrawalAccountDTO model = request.Model;

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

            //Check Available Balance of Recipient and Update Balance
            var account = _repository.Accounts.Get(model.AccountFrom);

            if (account == null)
                throw new EntityNotFoundException($"No Account found Id {model.AccountFrom}");

            if(account.Balance < model.Amount)
                throw new EntityNotFoundException($"Not enought Balance on AccountId {model.AccountFrom}");

            account.Balance -= model.Amount;
            _repository.Accounts.Update(account);

            //Get Account Owner
            var user = _repository.Users.Get(account.UserId);

            //Write to the Ledger
            var entity = new Transaction
            {
                AccountId = model.AccountFrom,
                Amount = model.Amount,
                Type = "CR",
                UserId = user.Id
            };
            _repository.Transactions.Add(entity);

            //Commit Changes
            await _repository.CommitAsync();

            return entity.Id;
        }

    }
}
