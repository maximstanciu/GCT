using FluentValidation;
using GCT.Contracts.Data;
using GCT.Contracts.Data.Entities;
using GCT.Contracts.DTO;
using GCT.Core.Exceptions;
using MediatR;

namespace GCT.Core.Handlers.Commands
{
    public class CreateAccountCommand : IRequest<int>
    {
        public CreateAccountDTO Model { get; }
        public CreateAccountCommand(CreateAccountDTO model)
        {
            this.Model = model;
        }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateAccountDTO> _validator;

        public CreateAccountCommandHandler(IUnitOfWork repository, IValidator<CreateAccountDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            CreateAccountDTO model = request.Model;

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

            var err = new List<string>();

            //Check if User Exists
            var user = _repository.Users.Get(model.UserId);

            if (user == null)
            {
                err.Add($"No User found Id {model.UserId}");
                throw new InvalidRequestBodyException { Errors = err.ToArray() };
            }

            //Create a new Account
            var entity = new Account
            {
                Name = model.Name,
                UserId= model.UserId
            };

            _repository.Accounts.Add(entity);
            await _repository.CommitAsync();

            return entity.Id;
        }
    }
}
