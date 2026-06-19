namespace HamrahanSystem.Presntation.Services
{
    public interface ICustomLensPrintSettingsService
    {
        Task<CustomLensPrintSettings> GetAsync();
        Task SaveAsync(CustomLensPrintSettings settings);
        Task<CustomLensPrintScopedSettings?> GetProcessSettingsAsync(int processId);
        Task SaveProcessSettingsAsync(int processId, CustomLensPrintScopedSettings settings);
        Task<CustomLensPrintScopedSettings?> GetStepSettingsAsync(int processStepId);
        Task SaveStepSettingsAsync(int processStepId, CustomLensPrintScopedSettings settings);
        Task<CustomLensPrintScopedSettings> ResolveAsync(int? processId, int? processStepId);
        bool HasValidReportTemplate(CustomLensPrintScopedSettings? settings);
    }

    public sealed class CustomLensPrintSettings
    {
        public bool IsEnabled { get; set; }
        public string ReportUrlTemplate { get; set; } = string.Empty;
        public List<string> Printers { get; set; } = new();
        public Dictionary<int, CustomLensPrintScopedSettings> ProcessSettings { get; set; } = new();
        public Dictionary<int, CustomLensPrintScopedSettings> StepSettings { get; set; } = new();
    }

    public sealed class CustomLensPrintScopedSettings
    {
        public bool IsEnabled { get; set; }
        public string ReportUrlTemplate { get; set; } = string.Empty;
        public List<string> Printers { get; set; } = new();
    }
}
