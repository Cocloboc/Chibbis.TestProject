using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application.Entities;
using Microsoft.AspNetCore.WebUtilities;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Chibbis.TestProject.CartService.Application.Services
{
    public class WebhookService : IWebhookService
    {
        private readonly string _prefix = "webhook";
        private readonly IRedisClient _redisClient;
        private readonly HttpClient _httpClient;

        public WebhookService(
            IRedisClient redisClient,
            HttpClient httpClient)
        {
            _redisClient = redisClient;
            _httpClient = httpClient;
        }

        public async Task<Webhook> GetWebhookAsync(string userUUID)
        {
            return await _redisClient.GetDefaultDatabase()
                .GetAsync<Webhook>($"{_prefix}:{userUUID}");
        }

        public async Task SetWebhookAsync(string userUUID, Webhook webhook)
        {
            await _redisClient.GetDefaultDatabase()
                .AddAsync($"{_prefix}:{userUUID}", webhook);
        }

        public async Task DeleteWebhookAsync(string userUUID)
        {
            await _redisClient.GetDefaultDatabase()
                .RemoveAsync($"{_prefix}:{userUUID}");
        }

        public async Task CallWebhookAsync(string userUUID)
        {
            var webhook = await GetWebhookAsync(userUUID);

            if (webhook == default)
            {
                return;
            }

            switch (webhook.WebhookType)
            {
                case WebhookType.Get:
                {
                    await CallGetWebhookAsync(userUUID, webhook.Url);
                    break;
                }
                case WebhookType.Post:
                {
                    await CallPostWebhookAsync(userUUID, webhook.Url);
                    break;
                }
            }
        }

        private async Task CallGetWebhookAsync(string userUUID, string url)
        {
            var query = new Dictionary<string, string>()
            {
                ["userUUID"] = userUUID
            };
            var uri = QueryHelpers.AddQueryString(url, query);

            await _httpClient.GetAsync(uri);
        }

        private async Task CallPostWebhookAsync(string userUUID, string url)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("userUUID", userUUID)
            });
            await _httpClient.PostAsync(url, content);
        }
    }
}