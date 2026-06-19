namespace HamrahanSystem.Presntation.Services
{
    public interface ICustomLensAutoPrintService
    {
        Task<CustomLensAutoPrintResult> DispatchAsync(long orderId, string? factorNo, int? processId = null, int? processStepId = null, CancellationToken cancellationToken = default);
    }

    public sealed class CustomLensAutoPrintResult
    {
        public bool Attempted { get; set; }
        public int SentCount { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
