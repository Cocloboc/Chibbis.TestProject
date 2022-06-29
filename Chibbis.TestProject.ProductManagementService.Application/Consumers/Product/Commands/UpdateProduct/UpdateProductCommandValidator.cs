using FluentValidation;

namespace Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Cost)
                .GreaterThan(0);
        }
    }
}