using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202508211210)]
	public class _202508211210_CreateTables : FluentMigrator.Migration
	{
		public override void Up()
		{
			string s = @"	
	
	
		if not exists(select * from sys.objects a join sys.columns b on a.object_id=b.object_id     where type='u' and  a.name='Tbl_Lns_Brand' and b.name='IsStock')
		 alter table [Tbl_Lns_Brand] add IsStock bit NOT NULL  default 0,	IsStockGranty bit NOT NULL  default 0,	IsSpecial [bit] NOT NULL  default 0






				";
			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
