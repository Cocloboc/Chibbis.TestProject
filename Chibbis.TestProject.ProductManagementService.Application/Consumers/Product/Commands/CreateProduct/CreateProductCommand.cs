using Chibbis.TestProject.Contracts.Common;

namespace Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.CreateProduct
{
    public record CreateProductCommand
    {
        public string Name { get; init; }
        public decimal Cost { get; init; }
        public bool ForBonusPoints { get; init; }
    }

    public record CreateProductCommandResponse : CommandResponseLoaded<ProductMinified>
    {
        
    }
}