using FluentValidation;

namespace Chibbis.TestProject.CartService.Application.Consumers.Cart.Common.Validation
{
    public class ProductMinifiedDtoValidator: AbstractValidator<ProductMinified>
    {
        public ProductMinifiedDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0);
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}