using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace HamrahanSystem.Presntation.Services
{
    public sealed class CustomLensAutoPrintService(
        ICustomLensPrintSettingsService settingsService,
        IHttpClientFactory httpClientFactory,
        ILogger<CustomLensAutoPrintService> logger) : ICustomLensAutoPrintService
    {
        private static readonly Regex PlaceholderRegex = new(@"\{(orderId|factorNo|printer)\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public async Task<CustomLensAutoPrintResult> DispatchAsync(long orderId, string? factorNo, CancellationToken cancellationToken = default)
        {
            var result = new CustomLensAutoPrintResult();
            var settings = await settingsService.GetAsync();
            if (!settings.IsEnabled || string.IsNullOrWhiteSpace(settings.ReportUrlTemplate))
            {
                return result;
            }

            var printers = settings.Printers.Count > 0 ? settings.Printers : new List<string> { string.Empty };
            var client = httpClientFactory.CreateClient("CustomLensAutoPrint");

            foreach (var printer in printers)
            {
                var url = BuildUrl(settings.ReportUrlTemplate, orderId, factorNo, printer);
                if (string.IsNullOrWhiteSpace(url))
                {
                    continue;
                }

                result.Attempted = true;
                try
                {
                    using var response = await client.GetAsync(url, cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var printed = await TryPrintPdfLocallyAsync(response, printer, cancellationToken);
                        if (printed)
                        {
                            result.SentCount++;
                        }
                        else
                        {
                            var error = $"Printer '{printer}': local print did not start.";
                            result.Errors.Add(error);
                            logger.LogWarning("Custom lens auto print did not start local print. URL={Url}, Printer={Printer}", url, printer);
                        }
                    }
                    else
                    {
                        var error = $"Printer '{printer}': {response.StatusCode}";
                        result.Errors.Add(error);
                        logger.LogWarning("Custom lens auto print failed. URL={Url}, StatusCode={StatusCode}", url, response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    var error = $"Printer '{printer}': {ex.Message}";
                    result.Errors.Add(error);
                    logger.LogError(ex, "Custom lens auto print request failed. URL={Url}", url);
                }
            }

            return result;
        }

        private static async Task<bool> TryPrintPdfLocallyAsync(HttpResponseMessage response, string? printer, CancellationToken cancellationToken)
        {
            var mediaType = response.Content.Headers.ContentType?.MediaType ?? string.Empty;
            if (!mediaType.Contains("pdf", StringComparison.OrdinalIgnoreCase))
            {
                // Legacy print endpoints may perform server-side printing without returning a PDF body.
                return true;
            }

            var pdfBytes = await response.Content.ReadAsByteArrayAsync(cancellationToken);
            if (pdfBytes.Length == 0)
            {
                return false;
            }

            var tempFilePath = Path.Combine(Path.GetTempPath(), $"custom-lens-{Guid.NewGuid():N}.pdf");
            await File.WriteAllBytesAsync(tempFilePath, pdfBytes, cancellationToken);

            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = tempFilePath,
                    UseShellExecute = true,
                    Verb = string.IsNullOrWhiteSpace(printer) ? "print" : "printto",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                if (!string.IsNullOrWhiteSpace(printer))
                {
                    startInfo.Arguments = $"\"{printer}\"";
                }

                using var process = Process.Start(startInfo);
                if (process == null)
                {
                    return false;
                }

                process.WaitForExit(5000);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await Task.Delay(30000, CancellationToken.None);
                        if (File.Exists(tempFilePath))
                        {
                            File.Delete(tempFilePath);
                        }
                    }
                    catch
                    {
                        // ignore temp file cleanup failures
                    }
                });
            }
        }

        private static string BuildUrl(string template, long orderId, string? factorNo, string? printer)
        {
            var replaced = PlaceholderRegex.Replace(template, match =>
            {
                var key = match.Groups[1].Value.ToLowerInvariant();
                return key switch
                {
                    "orderid" => Uri.EscapeDataString(orderId.ToString()),
                    "factorno" => Uri.EscapeDataString(factorNo ?? string.Empty),
                    "printer" => Uri.EscapeDataString(printer ?? string.Empty),
                    _ => string.Empty
                };
            }).Trim();

            var hasPrinterPlaceholder = template.Contains("{printer}", StringComparison.OrdinalIgnoreCase);
            if (!hasPrinterPlaceholder && !string.IsNullOrWhiteSpace(printer))
            {
                replaced = QueryHelpers.AddQueryString(replaced, "printer", printer);
            }

            return replaced;
        }
    }
}
