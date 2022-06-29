using Chibbis.TestProject.CartService.Application.Consumers.Cart.Common.Validation;
using FluentValidation;

namespace Chibbis.TestProject.CartService.Application.Consumers.Cart.Commands.RemoveProducts
{
    public class RemoveProductsCommandValidator: AbstractValidator<RemoveProductsCommand>
    {
        public RemoveProductsCommandValidator()
        {
            RuleFor(x => x.Products)
                .NotEmpty();
            RuleForEach(x => x.Products)
                .SetValidator(new ProductMinifiedDtoValidator());
        }
    }
}