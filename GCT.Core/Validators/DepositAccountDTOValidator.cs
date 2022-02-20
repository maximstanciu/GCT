using FluentValidation;
using GCT.Contracts.DTO;

namespace GCT.Core.Validators
{
    public class DepositAccountDTOValidator : AbstractValidator<DepositAccountDTO>
    {
        public DepositAccountDTOValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount is required");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount should be greater than 0");
            RuleFor(x => x.AccountTo).NotEmpty();
            RuleFor(x => x.AccountFrom).NotEmpty();
        }
    }
}
