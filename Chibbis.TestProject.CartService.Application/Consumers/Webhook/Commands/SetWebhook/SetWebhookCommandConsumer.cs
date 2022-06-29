using System;
using System.Threading.Tasks;
using AutoMapper;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Commands.AddProducts;
using Chibbis.TestProject.CartService.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Chibbis.TestProject.CartService.Application.Consumers.Webhook.Commands.SetWebhook
{
    public class SetWebhookCommandConsumer: IConsumer<SetWebhookCommand>
    {
        private readonly ILogger<SetWebhookCommandConsumer> _logger;
        private readonly IWebhookService _webhookService;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public SetWebhookCommandConsumer(
            ILogger<SetWebhookCommandConsumer> logger,
            IWebhookService webhookService,
            IUserContext userContext,
            IMapper mapper)
        {
            _logger = logger;
            _webhookService = webhookService;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<SetWebhookCommand> context)
        {
            var command = context.Message;
            try
            {
                var userUUID = _userContext.UserUUID;
                var webhook = _mapper.Map<Entities.Webhook>(command);
                
                await _webhookService.SetWebhookAsync(userUUID, webhook);
            }
            catch (Exception e)
            {
                _logger.LogError($"We have some errors:{e.Message}", e);
                throw;
            }

            await context.RespondAsync(new SetWebhookCommandResponse());
        }
    }
}