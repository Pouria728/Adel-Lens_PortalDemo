using System;
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{
	public partial class UserActivityLogDto
	{
		public long ActivityLogId { get; set; }
		public Guid CorrelationId { get; set; }
		public int? UserId { get; set; }
		public string? UserName { get; set; }
		public string? ActionType { get; set; }
		public string? Controller { get; set; }
		public string? Action { get; set; }
		public string? FormName { get; set; }
		public string? HttpMethod { get; set; }
		public string? Path { get; set; }
		public string? QueryString { get; set; }
		public string? RequestBody { get; set; }
		public int? StatusCode { get; set; }
		public int? DurationMs { get; set; }
		public string? IpAddress { get; set; }
		public string? UserAgent { get; set; }
		public DateTime CreatedAt { get; set; }

		public List<UserActivityLogDetailDto> Details { get; set; } = new List<UserActivityLogDetailDto>();
	}
}
