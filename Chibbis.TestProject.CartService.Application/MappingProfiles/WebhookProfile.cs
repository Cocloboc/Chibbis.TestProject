using AutoMapper;
using Chibbis.TestProject.CartService.Application.Consumers.Webhook.Commands.SetWebhook;
using Chibbis.TestProject.CartService.Application.Entities;

namespace Chibbis.TestProject.CartService.Application.MappingProfiles
{
    public class WebhookProfile : Profile
    {
        public WebhookProfile()
        {
            CreateMap<SetWebhookCommand, Webhook>()
                .ForMember(x => x.Url,
                    opt => opt.MapFrom(o => o.WebhookUrl));
        }
    }
}