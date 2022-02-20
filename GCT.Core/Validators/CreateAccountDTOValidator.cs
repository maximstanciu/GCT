using FluentValidation;
using GCT.Contracts.DTO;

namespace GCT.Core.Validators
{
    public class CreateAccountDTOValidator : AbstractValidator<CreateAccountDTO>
    {
        public CreateAccountDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User is required");
        }
    }
}
