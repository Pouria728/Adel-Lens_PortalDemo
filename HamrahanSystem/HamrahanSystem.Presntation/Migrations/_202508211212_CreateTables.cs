using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202508211212)]
	public class _202508211212_CreateTables : FluentMigrator.Migration
	{
		public override void Up()
		{
			string s = @"	
INSERT INTO [dbo].[Permissions]
           ([Title]
           ,[PermissionKey])
     VALUES
			('حذف برند','Brand_Delete'),
			('حذف Coating','Coating_Delete'),
			('حذف ColoringType','ColoringType_Delete'),
			('حذف Cyl','Cyl_Delete'),

			('حذف DesignType','DesignType_Delete'),

			('حذف FeameType','FrameType_Delete'),

			('حذف LensIndexR','LensIndexR_Delete'),

			('حذف LensType','LensType_Delete'),

			('حذف MaterialType','MaterialType_Delete'),

			('حذف LensTypeR','LensTypeR_Delete'),

			('حذف فرآیند','Process_Delete'),

			('حذف گروه دسترسی','Role_Delete'),

			('حذف گامهای فرآیند','Step_Delete')


				";
			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
