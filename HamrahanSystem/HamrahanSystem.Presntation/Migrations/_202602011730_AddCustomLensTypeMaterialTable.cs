using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602011730)]
    public class _202602011730_AddCustomLensTypeMaterialTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
IF OBJECT_ID('dbo.Tbl_Lns_CustomLensTypeMaterial', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Lns_CustomLensTypeMaterial] (
        [CustomLensTypeMaterialId] INT IDENTITY(1,1) NOT NULL,
        [DesignTypeId] INT NULL,
        [LensTypeName] NVARCHAR(254) NULL,
        [MaterialName] NVARCHAR(254) NULL,
        [OrderId] INT NOT NULL CONSTRAINT [DF_Tbl_Lns_CustomLensTypeMaterial_OrderId] DEFAULT 1,
        [IsActive] SMALLINT NOT NULL CONSTRAINT [DF_Tbl_Lns_CustomLensTypeMaterial_IsActive] DEFAULT 1,
        CONSTRAINT [PK_Tbl_Lns_CustomLensTypeMaterial] PRIMARY KEY CLUSTERED ([CustomLensTypeMaterialId])
    )
END;
";

            Execute.Sql(s);
        }

        public override void Down()
        {
        }
    }
}
