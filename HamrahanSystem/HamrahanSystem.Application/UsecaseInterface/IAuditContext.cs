namespace HamrahanSystem.Application.UseCaseInterface
{
	public interface IAuditContext
	{
		Guid? CorrelationId { get; set; }
	}
}
