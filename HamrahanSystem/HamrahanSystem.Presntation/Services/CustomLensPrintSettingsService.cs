using System.Text.Json;

namespace HamrahanSystem.Presntation.Services
{
    public sealed class CustomLensPrintSettingsService(IWebHostEnvironment environment, ILogger<CustomLensPrintSettingsService> logger)
        : ICustomLensPrintSettingsService
    {
        private static readonly SemaphoreSlim SyncRoot = new(1, 1);
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        private string FilePath => Path.Combine(environment.ContentRootPath, "App_Data", "custom-lens-print-settings.json");

        public async Task<CustomLensPrintSettings> GetAsync()
        {
            await SyncRoot.WaitAsync();
            try
            {
                return await LoadUnsafeAsync();
            }
            finally
            {
                SyncRoot.Release();
            }
        }

        public async Task SaveAsync(CustomLensPrintSettings settings)
        {
            await SyncRoot.WaitAsync();
            try
            {
                var normalized = Normalize(settings);
                var directory = Path.GetDirectoryName(FilePath);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var json = JsonSerializer.Serialize(normalized, _jsonOptions);
                await File.WriteAllTextAsync(FilePath, json);
            }
            finally
            {
                SyncRoot.Release();
            }
        }

        private async Task<CustomLensPrintSettings> LoadUnsafeAsync()
        {
            if (!File.Exists(FilePath))
            {
                return new CustomLensPrintSettings();
            }

            try
            {
                var json = await File.ReadAllTextAsync(FilePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new CustomLensPrintSettings();
                }

                var settings = JsonSerializer.Deserialize<CustomLensPrintSettings>(json, _jsonOptions);
                return Normalize(settings ?? new CustomLensPrintSettings());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to read custom lens print settings from {Path}", FilePath);
                return new CustomLensPrintSettings();
            }
        }

        private static CustomLensPrintSettings Normalize(CustomLensPrintSettings settings)
        {
            return new CustomLensPrintSettings
            {
                IsEnabled = settings.IsEnabled,
                ReportUrlTemplate = (settings.ReportUrlTemplate ?? string.Empty).Trim(),
                Printers = (settings.Printers ?? new List<string>())
                    .Select(x => (x ?? string.Empty).Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList()
            };
        }
    }
}
