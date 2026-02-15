using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602011030)]
    public class _202602011030_AddCustomLensTypeCoatingTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
IF OBJECT_ID('dbo.Tbl_Lns_CustomLensTypeCoating', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Lns_CustomLensTypeCoating] (
        [CustomLensTypeCoatingId] INT IDENTITY(1,1) NOT NULL,
        [DesignTypeId] INT NULL,
        [LensTypeName] NVARCHAR(254) NULL,
        [CoatingName] NVARCHAR(254) NULL,
        [OrderId] INT NOT NULL CONSTRAINT [DF_Tbl_Lns_CustomLensTypeCoating_OrderId] DEFAULT 1,
        [IsActive] SMALLINT NOT NULL CONSTRAINT [DF_Tbl_Lns_CustomLensTypeCoating_IsActive] DEFAULT 1,
        CONSTRAINT [PK_Tbl_Lns_CustomLensTypeCoating] PRIMARY KEY CLUSTERED ([CustomLensTypeCoatingId])
    );
END;
";

            Execute.Sql(s);
        }

        public override void Down()
        {
        }
    }
}

