namespace Chibbis.TestProject.CartService.Application.Services.ReportService
{
    public class CartReportParams
    {
        public int LessThanTenDays { get; set; }
        public int LessThanTwentyDays { get; set; }
        public int LessThanThirtyDays { get; set; }
        public int CartsWithBonuses { get; set; }
        public decimal AverageCost { get; set; }
    }
}