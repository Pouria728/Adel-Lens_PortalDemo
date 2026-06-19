using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [Migration(202602221930)]
    public class _202602221930_AddDefaultToCustomLensTypeCoating : Migration
    {
        public override void Up()
        {
            const string sql = @"
IF COL_LENGTH('dbo.Tbl_Lns_CustomLensTypeCoating', 'IsDefault') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tbl_Lns_CustomLensTypeCoating]
    ADD [IsDefault] BIT NULL
        CONSTRAINT [DF_Tbl_Lns_CustomLensTypeCoating_IsDefault] DEFAULT(0);
END;

IF COL_LENGTH('dbo.Tbl_Lns_CustomLensTypeCoating', 'IsDefault') IS NOT NULL
BEGIN
    EXEC(N'
    UPDATE dbo.Tbl_Lns_CustomLensTypeCoating
    SET IsDefault = ISNULL(IsDefault, 0)
    WHERE IsDefault IS NULL;
    ');

    EXEC(N'
    ;WITH duplicated_defaults AS
    (
        SELECT
            CustomLensTypeCoatingId,
            ROW_NUMBER() OVER (
                PARTITION BY DesignTypeId
                ORDER BY [OrderId], [CustomLensTypeCoatingId]
            ) AS rn
        FROM dbo.Tbl_Lns_CustomLensTypeCoating
        WHERE DesignTypeId IS NOT NULL
          AND IsDefault = 1
    )
    UPDATE t
    SET t.IsDefault = 0
    FROM dbo.Tbl_Lns_CustomLensTypeCoating t
    JOIN duplicated_defaults d
        ON d.CustomLensTypeCoatingId = t.CustomLensTypeCoatingId
    WHERE d.rn > 1;
    ');
END;

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'UX_Tbl_Lns_CustomLensTypeCoating_DesignType_Default'
      AND object_id = OBJECT_ID('dbo.Tbl_Lns_CustomLensTypeCoating')
)
BEGIN
    IF COL_LENGTH('dbo.Tbl_Lns_CustomLensTypeCoating', 'IsDefault') IS NOT NULL
    BEGIN
        EXEC(N'
        CREATE UNIQUE NONCLUSTERED INDEX [UX_Tbl_Lns_CustomLensTypeCoating_DesignType_Default]
            ON [dbo].[Tbl_Lns_CustomLensTypeCoating]([DesignTypeId])
            WHERE [IsDefault] = 1 AND [DesignTypeId] IS NOT NULL;
        ');
    END;
END;
";
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}
