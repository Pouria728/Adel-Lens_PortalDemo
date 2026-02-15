using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602101300)]
    public class _202602101300_AddCustomSphCylTables : FluentMigrator.Migration
    {
        public override void Up()
        {
            string sql = @"
IF OBJECT_ID('dbo.Tbl_Lns_CustomSph', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Lns_CustomSph] (
        [CustomSphId] INT IDENTITY(1,1) NOT NULL,
        [Name] NVARCHAR(254) NULL,
        [Code] NVARCHAR(254) NULL,
        [Description] NVARCHAR(500) NULL,
        [OrderId] INT NOT NULL CONSTRAINT [DF_Tbl_Lns_CustomSph_OrderId] DEFAULT 1,
        [IsActive] SMALLINT NOT NULL CONSTRAINT [DF_Tbl_Lns_CustomSph_IsActive] DEFAULT 1,
        CONSTRAINT [PK_Tbl_Lns_CustomSph] PRIMARY KEY CLUSTERED ([CustomSphId])
    )
END;

IF OBJECT_ID('dbo.Tbl_Lns_CustomCyl', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Lns_CustomCyl] (
        [CustomCylId] INT IDENTITY(1,1) NOT NULL,
        [Name] NVARCHAR(254) NULL,
        [Code] NVARCHAR(254) NULL,
        [Description] NVARCHAR(500) NULL,
        [OrderId] INT NOT NULL CONSTRAINT [DF_Tbl_Lns_CustomCyl_OrderId] DEFAULT 1,
        [IsActive] SMALLINT NOT NULL CONSTRAINT [DF_Tbl_Lns_CustomCyl_IsActive] DEFAULT 1,
        CONSTRAINT [PK_Tbl_Lns_CustomCyl] PRIMARY KEY CLUSTERED ([CustomCylId])
    )
END;
";

            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}
