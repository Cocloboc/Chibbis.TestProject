using FluentValidation;

namespace Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.CreateProduct
{
    public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Cost)
                .GreaterThan(0);
        }
    }
}