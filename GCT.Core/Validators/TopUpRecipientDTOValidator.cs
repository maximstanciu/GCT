using FluentValidation;
using GCT.Contracts.DTO;

namespace GCT.Core.Validators
{
    public class TopUpRecipientDTOValidator : AbstractValidator<TopUpAccountDTO>
    {
        public TopUpRecipientDTOValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount is required");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount should be greater than 0");
            RuleFor(x => x.AccountTo).NotEmpty();
        }
    }
}
