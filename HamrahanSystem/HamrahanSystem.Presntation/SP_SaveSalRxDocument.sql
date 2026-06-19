-- Run this script against your target AdelLens-compatible SQL Server database.
GO
/****** Object:  StoredProcedure [dbo].[SP_SaveSalRxDocument]    Script Date: 1405/01/24 09:08:17 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_SaveSalRxDocument]
    @DocumentDate    Varchar(10), -- تاریخ شمسی
    @PriorForm       INT,         -- شماره یا RecNo سند مبنا
    @RefRecno        INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE
        @SerialNo                INT,
        @Reserve                 INT,
        @RefCustomer             INT,
        @RefAddress              INT,
        @RecNo                   INT,
        @RefSeller               INT,
        @RefGroupCustomer        INT,
        @IndexDocument           INT,
        @StartFiscalYear         Varchar(10),
        @EndFiscalYear           NVarchar(10),
        @Automatic               BIT,
        @RefPriorForm            INT,
        @DaysNo                  INT,
        @DocumentNo              INT = NULL,
        @Company                 TINYINT = 3,
        @UserID                  INT = 1,
        @RefProcess              INT = NULL,
        @IndexSell               INT = 3,
        @ExternalSale            INT = 0,
        @IndexBelong             INT,
        @IndexRcptMethod         INT = 1,
        @IndexStatus             INT = 2,
        @IndexDeliveryStatus     INT = 2,
        @PreAmount               FLOAT = NULL,
        @RefForm                 INT = 5,
        @CodeCustomer            VARCHAR(30),
        @CodeCellar              VARCHAR(30),
        @CodeProject             VARCHAR(30),
        @RefSaleCenter           INT,
        @RefSaleMethod           INT,
        @RefDeliverySite         INT,
        @NextDRecNo              INT;

    BEGIN TRY
        -- سال مالی
        SELECT TOP (1)
            @StartFiscalYear = StartFiscalYear,
            @EndFiscalYear = EndFiscalYear
        FROM Tbl_Main_DefineFiscalYear
        WHERE Company = @Company
          AND @DocumentDate BETWEEN StartFiscalYear AND EndFiscalYear;

        IF @StartFiscalYear IS NULL
        BEGIN
            THROW 51000, N'سال مالی متناظر با تاریخ سند پیدا نشد.', 1;
        END

        -- پیدا کردن RecNo سند مبنا:
        -- هم ورودی RecNo را می پذیرد هم DocumentNo را.
        SELECT TOP (1) @RefPriorForm = h.RecNo
        FROM Sal.Tbl_HeadDocument h
        WHERE h.Company = @Company
          AND (h.RecNo = @PriorForm OR h.DocumentNo = @PriorForm)
        ORDER BY
            CASE WHEN h.RecNo = @PriorForm THEN 0 ELSE 1 END,
            h.RecNo DESC;

        IF @RefPriorForm IS NULL
        BEGIN
            THROW 51001, N'سند مبنای پیش فاکتور پیدا نشد.', 1;
        END

        -- اطلاعات هدر از سند مبنا
        SELECT TOP (1)
            @IndexBelong = IndexBelong,
            @CodeCustomer = RefCustomer,
            @CodeCellar = RefCellar,
            @CodeProject = RefProject,
            @RefSaleCenter = RefSaleCenter,
            @RefSaleMethod = RefSaleMethod,
            @RefDeliverySite = RefDeliverySite,
            @RefSeller = RefSeller,
            @RefProcess = RefProcess,
            @RefCustomer = RefCustomer,
            @IndexDocument = IndexDocument,
            @RefGroupCustomer = RefGroupCustomer
        FROM Sal.Tbl_HeadDocument
        WHERE Company = @Company
          AND RecNo = @RefPriorForm;

        -- مقصد: فاکتور فروش
        SET @IndexDocument = 3001;

        -- اگر RefProcess مبنا نامعتبر بود، از اولین فرآیند معتبر شرکت استفاده کن.
        IF @RefProcess IS NULL
           OR NOT EXISTS (
                SELECT 1
                FROM Sal.Tbl_DefineProcess p
                WHERE p.Company = @Company
                  AND p.RecNo = @RefProcess
           )
        BEGIN
            SELECT TOP (1) @RefProcess = p.RecNo
            FROM Sal.Tbl_DefineProcess p
            WHERE p.Company = @Company
            ORDER BY p.RecNo;
        END

        IF @RefProcess IS NULL
        BEGIN
            THROW 51002, N'فرآیند معتبر برای ثبت فاکتور پیدا نشد.', 1;
        END

        SELECT TOP (1) @Automatic = Automatic
        FROM Sal.Tbl_DocumentNo
        WHERE Company = @Company
          AND RefForm = @RefForm
        ORDER BY StartFiscalYear DESC;

        IF ISNULL(@Automatic, 1) = 1 OR ISNULL(@DocumentNo, 0) < 1
        BEGIN
            SELECT @DocumentNo = MAX(T1.DocumentNo)
            FROM Sal.Tbl_HeadDocument T1
            LEFT JOIN Sal.Tbl_DocumentNo T2
                ON T2.Company = T1.Company
               AND T2.RefForm = T1.RefForm
            WHERE T1.Company = @Company
              AND T1.RefForm = @RefForm
              AND (ISNULL(T2.SaleCenter, 0) = 0 OR T1.RefSaleCenter = @RefSaleCenter)
              AND (ISNULL(T2.SaleMethod, 0) = 0 OR T1.RefSaleMethod = @RefSaleMethod)
              AND (ISNULL(T2.Process, 0) = 0 OR T1.RefProcess = @RefProcess)
              AND (ISNULL(T2.FiscalYear, 0) = 0 OR T1.FiscalYear = @StartFiscalYear);

            SET @DocumentNo = ISNULL(@DocumentNo, 0) + 1;
        END

        BEGIN TRAN;

        -- شماره های روزانه/سریال با قفل مناسب برای جلوگیری از تداخل همزمان
        SELECT @DaysNo = ISNULL(MAX(DayNo), 0) + 1
        FROM Sal.Tbl_HeadDocument WITH (UPDLOCK, HOLDLOCK)
        WHERE Company = @Company
          AND FiscalYear = @StartFiscalYear
          AND DocumentDate = @DocumentDate
          AND RefForm = @RefForm
          AND RefProcess = @RefProcess;

        SELECT @SerialNo = ISNULL(MAX(SerialNo), 0) + 1
        FROM Sal.Tbl_HeadDocument WITH (UPDLOCK, HOLDLOCK)
        WHERE Company = @Company
          AND FiscalYear = @StartFiscalYear
          AND RefProcess = @RefProcess
          AND RefForm = @RefForm;

        SET @Reserve = 0;
        SELECT @Reserve = ReserveStatus
        FROM Sal.Tbl_DefineProcess
        WHERE Company = @Company
          AND RecNo = @RefProcess
          AND RefFormRsv = @RefForm
          AND ReserveStatus = 1;

        SELECT TOP (1) @RefAddress = RecNo
        FROM Sal.Tbl_InfoCustomer
        WHERE RefCustomer = ISNULL(@RefCustomer, @CodeCustomer)
          AND Original = 1;

        EXEC Main.SP_GeneratePK @Company, 'Sal', 'Tbl_HeadDocument', @RecNo OUTPUT;
        IF @RecNo IS NULL OR @RecNo <= 0
        BEGIN
            THROW 51003, N'کلید هدر فاکتور تولید نشد.', 1;
        END

        SET @RefRecno = @RecNo;

        INSERT INTO Sal.Tbl_HeadDocument
        (
            Company, RecNo, DayNo, DocumentNo, SerialNo, FinalNo, DetectNumber,
            FiscalYear, IndexDocument, DocumentDate, RefForm, RefProcess, RefCustomer, RefGroupCustomer,
            RefDeliverySite, RefSaleMethod, RefSaleCenter, IndexBelong, IndexSell, RefAddress, ExternalSale, IndexRcptMethod,
            RefSeller, RefCellar, RefProject, IndexStatus, IssuantID, IndexDeliveryStatus, PreAmount, PreAmountTot, Reserve, RefPriorForm
        )
        SELECT
            @Company,
            @RecNo,
            @DaysNo,
            @DocumentNo,
            @SerialNo,
            CAST(@DocumentNo AS NVARCHAR(10)),
            @DocumentNo,
            @StartFiscalYear,
            @IndexDocument,
            @DocumentDate,
            @RefForm,
            @RefProcess,
            @CodeCustomer,
            @RefGroupCustomer,
            @RefDeliverySite,
            @RefSaleMethod,
            @RefSaleCenter,
            @IndexBelong,
            @IndexSell,
            @RefAddress,
            @ExternalSale,
            @IndexRcptMethod,
            @RefSeller,
            @CodeCellar,
            @CodeProject,
            @IndexStatus,
            @UserID,
            @IndexDeliveryStatus,
            @PreAmount,
            @PreAmount,
            @Reserve,
            @RefPriorForm;

        -- تولید بازه جدید DRecNo برای جلوگیری از تداخل PK
        SELECT @NextDRecNo = ISNULL(MAX(d.DRecNo), 0)
        FROM Sal.Tbl_DetailDocument d WITH (UPDLOCK, HOLDLOCK)
        WHERE d.Company = @Company;

        ;WITH PriorDetails AS
        (
            SELECT
                ROW_NUMBER() OVER (ORDER BY d.[Row], d.DRecNo) AS Rn,
                d.[Row],
                d.RNBarCode,
                d.RNCellar,
                d.RNObject,
                d.RNScruple,
                d.Qty,
                d.ForQty,
                d.Rate,
                d.RNValuta,
                d.CurRate,
                d.DRefSaleMethod,
                d.DRefDeliverySite,
                d.DIndexRcptMethod,
                d.[Des],
                d.DIndexStatus,
                d.IsGift,
                d.KindOfService,
                d.IsGiftForDRecNo,
                d.DRefProject,
                d.SerialObject,
                d.DRefSeller,
                d.DRefSaleCenter,
                d.CurEqualRate,
                d.DRecNo AS RecNoAdvert
            FROM Sal.Tbl_DetailDocument d
            WHERE d.Company = @Company
              AND d.RefRecNo = @RefPriorForm
        )
        INSERT INTO Sal.Tbl_DetailDocument
        (
            DRecNo, RefRecNo, Company, [Row], RNBarCode, RNCellar,
            RNObject, RNScruple, Qty, PriQty, ForQty, Rate, RNValuta,
            CurRate, DRefSaleMethod, DRefDeliverySite, DIndexRcptMethod, [Des], DIndexStatus, IsGift, KindOfService,
            IsGiftForDRecNo, DRefProject, SerialObject, DRefSeller, DRefSaleCenter, CurEqualRate, RecNoAdvert
        )
        SELECT
            @NextDRecNo + pd.Rn,
            @RefRecno,
            @Company,
            pd.[Row],
            pd.RNBarCode,
            pd.RNCellar,
            pd.RNObject,
            pd.RNScruple,
            pd.Qty,
            pd.Qty,
            pd.ForQty,
            pd.Rate,
            pd.RNValuta,
            pd.CurRate,
            pd.DRefSaleMethod,
            pd.DRefDeliverySite,
            pd.DIndexRcptMethod,
            pd.[Des],
            pd.DIndexStatus,
            pd.IsGift,
            pd.KindOfService,
            pd.IsGiftForDRecNo,
            pd.DRefProject,
            pd.SerialObject,
            pd.DRefSeller,
            pd.DRefSaleCenter,
            pd.CurEqualRate,
            pd.RecNoAdvert
        FROM PriorDetails pd;

        EXEC [Sal].[SP_SaveDocSellar]
            @Company,
            @UserID,
            @RefRecno,
            @StartFiscalYear,
            @EndFiscalYear,
            @DocumentNo,
            1,
            1,
            0;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END
