using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application.Services;
using MassTransit;
using Microsoft.AspNetCore.Http;

namespace Chibbis.TestProject.CartService.Web.Filters
{
    public class AuthFilter<T> :
        IFilter<PublishContext<T>>,
        IFilter<SendContext<T>>,
        IFilter<ConsumeContext<T>>
        where T : class
    {
        private readonly IUserContext _userContext;
        private readonly HttpContext _httpContext;

        public AuthFilter(
            IUserContext userContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _userContext = userContext;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
        {
            AddAuthPayload(context);

            return next.Send(context);
        }

        public Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
        {
            AddAuthPayload(context);

            return next.Send(context);
        }

        public Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            GetAuthPayload(context);

            return next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
        }

        private void AddAuthPayload(PipeContext context)
        {
            
            if (_httpContext.Request.Headers.TryGetValue("userUUID", out var userUUID))
            {
                context.GetOrAddPayload<string>(() =>userUUID);
            }
        }

        private void GetAuthPayload(PipeContext context)
        {
            var userUuid = context.GetOrAddPayload(() => _userContext.UserUUID);
            if (userUuid != null)
            {
               _userContext.SetUserUUID(userUuid);
            }
        }
    }
}