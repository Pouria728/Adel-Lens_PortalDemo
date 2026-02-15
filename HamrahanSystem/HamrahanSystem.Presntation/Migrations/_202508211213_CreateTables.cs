using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202508211213)]
	public class _202508211213_CreateTables : FluentMigrator.Migration
	{
		public override void Up()
		{
			string s = @"	
INSERT INTO [dbo].[Permissions]
           ([Title]
           ,[PermissionKey])
     VALUES

('مشاهده گامهای فرایند','Step_Index'),
			('ویرایش گامهای فرایند','Step_Edit'),
('ایجاد گامهای فرایند','Step_Create'),
('حذف گامهای فرایند','Step_Delete')

GO

		if not exists(select * from sys.objects a join sys.columns b on a.object_id=b.object_id     where type='u' and  a.name='Tbl_Lns_Order' and b.name='FactorNo')
		 alter table [Tbl_Lns_Order] add FactorNo nvarchar(20) null 

				";
			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
