using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202508211214)]
	public class _202508211214_CreateTables : FluentMigrator.Migration
	{
		public override void Up()
		{
			string s = @"
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserActivityLogs]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[UserActivityLogs](
		[ActivityLogId] [bigint] IDENTITY(1,1) NOT NULL,
		[CorrelationId] [uniqueidentifier] NOT NULL,
		[UserId] [int] NULL,
		[UserName] [nvarchar](200) NULL,
		[ActionType] [nvarchar](50) NULL,
		[Controller] [nvarchar](100) NULL,
		[Action] [nvarchar](100) NULL,
		[FormName] [nvarchar](200) NULL,
		[HttpMethod] [nvarchar](10) NULL,
		[Path] [nvarchar](400) NULL,
		[QueryString] [nvarchar](max) NULL,
		[RequestBody] [nvarchar](max) NULL,
		[StatusCode] [int] NULL,
		[DurationMs] [int] NULL,
		[IpAddress] [nvarchar](64) NULL,
		[UserAgent] [nvarchar](512) NULL,
		[CreatedAt] [datetime] NOT NULL,
	 CONSTRAINT [PK_UserActivityLogs] PRIMARY KEY CLUSTERED 
	(
		[ActivityLogId] ASC
	),
	CONSTRAINT [AK_UserActivityLogs_CorrelationId] UNIQUE ([CorrelationId])
	);
END;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserActivityLogDetails]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[UserActivityLogDetails](
		[ActivityLogDetailId] [bigint] IDENTITY(1,1) NOT NULL,
		[CorrelationId] [uniqueidentifier] NOT NULL,
		[UserId] [int] NULL,
		[UserName] [nvarchar](200) NULL,
		[EntityName] [nvarchar](200) NULL,
		[EntityId] [nvarchar](100) NULL,
		[Operation] [nvarchar](20) NULL,
		[Changes] [nvarchar](max) NULL,
		[CreatedAt] [datetime] NOT NULL,
	 CONSTRAINT [PK_UserActivityLogDetails] PRIMARY KEY CLUSTERED 
	(
		[ActivityLogDetailId] ASC
	)
	);
END;

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_UserActivityLogs_CreatedAt' AND object_id = OBJECT_ID('dbo.UserActivityLogs'))
	CREATE INDEX [IX_UserActivityLogs_CreatedAt] ON [dbo].[UserActivityLogs] ([CreatedAt] DESC);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_UserActivityLogs_UserId' AND object_id = OBJECT_ID('dbo.UserActivityLogs'))
	CREATE INDEX [IX_UserActivityLogs_UserId] ON [dbo].[UserActivityLogs] ([UserId]);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_UserActivityLogDetails_CorrelationId' AND object_id = OBJECT_ID('dbo.UserActivityLogDetails'))
	CREATE INDEX [IX_UserActivityLogDetails_CorrelationId] ON [dbo].[UserActivityLogDetails] ([CorrelationId]);

IF NOT EXISTS (SELECT 1 FROM [dbo].[Permissions] WHERE [PermissionKey] = 'Audit_Index')
BEGIN
	INSERT INTO [dbo].[Permissions] ([Title], [PermissionKey])
	VALUES ('User Activity Logs', 'Audit_Index');
END;
";

			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
