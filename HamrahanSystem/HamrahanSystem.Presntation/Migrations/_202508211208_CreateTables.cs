using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202508211208)]
	public class _202508211208_CreateTables : FluentMigrator.Migration
	{
		public override void Up()
		{
			string s = @"	
	

		ALTER TABLE Permissions ALTER COLUMN title NVARCHAR(50);
GO


INSERT INTO [dbo].[Permissions]
           ([Title]
           ,[PermissionKey])
     VALUES
           ('مشاهده برند','Brand_Index'),
		    ('ایجاد برند','Brand_Create'),
			('ویرایش برند','Brand_Edit'),
			('مشاهده coating','Coating_Index'),
			('ایجاد coating','Coating_Create'),
			('ویرایش Coating','Coating_Edit'),
			('مشاهده ColoringType','ColoringType_Index'),
			('ایجاد ColoringType','ColoringType_Create'),
			('ویرایش ColoringType','ColoringType_Edit'),
			('مشاهده Cyl','Cyl_Index'),
			('ایجاد Cyl','Cyl_Create'),
			('ویرایش Cyl','Cyl_Edit'),

			('مشاهده DesignType','DesignType_Index'),
			('ایجاد DesignType','DesignType_Create'),
			('ویرایش DesignType','DesignType_Edit'),

			('مشاهده FrameType','FrameType_Index'),
			('ایجاد FrameType','FrameType_Create'),
			('ویرایش FeameType','FrameType_Edit'),

			('مشاهده LensIndexR','LensIndexR_Index'),
			('ایجاد LensIndexR','LensIndexR_Create'),
			('ویرایش LensIndexR','LensIndexR_Edit'),

			('مشاهده LensType','LensType_Index'),
			('ایجاد LensType','LensType_Create'),
			('ویرایش LensType','LensType_Edit'),

			('مشاهده MaterialType','MaterialType_Index'),
			('ایجاد MaterialType','MaterialType_Create'),
			('ویرایش MaterialType','MaterialType_Edit'),

			('مشاهده LensTypeR','LensTypeR_Index'),
			('ایجاد LensTypeR','LensTypeR_Create'),
			('ویرایش LensTypeR','LensTypeR_Edit'),

			('مشاهده فرآیند','Process_Index'),
			('ایجاد فرآیند','Process_Create'),
			('ویرایش فرآیند','Process_Edit'),

			('مشاهده گروه دسترسی','Role_Index'),
			('ایجاد گروه دسترسی ','Role_Create'),
			('ویرایش گروه دسترسی','Role_Edit'),

			('مشاهده گامهای فرآیند','Step_Index'),
			('ایجاد گامهای فرآیند','Step_Create'),
			('ویرایش گامهای فرآیند','Step_Edit'),

			('مشاهده کاربران','User_Index'),
			('ایجاد کاربران','User_Create'),
			('ویرایش کاربران','User_Edit'),

			('ثبت عدسی آماده','OrderStock_Index'),
			('ثبت عدسی آماده با گارانتی','OrderStockGranty_Index'),
			(' سفارشها','ListOrder_Index'),
			('بررسی سفارشها','OrderReq_Index')






";


			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
