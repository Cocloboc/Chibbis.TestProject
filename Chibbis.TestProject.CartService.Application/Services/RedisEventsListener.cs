using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Events.CartDeleted;
using MassTransit.Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;

namespace Chibbis.TestProject.CartService.Application.Services
{
    public class RedisEventsListener : IHostedService, IDisposable
    {
        private readonly string EXPIRED_KEYS_CHANNEL = "__keyevent@0__:expired";

        private readonly ConnectionMultiplexer _redis;
        private ISubscriber _expiredEventsSubscriber;
        private readonly ILogger<RedisEventsListener> _logger;
        private readonly IMediator _mediator;

        public RedisEventsListener(
            ILogger<RedisEventsListener> logger,
            IMediator mediator,
            IOptions<RedisConfiguration> redisConfiguration)
        {
            _logger = logger;
            _mediator = mediator;

            _redis = ConnectionMultiplexer.Connect(redisConfiguration.Value.ConnectionString);
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RedisEventsListener running.");

            _expiredEventsSubscriber = _redis.GetSubscriber();
            await _expiredEventsSubscriber.SubscribeAsync(EXPIRED_KEYS_CHANNEL, HandleExpiredEvent);
        }

        private async void HandleExpiredEvent(RedisChannel channel, RedisValue value)
        {
            var userUUID = value.ToString().Split(':').Last();

            await _mediator.Publish(new CartDeletedEvent
            {
                UserUUID = userUUID
            });
        }

        public async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RedisEventsListener is stopping.");

            await _expiredEventsSubscriber.UnsubscribeAsync(EXPIRED_KEYS_CHANNEL);
        }

        public void Dispose()
        {
            _redis.Dispose();
        }
    }
}