using System;
using System.IO;
using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application.Options;
using Microsoft.Extensions.Options;

namespace Chibbis.TestProject.CartService.Application.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly ReportOptions _reportOptions;

        public ReportService(IOptions<ReportOptions> reportOptions)
        {
            _reportOptions = reportOptions.Value;
        }

        public async Task GenerateCartReportAsync(CartReportParams reportParams)
        {
            var report = $@"Carts that expire within thirty days: {reportParams.LessThanThirtyDays}
Carts that expire within twenty days: {reportParams.LessThanTwentyDays}
Carts that expire within ten days: {reportParams.LessThanTenDays}
Average cost: {reportParams.AverageCost}
Carts containing products for bonuses: {reportParams.CartsWithBonuses}";
            
            // Write the specified text asynchronously to a new file named "WriteTextAsync.txt".
            var today = DateTime.Now.ToString("MM_dd_yyyy");
            await using StreamWriter outputFile = new StreamWriter(Path.Combine(_reportOptions.OutputPath, $"CartReport_{today}.txt"));
            
            await outputFile.WriteAsync(report);
        }
    }
}