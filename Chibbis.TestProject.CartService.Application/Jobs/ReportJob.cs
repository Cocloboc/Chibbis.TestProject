using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Common;
using Chibbis.TestProject.CartService.Application.Entities;
using Chibbis.TestProject.CartService.Application.Services;
using Chibbis.TestProject.CartService.Application.Services.ReportService;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Chibbis.TestProject.CartService.Application.Jobs
{
    [DisallowConcurrentExecution]
    public class ReportJob : IJob
    {
        private readonly ILogger<ReportJob> _logger;
        private readonly ICartService _cartService;
        private readonly IReportService _reportService;

        public ReportJob(
            ICartService cartService,
            ILogger<ReportJob> logger,
            IReportService reportService)
        {
            _cartService = cartService;
            _logger = logger;
            _reportService = reportService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Starting generate report at {DateTime.Now}");
            var carts = await _cartService.GetAllCartsAsync();
            var reportParams = new CartReportParams();

            var now = DateTime.Now;
            reportParams.LessThanThirtyDays = carts.Count(cart => cart.ExpireAt > now.AddDays(20));
            reportParams.LessThanTwentyDays = carts.Count(cart => cart.ExpireAt > now.AddDays(10) && cart.ExpireAt < now.AddDays(20));
            reportParams.LessThanTenDays = carts.Count - reportParams.LessThanTwentyDays - reportParams.LessThanThirtyDays;
            reportParams.CartsWithBonuses = carts.Count(cart => cart.CartItems.Any(cartItem => cartItem.Product.ForBonusPoints));
            reportParams.AverageCost = carts.SelectMany(cart => cart.CartItems)
                .Sum(cartItem => cartItem.Product.Cost * cartItem.Amount) / carts.Count;

            await _reportService.GenerateCartReportAsync(reportParams);
        }
    }
}