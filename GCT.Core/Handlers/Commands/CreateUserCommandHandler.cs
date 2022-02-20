using FluentValidation;
using GCT.Contracts.Data;
using GCT.Contracts.Data.Entities;
using GCT.Contracts.DTO;
using GCT.Core.Exceptions;
using MediatR;

namespace GCT.Core.Handlers.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public CreateUserDTO Model { get; }
        public CreateUserCommand(CreateUserDTO model)
        {
            this.Model = model;
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateUserDTO> _validator;

        public CreateUserCommandHandler(IUnitOfWork repository, IValidator<CreateUserDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            CreateUserDTO model = request.Model;
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

            var entity = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                IdCard = model.IdCard
            };

            _repository.Users.Add(entity);
            await _repository.CommitAsync();

            return entity.Id;
        }
    }
}
