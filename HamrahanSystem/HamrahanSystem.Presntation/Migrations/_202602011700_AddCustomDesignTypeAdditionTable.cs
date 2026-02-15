using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602011700)]
    public class _202602011700_AddCustomDesignTypeAdditionTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
IF OBJECT_ID('dbo.Tbl_Lns_CustomDesignTypeAddition', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Lns_CustomDesignTypeAddition] (
        [CustomDesignTypeAdditionId] INT IDENTITY(1,1) NOT NULL,
        [DesignTypeId] INT NULL,
        [AdditionValue] DECIMAL(18,2) NOT NULL,
        [OrderId] INT NOT NULL CONSTRAINT [DF_Tbl_Lns_CustomDesignTypeAddition_OrderId] DEFAULT 1,
        [IsActive] SMALLINT NOT NULL CONSTRAINT [DF_Tbl_Lns_CustomDesignTypeAddition_IsActive] DEFAULT 1,
        CONSTRAINT [PK_Tbl_Lns_CustomDesignTypeAddition] PRIMARY KEY CLUSTERED ([CustomDesignTypeAdditionId])
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
