using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chibbis.TestProject.ProductManagementService.Application.Common;
using Chibbis.TestProject.ProductManagementService.Application.Entities;
using Chibbis.TestProject.ProductManagementService.Application.Exceptions;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Chibbis.TestProject.ProductManagementService.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly string _prefix = "product";
        private readonly IRedisClient _redisClient;

        public ProductService(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        public async Task CreateProductAsync(Product product)
        {
            var oldProduct = await GetProductAsync(product.Id);

            if (oldProduct != null)
            {
                throw new HasDuplicateExceptions(Errors.ProductHasDuplicate(product.Id));
            }

            await _redisClient.GetDefaultDatabase().AddAsync(GetKey(product.Id), product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            var oldProduct = await GetProductAsync(product.Id);

            if (oldProduct == null)
            {
                throw new NotFoundException(Errors.ProductNotFound(product.Id));
            }
            
            await _redisClient.GetDefaultDatabase().AddAsync(GetKey(product.Id), product);
        }

        public async Task DeleteProductAsync(int productId)
        {
            await _redisClient.GetDefaultDatabase()
                .RemoveAsync(GetKey(productId));
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _redisClient.GetDefaultDatabase().GetAsync<Product>(GetKey(id));
        }

        public async Task<List<Product>> GetProductsAsync(List<int> ids)
        {
            var products = await _redisClient.GetDefaultDatabase()
                .GetAllAsync<Product>(GetKeys(ids).ToArray());

            return products.Select(x => x.Value)
                .Where(x => x != default).ToList();
        }

        public async Task<List<int>> ValidateProducts(List<int> ids)
        {
            var invalidIds = new List<int>();

            foreach (var id in ids)
            {
                var productExists = await _redisClient
                    .GetDefaultDatabase()
                    .ExistsAsync(GetKey(id));

                if (!productExists)
                {
                    invalidIds.Add(id);
                }
            }

            return invalidIds;
        }

        private string GetKey(int id)
        {
            return GetKeys(new List<int> {id}).FirstOrDefault();
        }

        private List<string> GetKeys(IEnumerable<int> ids)
        {
            return ids.Select(id => $"{_prefix}:{id}").ToList();
        }
    }
}