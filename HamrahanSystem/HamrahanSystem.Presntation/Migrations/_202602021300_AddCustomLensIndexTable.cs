using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602021300)]
    public class _202602021300_AddCustomLensIndexTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
IF OBJECT_ID('dbo.Tbl_Lns_CustomLensIndex', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Lns_CustomLensIndex] (
        [CustomLensIndexId] INT IDENTITY(1,1) NOT NULL,
        [DesignTypeId] INT NULL,
        [LensIndexName] NVARCHAR(254) NULL,
        [ColoringTypeStatus] NVARCHAR(10) NOT NULL CONSTRAINT [DF_Tbl_Lns_CustomLensIndex_ColoringTypeStatus] DEFAULT N'ندارد',
        [OrderId] INT NOT NULL CONSTRAINT [DF_Tbl_Lns_CustomLensIndex_OrderId] DEFAULT 1,
        [IsActive] SMALLINT NOT NULL CONSTRAINT [DF_Tbl_Lns_CustomLensIndex_IsActive] DEFAULT 1,
        CONSTRAINT [PK_Tbl_Lns_CustomLensIndex] PRIMARY KEY CLUSTERED ([CustomLensIndexId])
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
