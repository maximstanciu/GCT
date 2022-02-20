using FluentValidation;
using GCT.Contracts.DTO;

namespace GCT.Core.Validators
{
    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
            RuleFor(x => x.IdCard).NotEmpty().WithMessage("ID Card is required");

        }
    }
}
