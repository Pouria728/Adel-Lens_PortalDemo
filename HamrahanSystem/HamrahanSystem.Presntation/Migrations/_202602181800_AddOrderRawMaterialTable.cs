using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [Migration(202602181800)]
    public class _202602181800_AddOrderRawMaterialTable : Migration
    {
        public override void Up()
        {
            const string sql = @"
IF OBJECT_ID('dbo.Tbl_Wfw_OrderRawMaterial', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tbl_Wfw_OrderRawMaterial]
    (
        [OrderRawMaterialId] BIGINT IDENTITY(1,1) NOT NULL,
        [OrderId] BIGINT NOT NULL,
        [OrderProcessId] BIGINT NOT NULL,
        [ProcessStepId] INT NOT NULL,
        [DefineObjectId] INT NOT NULL,
        [DefineObjectRecNo] INT NULL,
        [Barcode] NVARCHAR(100) NULL,
        [MaterialName] NVARCHAR(254) NULL,
        [Quantity] INT NOT NULL CONSTRAINT [DF_Tbl_Wfw_OrderRawMaterial_Quantity] DEFAULT(1),
        [DateCreate] DATETIME NOT NULL CONSTRAINT [DF_Tbl_Wfw_OrderRawMaterial_DateCreate] DEFAULT(GETDATE()),
        [DateUpdate] DATETIME NOT NULL CONSTRAINT [DF_Tbl_Wfw_OrderRawMaterial_DateUpdate] DEFAULT(GETDATE()),
        [CreatedBy] INT NULL,
        [ModifiedBy] INT NULL,
        CONSTRAINT [PK_Tbl_Wfw_OrderRawMaterial] PRIMARY KEY CLUSTERED ([OrderRawMaterialId] ASC)
    );
END;

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'UX_Tbl_Wfw_OrderRawMaterial_OrderStepMaterial'
      AND object_id = OBJECT_ID('dbo.Tbl_Wfw_OrderRawMaterial')
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Tbl_Wfw_OrderRawMaterial_OrderStepMaterial]
        ON [dbo].[Tbl_Wfw_OrderRawMaterial]([OrderId], [ProcessStepId], [DefineObjectId]);
END;
";
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}
