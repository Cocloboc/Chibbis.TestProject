namespace Chibbis.TestProject.CartService.Application.Entities
{
    public class Webhook
    {
        public string Url { get; set; }
        public WebhookType WebhookType { get; set; }
    }

    public enum WebhookType
    {
        Get,
        Post
    }
}