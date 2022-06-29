using AutoMapper;
using Chibbis.TestProject.Contracts.Product.Common;
using Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.CreateProduct;
using Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.UpdateProduct;
using Chibbis.TestProject.ProductManagementService.Application.Entities;

namespace Chibbis.TestProject.ProductManagementService.Application.MappingProfiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
        }
    }
}