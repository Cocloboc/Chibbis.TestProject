using Chibbis.TestProject.CartService.Application.Consumers.Cart.Common.Validation;
using FluentValidation;

namespace Chibbis.TestProject.CartService.Application.Consumers.Cart.Commands.AddProducts
{
    public class AddProductCommandValidator: AbstractValidator<AddProductsCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(x => x.Products)
                .NotEmpty();
            RuleForEach(x => x.Products)
                .SetValidator(new ProductMinifiedDtoValidator());
        }
    }
}