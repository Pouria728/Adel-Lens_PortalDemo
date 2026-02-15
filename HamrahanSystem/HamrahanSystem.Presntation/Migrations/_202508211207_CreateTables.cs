using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202508211207)]
	public class _202508211207_CreateTables : FluentMigrator.Migration
	{
		public override void Up()
		{
			string s = @"	
		GO
		if not exists(select * from sys.objects a join sys.columns b on a.object_id=b.object_id     where type='u' and  a.name='Tbl_Lns_Order' and b.name='StatusId')
		alter table [Tbl_Lns_Order] add StatusId int not null default 1

		GO
		if not exists(select * from sys.objects a join sys.columns b on a.object_id=b.object_id     where type='u' and  a.name='Tbl_Wfw_ProcessStep' and b.name='orderId')
		alter table [Tbl_Wfw_ProcessStep] add OrderId  int not null default 1

		GO

		ALTER TABLE Tbl_Lns_Order ALTER COLUMN CreateDate NVARCHAR(26);
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcesses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_OrderProcesses](
	[OrderProcessId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[ProcessId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[DateComplete] [datetime] NULL,
 CONSTRAINT [PK_Tbl_wfw_OrderProcesses] PRIMARY KEY CLUSTERED 
(
	[OrderProcessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_wfw_OrderProcessSteps]    Script Date: 8/30/2025 1:27:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcessSteps]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_OrderProcessSteps](
	[OrderProcessStepId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderProcessId] [bigint] NOT NULL,
	[ProcessStepId] [int] NOT NULL,
	[UserId] [int] NULL,
	[StatusId] [int] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[DateComplete] [datetime] NULL,
 CONSTRAINT [PK_Tbl_wfw_OrderProcessSteps] PRIMARY KEY CLUSTERED 
(
	[OrderProcessStepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcesses_Tbl_Lns_Order]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_wfw_OrderProcesses]'))
ALTER TABLE [dbo].[Tbl_wfw_OrderProcesses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_wfw_OrderProcesses_Tbl_Lns_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Tbl_Lns_Order] ([OrderId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcesses_Tbl_Lns_Order]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_wfw_OrderProcesses]'))
ALTER TABLE [dbo].[Tbl_wfw_OrderProcesses] CHECK CONSTRAINT [FK_Tbl_wfw_OrderProcesses_Tbl_Lns_Order]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcesses_Tbl_Wfw_Process]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_wfw_OrderProcesses]'))
ALTER TABLE [dbo].[Tbl_wfw_OrderProcesses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_wfw_OrderProcesses_Tbl_Wfw_Process] FOREIGN KEY([ProcessId])
REFERENCES [dbo].[Tbl_Wfw_Process] ([ProcessId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcesses_Tbl_Wfw_Process]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_wfw_OrderProcesses]'))
ALTER TABLE [dbo].[Tbl_wfw_OrderProcesses] CHECK CONSTRAINT [FK_Tbl_wfw_OrderProcesses_Tbl_Wfw_Process]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcessSteps_Tbl_wfw_OrderProcesses]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_wfw_OrderProcessSteps]'))
ALTER TABLE [dbo].[Tbl_wfw_OrderProcessSteps]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_wfw_OrderProcessSteps_Tbl_wfw_OrderProcesses] FOREIGN KEY([OrderProcessId])
REFERENCES [dbo].[Tbl_wfw_OrderProcesses] ([OrderProcessId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcessSteps_Tbl_wfw_OrderProcesses]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_wfw_OrderProcessSteps]'))
ALTER TABLE [dbo].[Tbl_wfw_OrderProcessSteps] CHECK CONSTRAINT [FK_Tbl_wfw_OrderProcessSteps_Tbl_wfw_OrderProcesses]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcessSteps_Tbl_Wfw_ProcessStep]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_wfw_OrderProcessSteps]'))
ALTER TABLE [dbo].[Tbl_wfw_OrderProcessSteps]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_wfw_OrderProcessSteps_Tbl_Wfw_ProcessStep] FOREIGN KEY([ProcessStepId])
REFERENCES [dbo].[Tbl_Wfw_ProcessStep] ([ProcessStepId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcessSteps_Tbl_Wfw_ProcessStep]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_wfw_OrderProcessSteps]'))
ALTER TABLE [dbo].[Tbl_wfw_OrderProcessSteps] CHECK CONSTRAINT [FK_Tbl_wfw_OrderProcessSteps_Tbl_Wfw_ProcessStep]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcessSteps_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_wfw_OrderProcessSteps]'))
ALTER TABLE [dbo].[Tbl_wfw_OrderProcessSteps]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_wfw_OrderProcessSteps_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcessSteps_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_wfw_OrderProcessSteps]'))
ALTER TABLE [dbo].[Tbl_wfw_OrderProcessSteps] CHECK CONSTRAINT [FK_Tbl_wfw_OrderProcessSteps_Users]
GO

GO
update users set PasswordSalt='316f0547-01df-4a86-a556-86bff846f1c7',PasswordHash='NCzsQDPPqys='

";


			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
