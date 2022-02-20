using FluentValidation;
using GCT.Contracts.DTO;

namespace GCT.Core.Validators
{
    public class WithrawalAccountDTOValidator : AbstractValidator<WithdrawalAccountDTO>
    {
        public WithrawalAccountDTOValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount is required");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount should be greater than 0");
            RuleFor(x => x.AccountFrom).NotEmpty();
        }
       
    }
}
