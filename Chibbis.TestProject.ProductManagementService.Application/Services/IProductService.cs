using System.Collections.Generic;
using System.Threading.Tasks;
using Chibbis.TestProject.ProductManagementService.Application.Entities;

namespace Chibbis.TestProject.ProductManagementService.Application.Services
{
    public interface IProductService
    {
        Task<int> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task<Product> GetProductAsync(int id);
        Task<List<Product>> GetProductsAsync(List<int> ids);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>List of invalid products</returns>
        Task<List<int>> ValidateProducts(List<int> ids);
    }
}