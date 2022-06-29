using Chibbis.TestProject.CartService.Application.Entities;
using Chibbis.TestProject.Contracts.Common;

namespace Chibbis.TestProject.CartService.Application.Consumers.Webhook.Commands.SetWebhook
{
    public record SetWebhookCommand
    {
        public string WebhookUrl { get; init; }
        public WebhookType WebhookType { get; init; }
    }

    public record SetWebhookCommandResponse : CommandResponse
    {
        
    }
}