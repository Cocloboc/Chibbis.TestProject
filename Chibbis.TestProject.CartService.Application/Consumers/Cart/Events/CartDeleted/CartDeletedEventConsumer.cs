using System;
using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Chibbis.TestProject.CartService.Application.Consumers.Cart.Events.CartDeleted
{
    public class CartDeletedEventConsumer: IConsumer<CartDeletedEvent>
    {
        private readonly ILogger<CartDeletedEventConsumer> _logger;
        private readonly IWebhookService _webhookService;

        public CartDeletedEventConsumer(
            ILogger<CartDeletedEventConsumer> logger,
            IWebhookService webhookService)
        {
            _logger = logger;
            _webhookService = webhookService;
        }

        public async Task Consume(ConsumeContext<CartDeletedEvent> context)
        {
            var eventMsg = context.Message;

            try
            {
                await _webhookService.CallWebhookAsync(eventMsg.UserUUID);
            }
            catch (Exception e)
            {
                _logger.LogError($"We got some errors here:{e.Message}", e);
            }
        }
    }
}