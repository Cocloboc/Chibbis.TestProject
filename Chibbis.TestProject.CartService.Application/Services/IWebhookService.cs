using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application.Entities;

namespace Chibbis.TestProject.CartService.Application.Services
{
    public interface IWebhookService
    {
        Task<Webhook> GetWebhookAsync(string userUUID);
        Task SetWebhookAsync(string userUUID, Webhook webhook);
        Task DeleteWebhookAsync(string userUUID);
        public Task CallWebhookAsync(string userUUID);
    }
}