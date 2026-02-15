using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202508211204)]
	public class _202508211204_CreateTables : FluentMigrator.Migration
	{
		public override void Up()
		{
		string s= @"	
update Tbl_Lns_Brand set CreateDate='',CreatedBy='',Description='',ModifiedBy=1,ModifiedDate='1404/03/13'
GO
update Tbl_Lns_Coating set CreateDate='',CreatedBy='',Description='',ModifiedBy=1,ModifiedDate='1404/03/13'
GO
update Tbl_Lns_ColoringType set CreateDate='',CreatedBy='',Description='',ModifiedBy=1,ModifiedDate='1404/03/13'
GO
update Tbl_Lns_Cyl set CreateDate='',CreatedBy='',Description='',ModifiedBy=1,ModifiedDate='1404/03/13'
Go
update Tbl_Lns_DesignType set CreateDate='',CreatedBy='',Description='',ModifiedBy=1,ModifiedDate='1404/03/13'
Go
update Tbl_Lns_FrameType set CreateDate='',CreatedBy='',Description='',ModifiedBy=1,ModifiedDate='1404/03/13'
Go
update Tbl_Lns_LensIndex set CreateDate='',CreatedBy='',Description='',ModifiedBy=1,ModifiedDate='1404/03/13'
Go
update Tbl_Lns_LensIndexR set CreateDate='',CreatedBy='',Description='',ModifiedBy=1,ModifiedDate='1404/03/13'
Go
update Tbl_Lns_LensType set CreateDate='',CreatedBy='',Description='',ModifiedBy=1,ModifiedDate='1404/03/13'
Go
update Tbl_Lns_LensTypeR set CreateDate='',CreatedBy='',Description='',ModifiedBy=1,ModifiedDate='1404/03/13'
Go
update Tbl_Lns_MaterialType set CreateDate='',CreatedBy='',Description='',ModifiedBy=1,ModifiedDate='1404/03/13'
Go
update Tbl_Lns_Sph set CreateDate='',CreatedBy='',Description='',ModifiedBy=1,ModifiedDate='1404/03/13'
";


			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
