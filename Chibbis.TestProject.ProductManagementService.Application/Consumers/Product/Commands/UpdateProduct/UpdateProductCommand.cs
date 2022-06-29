using Chibbis.TestProject.Contracts.Common;

namespace Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.UpdateProduct
{
    public class UpdateProductCommand
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public decimal Cost { get; init; }
        public bool ForBonusPoints { get; init; }
    }

    public record UpdateProductCommandResponse : CommandResponse
    {
    }
}