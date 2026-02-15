using FluentMigrator;

namespace HamrahanSystem.Presntation.Migrations
{
    [FluentMigrator.Migration(202602011100)]
    public class _202602011100_SeedCustomLensData : FluentMigrator.Migration
    {
        public override void Up()
        {
            string s = @"
DECLARE @brand1Id INT;
DECLARE @brand2Id INT;

SELECT @brand1Id = BrandId FROM dbo.Tbl_Lns_Brand WHERE Code = 'CBR-TEST-1';
IF @brand1Id IS NULL
BEGIN
    INSERT INTO dbo.Tbl_Lns_Brand (Code, Name, Description, IsActive, IsSpecial, IsStock, IsStockGranty, OrderId)
    VALUES ('CBR-TEST-1', 'Test Brand A', 'Seeded custom brand A', 1, 1, 0, 0, 1);
    SELECT @brand1Id = SCOPE_IDENTITY();
END;

SELECT @brand2Id = BrandId FROM dbo.Tbl_Lns_Brand WHERE Code = 'CBR-TEST-2';
IF @brand2Id IS NULL
BEGIN
    INSERT INTO dbo.Tbl_Lns_Brand (Code, Name, Description, IsActive, IsSpecial, IsStock, IsStockGranty, OrderId)
    VALUES ('CBR-TEST-2', 'Test Brand B', 'Seeded custom brand B', 1, 1, 0, 0, 2);
    SELECT @brand2Id = SCOPE_IDENTITY();
END;

DECLARE @lensType1Id INT;
DECLARE @lensType2Id INT;
DECLARE @lensType3Id INT;

SELECT @lensType1Id = LensTypeId FROM dbo.Tbl_Lns_LensType WHERE Code = 'CLT-TEST-1';
IF @lensType1Id IS NULL
BEGIN
    INSERT INTO dbo.Tbl_Lns_LensType (Code, Name, Description, IsActive, IsSpecial, IsCorridor, OrderId, BrandId)
    VALUES ('CLT-TEST-1', 'Blue Cut', 'Seeded lens type 1', 1, 1, 1, 1, @brand1Id);
    SELECT @lensType1Id = SCOPE_IDENTITY();
END;

SELECT @lensType2Id = LensTypeId FROM dbo.Tbl_Lns_LensType WHERE Code = 'CLT-TEST-2';
IF @lensType2Id IS NULL
BEGIN
    INSERT INTO dbo.Tbl_Lns_LensType (Code, Name, Description, IsActive, IsSpecial, IsCorridor, OrderId, BrandId)
    VALUES ('CLT-TEST-2', 'Sun Lens', 'Seeded lens type 2', 1, 1, 0, 2, @brand1Id);
    SELECT @lensType2Id = SCOPE_IDENTITY();
END;

SELECT @lensType3Id = LensTypeId FROM dbo.Tbl_Lns_LensType WHERE Code = 'CLT-TEST-3';
IF @lensType3Id IS NULL
BEGIN
    INSERT INTO dbo.Tbl_Lns_LensType (Code, Name, Description, IsActive, IsSpecial, IsCorridor, OrderId, BrandId)
    VALUES ('CLT-TEST-3', 'Knife Cut', 'Seeded lens type 3', 1, 1, 1, 1, @brand2Id);
    SELECT @lensType3Id = SCOPE_IDENTITY();
END;

DECLARE @designType1Id INT;
DECLARE @designType2Id INT;
DECLARE @designType3Id INT;

SELECT @designType1Id = DesignTypeId FROM dbo.Tbl_Lns_DesignType WHERE Code = 'CDT-TEST-1';
IF @designType1Id IS NULL
BEGIN
    INSERT INTO dbo.Tbl_Lns_DesignType (Code, Name, Description, IsActive, IsSpecial, OrderId, LensTypeId)
    VALUES ('CDT-TEST-1', 'Design Alpha', 'Seeded design type 1', 1, 1, 1, @lensType1Id);
    SELECT @designType1Id = SCOPE_IDENTITY();
END;

SELECT @designType2Id = DesignTypeId FROM dbo.Tbl_Lns_DesignType WHERE Code = 'CDT-TEST-2';
IF @designType2Id IS NULL
BEGIN
    INSERT INTO dbo.Tbl_Lns_DesignType (Code, Name, Description, IsActive, IsSpecial, OrderId, LensTypeId)
    VALUES ('CDT-TEST-2', 'Design Beta', 'Seeded design type 2', 1, 1, 2, @lensType1Id);
    SELECT @designType2Id = SCOPE_IDENTITY();
END;

SELECT @designType3Id = DesignTypeId FROM dbo.Tbl_Lns_DesignType WHERE Code = 'CDT-TEST-3';
IF @designType3Id IS NULL
BEGIN
    INSERT INTO dbo.Tbl_Lns_DesignType (Code, Name, Description, IsActive, IsSpecial, OrderId, LensTypeId)
    VALUES ('CDT-TEST-3', 'Design Gamma', 'Seeded design type 3', 1, 1, 1, @lensType3Id);
    SELECT @designType3Id = SCOPE_IDENTITY();
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Tbl_Lns_CustomLensTypeCoating WHERE DesignTypeId = @designType1Id AND LensTypeName = 'Blue Cut' AND CoatingName = 'HC')
BEGIN
    INSERT INTO dbo.Tbl_Lns_CustomLensTypeCoating (DesignTypeId, LensTypeName, CoatingName, OrderId, IsActive)
    VALUES (@designType1Id, 'Blue Cut', 'HC', 1, 1);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.Tbl_Lns_CustomLensTypeCoating WHERE DesignTypeId = @designType1Id AND LensTypeName = 'Blue Cut' AND CoatingName = 'AR')
BEGIN
    INSERT INTO dbo.Tbl_Lns_CustomLensTypeCoating (DesignTypeId, LensTypeName, CoatingName, OrderId, IsActive)
    VALUES (@designType1Id, 'Blue Cut', 'AR', 2, 1);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.Tbl_Lns_CustomLensTypeCoating WHERE DesignTypeId = @designType2Id AND LensTypeName = 'Sun Lens' AND CoatingName = 'UV400')
BEGIN
    INSERT INTO dbo.Tbl_Lns_CustomLensTypeCoating (DesignTypeId, LensTypeName, CoatingName, OrderId, IsActive)
    VALUES (@designType2Id, 'Sun Lens', 'UV400', 1, 1);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.Tbl_Lns_CustomLensTypeCoating WHERE DesignTypeId = @designType3Id AND LensTypeName = 'Knife Cut' AND CoatingName = 'Super HC')
BEGIN
    INSERT INTO dbo.Tbl_Lns_CustomLensTypeCoating (DesignTypeId, LensTypeName, CoatingName, OrderId, IsActive)
    VALUES (@designType3Id, 'Knife Cut', 'Super HC', 1, 1);
END;
";

            Execute.Sql(s);
        }

        public override void Down()
        {
        }
    }
}
