using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202508211203)]
	public class _202508211203_CreateTables : FluentMigrator.Migration
	{
		public override void Up()
		{
		string s= @"	
if not exists(select * from sys.objects  where type='u' and  name='Permissions')
begin 
	CREATE TABLE [dbo].[Permissions](
		[PermissionId] [int] IDENTITY(1,1) NOT NULL,
		[Title] [nvarchar](20) NULL,
		[PermissionKey] [nvarchar](50) NULL,
	 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
	(
		[PermissionId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
end

GO
if not exists(select * from sys.objects a join sys.columns b on a.object_id=b.object_id     where type='u' and  a.name='UserPermissions' and b.name='PermissionId')
alter table UserPermissions add PermissionId int null
GO
if not exists(select * from sys.objects a join sys.columns b on a.object_id=b.object_id     where type='u' and  a.name='RolePermissions' and b.name='PermissionId')
alter table [RolePermissions] add PermissionId int null

GO
if not exists(select * from sys.objects a join sys.columns b on a.object_id=b.object_id     where type='u' and  a.name='Users' and b.name='IsAdmin')
alter table [Users] add IsAdmin  bit not null default 0


";


			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
