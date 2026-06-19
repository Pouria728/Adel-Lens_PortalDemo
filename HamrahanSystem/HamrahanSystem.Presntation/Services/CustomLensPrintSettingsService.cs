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

        public async Task<CustomLensPrintScopedSettings?> GetProcessSettingsAsync(int processId)
        {
            if (processId <= 0)
            {
                return null;
            }

            await SyncRoot.WaitAsync();
            try
            {
                var settings = await LoadUnsafeAsync();
                return settings.ProcessSettings.TryGetValue(processId, out var scoped)
                    ? CloneScopedSettings(scoped)
                    : null;
            }
            finally
            {
                SyncRoot.Release();
            }
        }

        public async Task SaveProcessSettingsAsync(int processId, CustomLensPrintScopedSettings settings)
        {
            if (processId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(processId));
            }

            await SyncRoot.WaitAsync();
            try
            {
                var current = await LoadUnsafeAsync();
                current.ProcessSettings[processId] = NormalizeScopedSettings(settings);
                await SaveUnsafeAsync(current);
            }
            finally
            {
                SyncRoot.Release();
            }
        }

        public async Task<CustomLensPrintScopedSettings?> GetStepSettingsAsync(int processStepId)
        {
            if (processStepId <= 0)
            {
                return null;
            }

            await SyncRoot.WaitAsync();
            try
            {
                var settings = await LoadUnsafeAsync();
                return settings.StepSettings.TryGetValue(processStepId, out var scoped)
                    ? CloneScopedSettings(scoped)
                    : null;
            }
            finally
            {
                SyncRoot.Release();
            }
        }

        public async Task SaveStepSettingsAsync(int processStepId, CustomLensPrintScopedSettings settings)
        {
            if (processStepId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(processStepId));
            }

            await SyncRoot.WaitAsync();
            try
            {
                var current = await LoadUnsafeAsync();
                current.StepSettings[processStepId] = NormalizeScopedSettings(settings);
                await SaveUnsafeAsync(current);
            }
            finally
            {
                SyncRoot.Release();
            }
        }

        public async Task<CustomLensPrintScopedSettings> ResolveAsync(int? processId, int? processStepId)
        {
            await SyncRoot.WaitAsync();
            try
            {
                var settings = await LoadUnsafeAsync();

                if (processStepId.HasValue &&
                    processStepId.Value > 0 &&
                    settings.StepSettings.TryGetValue(processStepId.Value, out var stepScoped))
                {
                    return CloneScopedSettings(stepScoped);
                }

                if (processId.HasValue &&
                    processId.Value > 0 &&
                    settings.ProcessSettings.TryGetValue(processId.Value, out var processScoped))
                {
                    return CloneScopedSettings(processScoped);
                }

                return new CustomLensPrintScopedSettings
                {
                    IsEnabled = settings.IsEnabled,
                    ReportUrlTemplate = settings.ReportUrlTemplate,
                    Printers = settings.Printers.ToList()
                };
            }
            finally
            {
                SyncRoot.Release();
            }
        }

        public bool HasValidReportTemplate(CustomLensPrintScopedSettings? settings)
        {
            return settings != null &&
                   settings.IsEnabled &&
                   !string.IsNullOrWhiteSpace(settings.ReportUrlTemplate);
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

        private async Task SaveUnsafeAsync(CustomLensPrintSettings settings)
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
                    .ToList(),
                ProcessSettings = NormalizeScopedDictionary(settings.ProcessSettings),
                StepSettings = NormalizeScopedDictionary(settings.StepSettings)
            };
        }

        private static Dictionary<int, CustomLensPrintScopedSettings> NormalizeScopedDictionary(
            IDictionary<int, CustomLensPrintScopedSettings>? source)
        {
            var result = new Dictionary<int, CustomLensPrintScopedSettings>();
            if (source == null)
            {
                return result;
            }

            foreach (var pair in source.Where(x => x.Key > 0))
            {
                result[pair.Key] = NormalizeScopedSettings(pair.Value);
            }

            return result;
        }

        private static CustomLensPrintScopedSettings NormalizeScopedSettings(CustomLensPrintScopedSettings? settings)
        {
            settings ??= new CustomLensPrintScopedSettings();

            return new CustomLensPrintScopedSettings
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

        private static CustomLensPrintScopedSettings CloneScopedSettings(CustomLensPrintScopedSettings settings)
        {
            return new CustomLensPrintScopedSettings
            {
                IsEnabled = settings.IsEnabled,
                ReportUrlTemplate = settings.ReportUrlTemplate,
                Printers = settings.Printers?.ToList() ?? new List<string>()
            };
        }
    }
}
