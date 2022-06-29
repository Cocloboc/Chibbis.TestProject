namespace Chibbis.TestProject.CartService.Application.Options
{
    public class ReportOptions
    {
        public static string Section => "ReportOptions";
        public string OutputPath { get; set; }
        public string Schedule { get; set; }
    }
}