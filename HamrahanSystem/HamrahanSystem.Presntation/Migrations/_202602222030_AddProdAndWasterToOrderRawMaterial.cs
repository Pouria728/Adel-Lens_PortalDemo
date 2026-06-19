using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [Migration(202602222030)]
    public class _202602222030_AddProdAndWasterToOrderRawMaterial : Migration
    {
        public override void Up()
        {
            const string sql = @"
IF OBJECT_ID('dbo.Tbl_Wfw_OrderRawMaterial', 'U') IS NOT NULL
BEGIN
    IF COL_LENGTH('dbo.Tbl_Wfw_OrderRawMaterial', 'WasterRNObject') IS NULL
    BEGIN
        ALTER TABLE [dbo].[Tbl_Wfw_OrderRawMaterial]
        ADD [WasterRNObject] INT NULL;
    END;

    IF COL_LENGTH('dbo.Tbl_Wfw_OrderRawMaterial', 'ProdRNObject') IS NULL
    BEGIN
        ALTER TABLE [dbo].[Tbl_Wfw_OrderRawMaterial]
        ADD [ProdRNObject] INT NULL;
    END;

    IF EXISTS
    (
        SELECT 1
        FROM sys.indexes
        WHERE name = 'UX_Tbl_Wfw_OrderRawMaterial_OrderStepMaterial'
          AND object_id = OBJECT_ID('dbo.Tbl_Wfw_OrderRawMaterial')
    )
    BEGIN
        DROP INDEX [UX_Tbl_Wfw_OrderRawMaterial_OrderStepMaterial]
            ON [dbo].[Tbl_Wfw_OrderRawMaterial];
    END;

    IF NOT EXISTS
    (
        SELECT 1
        FROM sys.indexes
        WHERE object_id = OBJECT_ID('dbo.Tbl_Wfw_OrderRawMaterial')
          AND name = 'UX_Tbl_Wfw_OrderRawMaterial_OrderStepMaterial'
    )
    BEGIN
        CREATE UNIQUE NONCLUSTERED INDEX [UX_Tbl_Wfw_OrderRawMaterial_OrderStepMaterial]
        ON dbo.Tbl_Wfw_OrderRawMaterial
        (
            OrderId ASC,
            ProcessStepId ASC,
            DefineObjectId ASC,
            WasterRNObject ASC,
            ProdRNObject ASC
        );
    END;
END;

EXEC('
CREATE OR ALTER TRIGGER dbo.tr_wfw_orderprocess_insert_finalproduct
ON dbo.Tbl_Wfw_OrderProcesses
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH SourceRows AS
    (
        SELECT
            i.OrderProcessId,
            i.OrderId,
            i.ProcessId,
            o.Company,
            o.DefineObjectId,
            o.CreatedBy,
            ps.ProcessStepId,
            obj.RecNo AS DefineObjectRecNo,
            obj.NameObject,
            obj.CodeObject,
            bc.BarCode,
            ISNULL(q.TotalQty, 0) AS TotalQty
        FROM inserted i
        JOIN dbo.Tbl_Lns_Order o
            ON o.OrderId = i.OrderId
        OUTER APPLY
        (
            SELECT TOP (1) s.ProcessStepId
            FROM dbo.Tbl_Wfw_ProcessStep s
            WHERE s.ProcessId = i.ProcessId
              AND ISNULL(s.IsActive, 1) = 1
            ORDER BY s.OrderId, s.ProcessStepId
        ) ps
        LEFT JOIN dbo.Tbl_Clr_DefineObject obj
            ON obj.DefineObjectId = o.DefineObjectId
           AND (obj.Company = o.Company OR o.Company IS NULL OR obj.Company = 3)
        OUTER APPLY
        (
            SELECT TOP (1) b.BarCode
            FROM dbo.Tbl_Clr_ObjectBarCode b
            WHERE b.RNObject = obj.RecNo
              AND (b.Company = o.Company OR o.Company IS NULL OR b.Company = 3)
            ORDER BY b.Priority, b.RecNo
        ) bc
        OUTER APPLY
        (
            SELECT SUM(CASE WHEN ISNULL(oi.Quantity, 0) > 0 THEN oi.Quantity ELSE 0 END) AS TotalQty
            FROM dbo.Tbl_Lns_OrderItem oi
            WHERE oi.OrderId = o.OrderId
        ) q
        WHERE ISNULL(o.IndexDocument, 0) = 5001
          AND o.DefineObjectId IS NOT NULL
    )
    INSERT INTO dbo.Tbl_Wfw_OrderRawMaterial
    (
        OrderId,
        OrderProcessId,
        ProcessStepId,
        DefineObjectId,
        DefineObjectRecNo,
        Barcode,
        MaterialName,
        Quantity,
        DateCreate,
        DateUpdate,
        CreatedBy,
        ModifiedBy,
        WasterRNObject,
        ProdRNObject
    )
    SELECT
        s.OrderId,
        s.OrderProcessId,
        s.ProcessStepId,
        s.DefineObjectId,
        s.DefineObjectRecNo,
        COALESCE(NULLIF(s.BarCode, ''''), NULLIF(s.CodeObject, '''')),
        s.NameObject,
        CASE WHEN s.TotalQty > 0 THEN s.TotalQty ELSE 1 END,
        GETDATE(),
        GETDATE(),
        s.CreatedBy,
        s.CreatedBy,
        NULL,
        s.DefineObjectRecNo
    FROM SourceRows s
    WHERE s.ProcessStepId IS NOT NULL
      AND s.DefineObjectRecNo IS NOT NULL
      AND NOT EXISTS
      (
          SELECT 1
          FROM dbo.Tbl_Wfw_OrderRawMaterial r
          WHERE r.OrderProcessId = s.OrderProcessId
            AND r.ProdRNObject = s.DefineObjectRecNo
      );
END;
');
";

            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}
