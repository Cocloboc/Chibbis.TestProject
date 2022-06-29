namespace Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.Common
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public bool ForBonusPoints { get; set; }
    }
}