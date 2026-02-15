using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data;

namespace HamrahanSystem.Presntation.Migrations
{
	[FluentMigrator.Migration(202508211201)]
	public class _202508211201_CreateTables : FluentMigrator.Migration
	{
		public override void Up()
		{
		string s= @"

/****** Object:  Table [dbo].[RolePermissions]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolePermissions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RolePermissions](
	[RolePermissionId] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[PermissionKey] [nvarchar](100) NOT NULL,
	[PermissionId] [int] NULL,
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
(
	[RolePermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NOT NULL,
	[TenantId] [int] NOT NULL,
	[RoleKey] [nvarchar](100) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RoleToRoles]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleToRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoleToRoles](
	[RoleToRolesId] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[RolesId] [int] NOT NULL,
 CONSTRAINT [PK_RoleToRoles] PRIMARY KEY CLUSTERED 
(
	[RoleToRolesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_Brand]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Brand]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_Brand](
	[BrandId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[Name] [nvarchar](254) NULL,
	[Code] [nvarchar](30) NULL,
	[IsActive] [smallint] NULL,
	[OrderId] [int] NOT NULL,
	[IsStock] [bit] NOT NULL,
	[IsStockGranty] [bit] NOT NULL,
	[IsSpecial] [bit] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_BRAND] PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_BrandCoating]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandCoating]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_BrandCoating](
	[BrandCoatingId] [int] IDENTITY(1,1) NOT NULL,
	[CoatingId] [int] NULL,
	[BrandId] [int] NULL,
	[IsDefault] [bit] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_BRANDCOATING] PRIMARY KEY CLUSTERED 
(
	[BrandCoatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_BrandDesignType]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandDesignType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_BrandDesignType](
	[BrandDesignTypeId] [int] IDENTITY(1,1) NOT NULL,
	[brandLensTypeId] [int] NULL,
	[DesignTypeId] [int] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_BRANDDESIGNTYPE] PRIMARY KEY CLUSTERED 
(
	[BrandDesignTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_BrandLensType]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandLensType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_BrandLensType](
	[brandLensTypeId] [int] IDENTITY(1,1) NOT NULL,
	[LensTypeId] [int] NULL,
	[BrandId] [int] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_BRANDLENSTYPE] PRIMARY KEY CLUSTERED 
(
	[brandLensTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_BrandLensTypeR]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandLensTypeR]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_BrandLensTypeR](
	[BrandLensTypeRId] [int] IDENTITY(1,1) NOT NULL,
	[BrandId] [int] NULL,
	[LensTypeRId] [int] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_BRANDLENSTYPER] PRIMARY KEY CLUSTERED 
(
	[BrandLensTypeRId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_Coating]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Coating]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_Coating](
	[CoatingId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[Name] [nvarchar](254) NULL,
	[Code] [nvarchar](30) NULL,
	[IsActive] [smallint] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_COATING] PRIMARY KEY CLUSTERED 
(
	[CoatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_ColoringType]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_ColoringType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_ColoringType](
	[ColoringTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[Name] [nvarchar](254) NULL,
	[Code] [nvarchar](30) NULL,
	[IsActive] [smallint] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_COLORINGTYPE] PRIMARY KEY CLUSTERED 
(
	[ColoringTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_Cyl]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Cyl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_Cyl](
	[CylId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[Name] [nvarchar](254) NULL,
	[Code] [nvarchar](30) NULL,
	[IsActive] [smallint] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_CYL] PRIMARY KEY CLUSTERED 
(
	[CylId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_DesignType]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_DesignType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_DesignType](
	[DesignTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[Name] [nvarchar](254) NULL,
	[Code] [nvarchar](30) NULL,
	[IsActive] [smallint] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_DESIGNTYPE] PRIMARY KEY CLUSTERED 
(
	[DesignTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_DesignTypeLensIndex]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_DesignTypeLensIndex]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_DesignTypeLensIndex](
	[DesignTypeLensIndexId] [int] IDENTITY(1,1) NOT NULL,
	[BrandDesignTypeId] [int] NULL,
	[LensIndexId] [int] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_DESIGNTYPELENSINDEX] PRIMARY KEY CLUSTERED 
(
	[DesignTypeLensIndexId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_FrameType]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_FrameType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_FrameType](
	[FrameTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[Name] [nvarchar](254) NULL,
	[Code] [nvarchar](30) NULL,
	[IsActive] [smallint] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_FRAMETYPE] PRIMARY KEY CLUSTERED 
(
	[FrameTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_LensIndex]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensIndex]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_LensIndex](
	[LensIndexId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[Name] [nvarchar](254) NULL,
	[Code] [nvarchar](30) NULL,
	[IsActive] [smallint] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_LENSINDEX] PRIMARY KEY CLUSTERED 
(
	[LensIndexId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_LensIndexMaterialType]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensIndexMaterialType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_LensIndexMaterialType](
	[LensIndexMaterialTypeId] [int] IDENTITY(1,1) NOT NULL,
	[DesignTypeLensIndexId] [int] NULL,
	[DefineObjectId] [int] NULL,
	[MaterialTypeId] [int] NULL,
 CONSTRAINT [PK_TBL_LNS_LENSINDEXMATERIALTY] PRIMARY KEY CLUSTERED 
(
	[LensIndexMaterialTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_LensIndexR]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensIndexR]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_LensIndexR](
	[LensIndexRId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[Name] [nvarchar](254) NULL,
	[Code] [nvarchar](30) NULL,
	[IsActive] [smallint] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_LENSINDEXR] PRIMARY KEY CLUSTERED 
(
	[LensIndexRId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_LensIndexRSph]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensIndexRSph]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_LensIndexRSph](
	[LensIndexRSphId] [int] IDENTITY(1,1) NOT NULL,
	[SphId] [int] NULL,
	[LensTypeRLensIndexRId] [int] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_LENSINDEXRSPH] PRIMARY KEY CLUSTERED 
(
	[LensIndexRSphId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_LensType]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_LensType](
	[LensTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[Name] [nvarchar](254) NULL,
	[Code] [nvarchar](30) NULL,
	[IsActive] [smallint] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_LENSTYPE] PRIMARY KEY CLUSTERED 
(
	[LensTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_LensTypeR]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensTypeR]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_LensTypeR](
	[LensTypeRId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[Name] [nvarchar](254) NULL,
	[Code] [nvarchar](30) NULL,
	[IsActive] [smallint] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_LENSTYPER] PRIMARY KEY CLUSTERED 
(
	[LensTypeRId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_LensTypeRLensIndexR]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensTypeRLensIndexR]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_LensTypeRLensIndexR](
	[LensTypeRLensIndexRId] [int] IDENTITY(1,1) NOT NULL,
	[BrandLensTypeRId] [int] NULL,
	[LensIndexRId] [int] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_LENSTYPERLENSINDEXR] PRIMARY KEY CLUSTERED 
(
	[LensTypeRLensIndexRId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_MaterialType]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_MaterialType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_MaterialType](
	[MaterialTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[Name] [nvarchar](254) NULL,
	[Code] [nvarchar](30) NULL,
	[IsActive] [smallint] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_MATERIALTYPE] PRIMARY KEY NONCLUSTERED 
(
	[MaterialTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_Order]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_Order](
	[OrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[CreateDate] [nvarchar](26) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[DesignTypeId] [int] NULL,
	[LensIndexId] [int] NULL,
	[MaterialTypeId] [int] NULL,
	[CoatingId] [int] NULL,
	[BrandDesignTypeId] [int] NULL,
	[LensTypeId] [int] NULL,
	[LensIndexMaterialTypeId] [int] NULL,
	[BrandLensTypeRId] [int] NULL,
	[LensTypeRLensIndexRId] [int] NULL,
	[DesignTypeLensIndexId] [int] NULL,
	[DefineObjectId] [int] NULL,
	[BrandId] [int] NULL,
	[brandLensTypeId] [int] NULL,
	[Acc_DefineValutaId] [int] NULL,
	[InfoCustomerId] [int] NULL,
	[ColoringTypeId] [int] NULL,
	[DefineCustomerId] [int] NULL,
	[FrameTypeId] [int] NULL,
	[Number] [nvarchar](15) NULL,
	[HBox] [real] NULL,
	[VBox] [real] NULL,
	[Dbl] [real] NULL,
	[EffectiveDiameter] [real] NULL,
	[Panto] [real] NULL,
	[Ffa] [real] NULL,
	[ItemBase] [int] NULL,
	[Vd] [real] NULL,
	[Description] [nvarchar](254) NULL,
	[Consumer] [nvarchar](254) NULL,
	[dateMustDelivered] [nvarchar](10) NULL,
	[Price] [decimal](18, 2) NULL,
	[TrackingCode] [nvarchar](20) NULL,
	[HasColor] [bit] NULL,
	[HasCoating] [bit] NULL,
	[PrintLabel] [bit] NULL,
	[LabelLPrint] [bit] NULL,
	[LabelRPrint] [bit] NULL,
	[LabelLRPrint] [bit] NULL,
	[PrintWarranty] [bit] NULL,
	[OrderPrint] [bit] NULL,
	[WarrantyLRPrint] [bit] NULL,
	[BeforeLensType] [int] NULL,
	[BeforeLensSpec] [nvarchar](20) NULL,
	[StoreName] [nvarchar](254) NULL,
	[OrderClass] [int] NULL,
	[Corridor] [int] NULL,
	[Color] [varchar](254) NULL,
	[ColorRatio] [decimal](18, 0) NULL,
	[ColorRatioTop] [decimal](18, 0) NULL,
	[ColorBottom] [decimal](18, 0) NULL,
	[IpdL] [decimal](18, 0) NULL,
	[IpdR] [decimal](18, 0) NULL,
	[FittingL] [decimal](18, 0) NULL,
	[FittingR] [decimal](18, 0) NULL,
	[IndexDocument] [int] NULL,
	[StatusId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_ORDER] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_OrderItem]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_OrderItem](
	[OrderItemId] [bigint] IDENTITY(1,1) NOT NULL,
	[LensIndexRSphId] [int] NULL,
	[CylId] [int] NULL,
	[SphCylId] [int] NULL,
	[OrderId] [bigint] NULL,
	[SphId] [int] NULL,
	[RowNumber] [nvarchar](20) NULL,
	[Price] [decimal](18, 2) NULL,
	[Dia] [smallint] NULL,
	[Dc] [smallint] NULL,
	[Prism] [smallint] NULL,
	[BaseOfPrism] [smallint] NULL,
	[Position] [smallint] NULL,
	[Axis] [int] NULL,
	[Fitting] [real] NULL,
	[ItemAdd] [smallint] NULL,
	[Ipd] [real] NULL,
	[Cutting] [int] NULL,
	[RowVersion] [datetime] NULL,
	[CorrProductId] [bigint] NULL,
	[EyeType] [int] NULL,
	[Discount] [decimal](18, 2) NULL,
	[Quantity] [int] NULL,
	[ProvidedQuantity] [int] NULL,
	[Fee] [decimal](18, 2) NULL,
	[Description] [nvarchar](254) NULL,
	[CorrLRowNumber] [int] NULL,
	[ConsumerTitle] [nvarchar](20) NULL,
	[Optician] [nvarchar](254) NULL,
	[NeedTools] [bit] NULL,
	[IsRight] [smallint] NULL,
	[DefineObjectId] [int] NULL,
	[BrandLensTypeRId] [int] NULL,
	[LensTypeRLensIndexRId] [int] NULL,
	[BrandId] [int] NULL,
 CONSTRAINT [PK_TBL_LNS_ORDERITEM] PRIMARY KEY CLUSTERED 
(
	[OrderItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_OrderServices]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderServices]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_OrderServices](
	[orderServicesId] [bigint] IDENTITY(1,1) NOT NULL,
	[DefineServiceId] [int] NULL,
	[OrderId] [bigint] NULL,
 CONSTRAINT [PK_TBL_LNS_ORDERSERVICES] PRIMARY KEY CLUSTERED 
(
	[orderServicesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_Sph]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Sph]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_Sph](
	[SphId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[Name] [nvarchar](254) NULL,
	[Code] [nvarchar](30) NULL,
	[IsActive] [smallint] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_SPH] PRIMARY KEY CLUSTERED 
(
	[SphId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Lns_SphCyl]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_SphCyl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Lns_SphCyl](
	[SphCylId] [int] IDENTITY(1,1) NOT NULL,
	[DefineObjectId] [int] NULL,
	[LensIndexRSphId] [int] NULL,
	[CylId] [int] NULL,
	[Stock] [int] NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TBL_LNS_SPHCYL] PRIMARY KEY CLUSTERED 
(
	[SphCylId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_Attach]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_Attach]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_Attach](
	[AttachId] [bigint] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Code] [nvarchar](50) NULL,
	[IsActive] [smallint] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[ModifiedBy] [int] NULL,
	[CartableId] [bigint] NULL,
	[Suffix] [nvarchar](10) NULL,
	[FileStock] [nvarchar](max) NULL,
	[IndexDocument] [smallint] NULL,
	[DocumentId] [int] NULL,
 CONSTRAINT [PK_TBL_WFW_ATTACH] PRIMARY KEY CLUSTERED 
(
	[AttachId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_Cartable]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_Cartable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_Cartable](
	[CartableId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProcessStepId] [int] NULL,
	[IndexDocument] [smallint] NULL,
	[DocumentDate] [nvarchar](10) NULL,
	[DocumentNo] [int] NULL,
	[DocumentRecNo] [int] NULL,
	[DocumentId] [int] NULL,
	[Company] [int] NULL,
	[CreateDate] [nvarchar](10) NULL,
	[UpdateDate] [nvarchar](10) NULL,
	[DaysNo] [int] NULL,
	[AdvertNo] [int] NULL,
	[FiscalYear] [nvarchar](10) NULL,
	[UserId] [int] NULL,
	[Status] [smallint] NULL,
 CONSTRAINT [PK_TBL_WFW_CARTABLE] PRIMARY KEY CLUSTERED 
(
	[CartableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_Condition]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_Condition]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_Condition](
	[ConditionId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Code] [nvarchar](50) NULL,
	[IsActive] [smallint] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_TBL_WFW_CONDITION] PRIMARY KEY CLUSTERED 
(
	[ConditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_OrderProcesses]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcesses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_OrderProcesses](
	[OrderProcessId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[ProcessId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[DateComplete] [datetime] NULL,
 CONSTRAINT [PK_Tbl_wfw_OrderProcesses] PRIMARY KEY CLUSTERED 
(
	[OrderProcessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_OrderProcessSteps]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcessSteps]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_OrderProcessSteps](
	[OrderProcessStepId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderProcessId] [bigint] NOT NULL,
	[ProcessStepId] [int] NOT NULL,
	[UserId] [int] NULL,
	[StatusId] [int] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[DateComplete] [datetime] NULL,
 CONSTRAINT [PK_Tbl_wfw_OrderProcessSteps] PRIMARY KEY CLUSTERED 
(
	[OrderProcessStepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_Process]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_Process]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_Process](
	[ProcessId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Code] [nvarchar](50) NULL,
	[IsActive] [smallint] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[ModifiedBy] [int] NULL,
	[CodeSystem] [smallint] NULL,
	[IndexDocument] [smallint] NULL,
 CONSTRAINT [PK_TBL_WFW_PROCESS] PRIMARY KEY CLUSTERED 
(
	[ProcessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_ProcessAction]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_ProcessAction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_ProcessAction](
	[ProcessActionId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Code] [nvarchar](50) NULL,
	[IsActive] [smallint] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[ModifiedBy] [int] NULL,
	[Color] [nvarchar](254) NULL,
	[Icon] [nvarchar](254) NULL,
 CONSTRAINT [PK_TBL_WFW_PROCESSACTION] PRIMARY KEY CLUSTERED 
(
	[ProcessActionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_ProcessStep]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_ProcessStep]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_ProcessStep](
	[ProcessStepId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Code] [nvarchar](50) NULL,
	[IsActive] [smallint] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[ModifiedBy] [int] NULL,
	[ProcessId] [int] NULL,
	[Leyout] [nvarchar](254) NULL,
	[IsPrint] [bit] NOT NULL,
	[AllowNextStep] [bit] NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProcedureId] [int] NOT NULL,
	[IsBarcode] [bit] NOT NULL,
 CONSTRAINT [PK_TBL_WFW_PROCESSSTEP] PRIMARY KEY CLUSTERED 
(
	[ProcessStepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_RelationStep]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_RelationStep]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_RelationStep](
	[RelationStepId] [int] IDENTITY(1,1) NOT NULL,
	[FromProcessStepId] [int] NULL,
	[ToProcessStepId] [int] NULL,
	[StepActionId] [int] NULL,
 CONSTRAINT [PK_TBL_WFW_RELATIONSTEP] PRIMARY KEY CLUSTERED 
(
	[RelationStepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_RelationStepCondition]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_RelationStepCondition]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_RelationStepCondition](
	[RelationStepConditionId] [int] IDENTITY(1,1) NOT NULL,
	[RelationStepId] [int] NULL,
	[ConditionId] [int] NULL,
	[OkResult] [nvarchar](254) NULL,
 CONSTRAINT [PK_TBL_WFW_RELATIONSTEPCONDITI] PRIMARY KEY CLUSTERED 
(
	[RelationStepConditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_ResultStep]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_ResultStep]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_ResultStep](
	[ResultStepId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Code] [nvarchar](50) NULL,
	[IsActive] [smallint] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[ModifiedBy] [int] NULL,
	[RelationStepId] [int] NULL,
	[Command] [nvarchar](254) NULL,
	[CommandType] [smallint] NOT NULL,
 CONSTRAINT [PK_TBL_WFW_RESULTSTEP] PRIMARY KEY CLUSTERED 
(
	[ResultStepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_RoleStep]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_RoleStep]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_RoleStep](
	[RoleStepId] [int] IDENTITY(1,1) NOT NULL,
	[ProcessStepId] [int] NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_TBL_WFW_ROLESTEP] PRIMARY KEY CLUSTERED 
(
	[RoleStepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_Status]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_Status]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_Status](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[Company] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Code] [nvarchar](50) NULL,
	[IsActive] [smallint] NULL,
	[Description] [nvarchar](254) NULL,
	[CreateDate] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [nvarchar](10) NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_TBL_WFW_STATUS] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_StepAction]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_StepAction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_StepAction](
	[StepActionId] [int] IDENTITY(1,1) NOT NULL,
	[ProcessStepId] [int] NULL,
	[ProcessActionId] [int] NULL,
 CONSTRAINT [PK_TBL_WFW_STEPACTION] PRIMARY KEY CLUSTERED 
(
	[StepActionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_Wfw_UserCartable]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_UserCartable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_Wfw_UserCartable](
	[UserCartableId] [bigint] IDENTITY(1,1) NOT NULL,
	[StatusId] [int] NULL,
	[CartableId] [bigint] NULL,
	[UserId] [int] NULL,
	[Active] [smallint] NULL,
	[Paraph] [nvarchar](254) NULL,
	[UpdateDate] [nvarchar](10) NULL,
 CONSTRAINT [PK_TBL_WFW_USERCARTABLE] PRIMARY KEY CLUSTERED 
(
	[UserCartableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserPermissions]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserPermissions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserPermissions](
	[UserPermissionId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PermissionKey] [nvarchar](100) NOT NULL,
	[Granted] [bit] NOT NULL,
	[PermissionId] [int] NULL,
 CONSTRAINT [PK_UserPermissions] PRIMARY KEY CLUSTERED 
(
	[UserPermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserPreferences]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserPreferences]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserPreferences](
	[UserPreferenceId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[PreferenceType] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserPreferences] PRIMARY KEY CLUSTERED 
(
	[UserPreferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserRoles](
	[UserRoleId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1404/07/21 10:05:27 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Source] [nvarchar](4) NOT NULL,
	[PasswordHash] [nvarchar](86) NOT NULL,
	[PasswordSalt] [nvarchar](60) NULL,
	[LastDirectoryUpdate] [datetime] NULL,
	[UserImage] [nvarchar](100) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertUserId] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[IsActive] [smallint] NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[NationalCode] [nvarchar](10) NULL,
	[Mobile] [nvarchar](14) NULL,
	[WinId] [int] NULL,
	[TenantId] [int] NOT NULL,
	[CompanyId] [int] NOT NULL,
	[FiscalYear] [nvarchar](10) NULL,
	[MobilePhoneNumber] [nvarchar](20) NULL,
	[MobilePhoneVerified] [bit] NOT NULL,
	[TwoFactorAuth] [int] NULL,
	[InfoCustomerId] [int] NULL,
	[AllowForceSend] [bit] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolePermissions_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolePermissions]'))
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolePermissions_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolePermissions]'))
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleToRoles]'))
ALTER TABLE [dbo].[RoleToRoles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_RoleId] FOREIGN KEY([RolesId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleToRoles]'))
ALTER TABLE [dbo].[RoleToRoles] CHECK CONSTRAINT [FK_Roles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roles_RolesId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleToRoles]'))
ALTER TABLE [dbo].[RoleToRoles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_RolesId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Roles_RolesId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleToRoles]'))
ALTER TABLE [dbo].[RoleToRoles] CHECK CONSTRAINT [FK_Roles_RolesId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_15]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandCoating]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandCoating]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_15] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Tbl_Lns_Brand] ([BrandId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_15]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandCoating]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandCoating] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_15]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_16]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandCoating]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandCoating]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_16] FOREIGN KEY([CoatingId])
REFERENCES [dbo].[Tbl_Lns_Coating] ([CoatingId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_16]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandCoating]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandCoating] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_16]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_2]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandDesignType]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandDesignType]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_2] FOREIGN KEY([DesignTypeId])
REFERENCES [dbo].[Tbl_Lns_DesignType] ([DesignTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_2]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandDesignType]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandDesignType] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_2]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_49]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandDesignType]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandDesignType]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_49] FOREIGN KEY([brandLensTypeId])
REFERENCES [dbo].[Tbl_Lns_BrandLensType] ([brandLensTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_49]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandDesignType]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandDesignType] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_49]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_4]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandLensType]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandLensType]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_4] FOREIGN KEY([LensTypeId])
REFERENCES [dbo].[Tbl_Lns_LensType] ([LensTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_4]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandLensType]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandLensType] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_4]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_5]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandLensType]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandLensType]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_5] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Tbl_Lns_Brand] ([BrandId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_5]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandLensType]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandLensType] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_5]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_26]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandLensTypeR]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandLensTypeR]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_26] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Tbl_Lns_Brand] ([BrandId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_26]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandLensTypeR]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandLensTypeR] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_26]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_27]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandLensTypeR]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandLensTypeR]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_27] FOREIGN KEY([LensTypeRId])
REFERENCES [dbo].[Tbl_Lns_LensTypeR] ([LensTypeRId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_27]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_BrandLensTypeR]'))
ALTER TABLE [dbo].[Tbl_Lns_BrandLensTypeR] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_27]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_12]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_DesignTypeLensIndex]'))
ALTER TABLE [dbo].[Tbl_Lns_DesignTypeLensIndex]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_12] FOREIGN KEY([LensIndexId])
REFERENCES [dbo].[Tbl_Lns_LensIndex] ([LensIndexId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_12]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_DesignTypeLensIndex]'))
ALTER TABLE [dbo].[Tbl_Lns_DesignTypeLensIndex] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_12]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_43]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_DesignTypeLensIndex]'))
ALTER TABLE [dbo].[Tbl_Lns_DesignTypeLensIndex]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_43] FOREIGN KEY([BrandDesignTypeId])
REFERENCES [dbo].[Tbl_Lns_BrandDesignType] ([BrandDesignTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_43]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_DesignTypeLensIndex]'))
ALTER TABLE [dbo].[Tbl_Lns_DesignTypeLensIndex] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_43]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_44]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensIndexMaterialType]'))
ALTER TABLE [dbo].[Tbl_Lns_LensIndexMaterialType]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_44] FOREIGN KEY([DesignTypeLensIndexId])
REFERENCES [dbo].[Tbl_Lns_DesignTypeLensIndex] ([DesignTypeLensIndexId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_44]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensIndexMaterialType]'))
ALTER TABLE [dbo].[Tbl_Lns_LensIndexMaterialType] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_44]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_7]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensIndexMaterialType]'))
ALTER TABLE [dbo].[Tbl_Lns_LensIndexMaterialType]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_7] FOREIGN KEY([MaterialTypeId])
REFERENCES [dbo].[Tbl_Lns_MaterialType] ([MaterialTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_7]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensIndexMaterialType]'))
ALTER TABLE [dbo].[Tbl_Lns_LensIndexMaterialType] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_7]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_21]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensIndexRSph]'))
ALTER TABLE [dbo].[Tbl_Lns_LensIndexRSph]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_21] FOREIGN KEY([SphId])
REFERENCES [dbo].[Tbl_Lns_Sph] ([SphId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_21]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensIndexRSph]'))
ALTER TABLE [dbo].[Tbl_Lns_LensIndexRSph] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_21]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_46]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensIndexRSph]'))
ALTER TABLE [dbo].[Tbl_Lns_LensIndexRSph]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_46] FOREIGN KEY([LensTypeRLensIndexRId])
REFERENCES [dbo].[Tbl_Lns_LensTypeRLensIndexR] ([LensTypeRLensIndexRId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_46]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensIndexRSph]'))
ALTER TABLE [dbo].[Tbl_Lns_LensIndexRSph] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_46]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_18]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensTypeRLensIndexR]'))
ALTER TABLE [dbo].[Tbl_Lns_LensTypeRLensIndexR]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_18] FOREIGN KEY([LensIndexRId])
REFERENCES [dbo].[Tbl_Lns_LensIndexR] ([LensIndexRId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_18]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensTypeRLensIndexR]'))
ALTER TABLE [dbo].[Tbl_Lns_LensTypeRLensIndexR] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_18]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_45]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensTypeRLensIndexR]'))
ALTER TABLE [dbo].[Tbl_Lns_LensTypeRLensIndexR]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_45] FOREIGN KEY([BrandLensTypeRId])
REFERENCES [dbo].[Tbl_Lns_BrandLensTypeR] ([BrandLensTypeRId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_45]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_LensTypeRLensIndexR]'))
ALTER TABLE [dbo].[Tbl_Lns_LensTypeRLensIndexR] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_45]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_28]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_28] FOREIGN KEY([FrameTypeId])
REFERENCES [dbo].[Tbl_Lns_FrameType] ([FrameTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_28]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_28]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_29]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_29] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Tbl_Lns_Brand] ([BrandId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_29]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_29]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_30]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_30] FOREIGN KEY([LensTypeId])
REFERENCES [dbo].[Tbl_Lns_LensType] ([LensTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_30]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_30]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_31]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_31] FOREIGN KEY([DesignTypeId])
REFERENCES [dbo].[Tbl_Lns_DesignType] ([DesignTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_31]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_31]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_32]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_32] FOREIGN KEY([LensIndexId])
REFERENCES [dbo].[Tbl_Lns_LensIndex] ([LensIndexId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_32]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_32]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_34]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_34] FOREIGN KEY([CoatingId])
REFERENCES [dbo].[Tbl_Lns_Coating] ([CoatingId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_34]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_34]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_40]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_40] FOREIGN KEY([ColoringTypeId])
REFERENCES [dbo].[Tbl_Lns_ColoringType] ([ColoringTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_40]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_40]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_48]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_48] FOREIGN KEY([MaterialTypeId])
REFERENCES [dbo].[Tbl_Lns_MaterialType] ([MaterialTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_48]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_48]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_50]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_50] FOREIGN KEY([brandLensTypeId])
REFERENCES [dbo].[Tbl_Lns_BrandLensType] ([brandLensTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_50]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_50]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_51]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_51] FOREIGN KEY([BrandDesignTypeId])
REFERENCES [dbo].[Tbl_Lns_BrandDesignType] ([BrandDesignTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_51]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_51]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_52]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_52] FOREIGN KEY([DesignTypeLensIndexId])
REFERENCES [dbo].[Tbl_Lns_DesignTypeLensIndex] ([DesignTypeLensIndexId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_52]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_52]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_53]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_53] FOREIGN KEY([LensIndexMaterialTypeId])
REFERENCES [dbo].[Tbl_Lns_LensIndexMaterialType] ([LensIndexMaterialTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_53]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_53]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_54]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_54] FOREIGN KEY([BrandLensTypeRId])
REFERENCES [dbo].[Tbl_Lns_BrandLensTypeR] ([BrandLensTypeRId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_54]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_54]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_55]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_55] FOREIGN KEY([LensTypeRLensIndexRId])
REFERENCES [dbo].[Tbl_Lns_LensTypeRLensIndexR] ([LensTypeRLensIndexRId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_55]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_Order]'))
ALTER TABLE [dbo].[Tbl_Lns_Order] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_55]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_35]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_35] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Tbl_Lns_Order] ([OrderId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_35]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_35]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_41]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_41] FOREIGN KEY([SphId])
REFERENCES [dbo].[Tbl_Lns_Sph] ([SphId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_41]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_41]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_42]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_42] FOREIGN KEY([CylId])
REFERENCES [dbo].[Tbl_Lns_Cyl] ([CylId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_42]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_42]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_56]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_56] FOREIGN KEY([LensIndexRSphId])
REFERENCES [dbo].[Tbl_Lns_LensIndexRSph] ([LensIndexRSphId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_56]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_56]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_57]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_57] FOREIGN KEY([SphCylId])
REFERENCES [dbo].[Tbl_Lns_SphCyl] ([SphCylId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_57]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_57]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_Lns_OrderItem_Tbl_Lns_Brand]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Lns_OrderItem_Tbl_Lns_Brand] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Tbl_Lns_Brand] ([BrandId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_Lns_OrderItem_Tbl_Lns_Brand]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem] CHECK CONSTRAINT [FK_Tbl_Lns_OrderItem_Tbl_Lns_Brand]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_Lns_OrderItem_Tbl_Lns_BrandLensTypeR]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Lns_OrderItem_Tbl_Lns_BrandLensTypeR] FOREIGN KEY([BrandLensTypeRId])
REFERENCES [dbo].[Tbl_Lns_BrandLensTypeR] ([BrandLensTypeRId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_Lns_OrderItem_Tbl_Lns_BrandLensTypeR]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem] CHECK CONSTRAINT [FK_Tbl_Lns_OrderItem_Tbl_Lns_BrandLensTypeR]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_Lns_OrderItem_Tbl_Lns_LensTypeRLensIndexR]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Lns_OrderItem_Tbl_Lns_LensTypeRLensIndexR] FOREIGN KEY([LensTypeRLensIndexRId])
REFERENCES [dbo].[Tbl_Lns_LensTypeRLensIndexR] ([LensTypeRLensIndexRId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_Lns_OrderItem_Tbl_Lns_LensTypeRLensIndexR]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderItem]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderItem] CHECK CONSTRAINT [FK_Tbl_Lns_OrderItem_Tbl_Lns_LensTypeRLensIndexR]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_36]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderServices]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderServices]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_36] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Tbl_Lns_Order] ([OrderId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_36]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_OrderServices]'))
ALTER TABLE [dbo].[Tbl_Lns_OrderServices] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_36]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_25]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_SphCyl]'))
ALTER TABLE [dbo].[Tbl_Lns_SphCyl]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_25] FOREIGN KEY([CylId])
REFERENCES [dbo].[Tbl_Lns_Cyl] ([CylId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_25]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_SphCyl]'))
ALTER TABLE [dbo].[Tbl_Lns_SphCyl] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_25]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_47]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_SphCyl]'))
ALTER TABLE [dbo].[Tbl_Lns_SphCyl]  WITH CHECK ADD  CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_47] FOREIGN KEY([LensIndexRSphId])
REFERENCES [dbo].[Tbl_Lns_LensIndexRSph] ([LensIndexRSphId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TBL_LNS__ASSOCIATI_TBL_LNS_47]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Lns_SphCyl]'))
ALTER TABLE [dbo].[Tbl_Lns_SphCyl] CHECK CONSTRAINT [FK_TBL_LNS__ASSOCIATI_TBL_LNS_47]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcesses_Tbl_Lns_Order]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcesses]'))
ALTER TABLE [dbo].[Tbl_Wfw_OrderProcesses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_wfw_OrderProcesses_Tbl_Lns_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Tbl_Lns_Order] ([OrderId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcesses_Tbl_Lns_Order]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcesses]'))
ALTER TABLE [dbo].[Tbl_Wfw_OrderProcesses] CHECK CONSTRAINT [FK_Tbl_wfw_OrderProcesses_Tbl_Lns_Order]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcesses_Tbl_Wfw_Process]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcesses]'))
ALTER TABLE [dbo].[Tbl_Wfw_OrderProcesses]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_wfw_OrderProcesses_Tbl_Wfw_Process] FOREIGN KEY([ProcessId])
REFERENCES [dbo].[Tbl_Wfw_Process] ([ProcessId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcesses_Tbl_Wfw_Process]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcesses]'))
ALTER TABLE [dbo].[Tbl_Wfw_OrderProcesses] CHECK CONSTRAINT [FK_Tbl_wfw_OrderProcesses_Tbl_Wfw_Process]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcessSteps_Tbl_wfw_OrderProcesses]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcessSteps]'))
ALTER TABLE [dbo].[Tbl_Wfw_OrderProcessSteps]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_wfw_OrderProcessSteps_Tbl_wfw_OrderProcesses] FOREIGN KEY([OrderProcessId])
REFERENCES [dbo].[Tbl_Wfw_OrderProcesses] ([OrderProcessId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcessSteps_Tbl_wfw_OrderProcesses]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcessSteps]'))
ALTER TABLE [dbo].[Tbl_Wfw_OrderProcessSteps] CHECK CONSTRAINT [FK_Tbl_wfw_OrderProcessSteps_Tbl_wfw_OrderProcesses]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcessSteps_Tbl_Wfw_ProcessStep]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcessSteps]'))
ALTER TABLE [dbo].[Tbl_Wfw_OrderProcessSteps]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_wfw_OrderProcessSteps_Tbl_Wfw_ProcessStep] FOREIGN KEY([ProcessStepId])
REFERENCES [dbo].[Tbl_Wfw_ProcessStep] ([ProcessStepId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcessSteps_Tbl_Wfw_ProcessStep]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcessSteps]'))
ALTER TABLE [dbo].[Tbl_Wfw_OrderProcessSteps] CHECK CONSTRAINT [FK_Tbl_wfw_OrderProcessSteps_Tbl_Wfw_ProcessStep]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcessSteps_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcessSteps]'))
ALTER TABLE [dbo].[Tbl_Wfw_OrderProcessSteps]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_wfw_OrderProcessSteps_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_wfw_OrderProcessSteps_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_Wfw_OrderProcessSteps]'))
ALTER TABLE [dbo].[Tbl_Wfw_OrderProcessSteps] CHECK CONSTRAINT [FK_Tbl_wfw_OrderProcessSteps_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPermissions_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPermissions]'))
ALTER TABLE [dbo].[UserPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserPermissions_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPermissions_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPermissions]'))
ALTER TABLE [dbo].[UserPermissions] CHECK CONSTRAINT [FK_UserPermissions_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoles]'))
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoles]'))
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoles_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoles]'))
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoles_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoles]'))
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_UserId]
GO


";


			Execute.Sql(s);
		}

		public override void Down()
		{
		}
	}
}
