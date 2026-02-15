using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202508211205)]
	public class _202508211205_CreateTables : FluentMigrator.Migration
	{
		public override void Up()
		{
		string s= @"	
if not exists(select * from sys.objects a join sys.columns b on a.object_id=b.object_id     where type='u' and  a.name='users' and b.name='PasswordSalt')
ALTER TABLE users ALTER COLUMN PasswordSalt NVARCHAR(60);
GO
delete rolepermissions where len(permissionkey)>0 
GO
if(exists(select * from sys.indexes where name='UQ_RolePerm_RoleId_PermKey'))
DROP INDEX UQ_RolePerm_RoleId_PermKey ON dbo.RolePermissions
GO
CREATE UNIQUE NONCLUSTERED INDEX UQ_RolePerm_RoleId_PermKey ON dbo.RolePermissions
	(
	RoleId,
	PermissionId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

";


			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
