using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Commands.AddProducts;
using Chibbis.TestProject.CartService.Application.Consumers.Webhook.Commands.DeleteWebhook;
using Chibbis.TestProject.CartService.Application.Consumers.Webhook.Commands.SetWebhook;
using Chibbis.TestProject.CartService.Application.Services;
using Chibbis.TestProject.CartService.Web.Attributes;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Chibbis.TestProject.CartService.Web.Controllers
{
    [UserData]
    [ApiController]
    [Route("[controller]")]
    public class WebhookController: Controller
    {
        private readonly IMediator _mediator;

        public WebhookController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> SetWebhook(SetWebhookCommand command)
        {
            await _mediator
                .CreateRequestClient<SetWebhookCommand>()
                .GetResponse<SetWebhookCommandResponse>(command);

            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteWebhook([FromServices] IUserContext userContext)
        {
            var command = new DeleteWebhookCommand();
            
            await _mediator
                .CreateRequestClient<DeleteWebhookCommand>()
                .GetResponse<DeleteWebhookCommandResponse>(command);

            return Ok();
        }
    }
}