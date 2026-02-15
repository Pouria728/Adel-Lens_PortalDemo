using System;
using HamrahanSystem.Application.UseCaseInterface;

namespace HamrahanSystem.Presntation.Services
{
	public sealed class AuditContext : IAuditContext
	{
		public Guid? CorrelationId { get; set; }
	}
}
