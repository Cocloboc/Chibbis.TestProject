using System.Threading.Tasks;

namespace Chibbis.TestProject.CartService.Application.Services.ReportService
{
    public interface IReportService
    {
        Task GenerateCartReportAsync(CartReportParams reportParams);
    }
}