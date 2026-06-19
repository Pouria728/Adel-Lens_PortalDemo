using System.ComponentModel.DataAnnotations;

namespace HamrahanSystem.Presntation.Models
{
    public sealed class CustomLensPrintSettingsViewModel
    {
        public int? ProcessId { get; set; }
        public int? ProcessStepId { get; set; }
        public string ReturnUrl { get; set; } = string.Empty;

        public bool IsEnabled { get; set; }

        [Display(Name = "آدرس گزارش")]
        public string ReportUrlTemplate { get; set; } = string.Empty;

        [Display(Name = "پرینترها")]
        public string PrinterNamesText { get; set; } = string.Empty;
    }
}
