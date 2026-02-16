namespace HamrahanSystem.Presntation.Services
{
    public interface ICustomLensPrintSettingsService
    {
        Task<CustomLensPrintSettings> GetAsync();
        Task SaveAsync(CustomLensPrintSettings settings);
    }

    public sealed class CustomLensPrintSettings
    {
        public bool IsEnabled { get; set; }
        public string ReportUrlTemplate { get; set; } = string.Empty;
        public List<string> Printers { get; set; } = new();
    }
}
