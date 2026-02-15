using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202508211209)]
	public class _202508211209_CreateTables : FluentMigrator.Migration
	{
		public override void Up()
		{
			string s = @"	
	
if not exists(select * from sys.objects a join sys.columns b on a.object_id=b.object_id     where type='u' and  a.name='Tbl_Wfw_ProcessStep' and b.name='ProcedureId')
alter table [Tbl_Wfw_ProcessStep] add ProcedureId int not null default 0
GO
if not exists(select * from sys.objects a join sys.columns b on a.object_id=b.object_id     where type='u' and  a.name='Tbl_Wfw_ProcessStep' and b.name='IsBarcode')
alter table [Tbl_Wfw_ProcessStep] add IsBarcode bit not null default 0

GO

if not exists(select * from sys.objects a join sys.columns b on a.object_id=b.object_id     where type='u' and  a.name='Tbl_Clr_DefineObject' and b.name='IsActive')
	alter table Tbl_Clr_DefineObject add 	[IsActive] [smallint] NULL,	[RnKind] [int] NULL,	[RnGroup] [int] NULL
GO
if not exists(select * from sys.objects a join sys.columns b on a.object_id=b.object_id     where type='u' and  a.name='Tbl_DefineCustomer' and b.name='DefineCustomerId')
	alter table sal.[Tbl_DefineCustomer] add [DefineCustomerId] [int] IDENTITY(1,1) NOT NULL,	[ParentId] [int] NULL,	[Path] [nvarchar](255) NULL
GO
if  exists(select * from sys.objects a      where type='V' and  a.name='View_Sal_ListCustomer' )
Drop VIEW [dbo].[View_Sal_ListCustomer]
GO
Create VIEW [dbo].[View_Sal_ListCustomer]
AS
SELECT        t1.RecNo, t1.CodeCompany, t1.CodeCompany AS Expr1, REPLACE(REPLACE(CAST(t1.NameFormal AS nvarchar(MAX)), NCHAR(1610), NCHAR(1740)), NCHAR(1603), NCHAR(1705)) AS NameFormal, t1.NameFormalEN, 
                         t1.StateFormal, t1.Company, t1.TypeFormal, t1.Active, t1.SActive, t1.DateOpen, t1.CodeValuta, t1.RNValuta, t1.Caption, t1.RNGroupFormal, t1.RefMasterKey, t1.RefMasterCompany, t2.DefineCustomerId, 
                         t2.RecNo AS RefCustomer
FROM            (SELECT        DC.RecNo, DC.CodeCompany, DC.CodeCompany AS Expr1, DC.NameCompany + CASE ShowFormalCaptions WHEN 1 THEN (CASE WHEN dbo.IsNullOrSpace(Caption, '') <> '' THEN '(' + Caption + ')' ELSE '' END) 
                                ELSE '' END AS NameFormal, DC.LatinNameCompany AS NameFormalEN, DC.StateFormal, DC.Company, SV.TypeFormal, DC.Active, (CASE DC.Active WHEN 0 THEN 'غیر فعال' WHEN 1 THEN 'فعال' END) AS SActive, 
                                '' AS DateOpen, NULL AS CodeValuta, NULL AS RNValuta, DC.Caption, ISNULL(DC.RNGroupFormal, 0) AS RNGroupFormal, DC.RefMasterKey, DC.RefMasterCompany
                           FROM            dbo.Tbl_Acc_DefineCompany AS DC INNER JOIN
                                                    dbo.Tbl_Main_StaticValue AS SV ON DC.StateFormal = SV.Number INNER JOIN
                                                    Main.Tbl_SystemManager AS S ON S.Company = DC.Company
                           UNION
                           SELECT        DP.RecNo, DP.CodePersona, DP.CodePersona AS Expr1, DP.NamePersona + ' ' + DP.FamilyPersona + CASE ShowFormalCaptions WHEN 1 THEN (CASE WHEN dbo.IsNullOrSpace(Caption, '') 
                                                    <> '' THEN '(' + Caption + ')' ELSE '' END) ELSE '' END AS NameFormal, DP.LatinNamePersona + ' ' + DP.LatinFamilyPersona AS NameFormalEN, DP.StateFormal, DP.Company, SV.TypeFormal, DP.Active, 
                                                    (CASE DP.Active WHEN 0 THEN 'غیر فعال' WHEN 1 THEN 'فعال' END) AS SActive, '' AS DateOpen, NULL AS CodeValuta, NULL AS RNValuta, DP.Caption, ISNULL(DP.RNGroupFormal, 0) AS Expr2, DP.RefMasterKey, 
                                                    DP.RefMasterCompany
                           FROM            dbo.Tbl_Acc_DefinePersona AS DP INNER JOIN
                                                    dbo.Tbl_Main_StaticValue AS SV ON DP.StateFormal = SV.Number INNER JOIN
                                                    Main.Tbl_SystemManager AS S ON S.Company = DP.Company) AS t1 LEFT OUTER JOIN
                         Sal.Tbl_DefineCustomer AS t2 ON t1.Company = t2.Company AND t1.RecNo = t2.RefFormal
GO




				";
			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
