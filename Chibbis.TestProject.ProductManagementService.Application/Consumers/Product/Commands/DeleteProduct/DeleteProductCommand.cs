using Chibbis.TestProject.Contracts.Common;

namespace Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.RemoveProduct
{
    public record DeleteProductCommand
    {
        public int Id { get; set; }
    }

    public record DeleteProductCommandResponse : CommandResponse
    {
        
    }
}