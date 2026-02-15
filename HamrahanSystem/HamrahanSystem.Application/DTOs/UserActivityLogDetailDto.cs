using System;

namespace HamrahanSystem.Application.DTOs
{
	public partial class UserActivityLogDetailDto
	{
		public long ActivityLogDetailId { get; set; }
		public Guid CorrelationId { get; set; }
		public int? UserId { get; set; }
		public string? UserName { get; set; }
		public string? EntityName { get; set; }
		public string? EntityId { get; set; }
		public string? Operation { get; set; }
		public string? Changes { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
