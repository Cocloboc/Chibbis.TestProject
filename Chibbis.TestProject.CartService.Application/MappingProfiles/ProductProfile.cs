using AutoMapper;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Commands.AddProducts;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Common;
using Chibbis.TestProject.CartService.Application.Consumers.Webhook.Commands.SetWebhook;
using Chibbis.TestProject.CartService.Application.Entities;
using Chibbis.TestProject.Contracts.Product.Common;

namespace Chibbis.TestProject.CartService.Application.MappingProfiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductMinified, CartItemMinified>()
                .ForMember(x => x.ProductId,
                    opt => opt.MapFrom(o => o.Id));
            CreateMap<ProductDto, Product>();
        }
    }
}