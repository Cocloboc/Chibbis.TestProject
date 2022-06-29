using System;
using System.Threading.Tasks;
using AutoMapper;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Commands.AddProducts;
using Chibbis.TestProject.CartService.Application.Consumers.Webhook.Commands.SetWebhook;
using Chibbis.TestProject.CartService.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Chibbis.TestProject.CartService.Application.Consumers.Webhook.Commands.DeleteWebhook
{
    public class DeleteWebhookCommandConsumer: IConsumer<DeleteWebhookCommand>
    {
        private readonly ILogger<DeleteWebhookCommandConsumer> _logger;
        private readonly IWebhookService _webhookService;
        private readonly IUserContext _userContext;

        public DeleteWebhookCommandConsumer(
            ILogger<DeleteWebhookCommandConsumer> logger,
            IWebhookService webhookService,
            IUserContext userContext)
        {
            _logger = logger;
            _webhookService = webhookService;
            _userContext = userContext;
        }

        public async Task Consume(ConsumeContext<DeleteWebhookCommand> context)
        {
            try
            {
                var userUUID = _userContext.UserUUID;
                
                await _webhookService.DeleteWebhookAsync(userUUID);
            }
            catch (Exception e)
            {
                _logger.LogError($"We have some errors:{e.Message}", e);
                throw;
            }

            await context.RespondAsync(new DeleteWebhookCommandResponse());
        }
    }
}