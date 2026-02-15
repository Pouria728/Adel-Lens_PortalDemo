

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HamrahanSystem.Domain.Entity
{
    /// <summary>
    /// There are no comments for TblClrDefineObjectConfiguration in the schema.
    /// </summary>
    public partial class TblClrDefineObjectConfiguration : IEntityTypeConfiguration<TblClrDefineObject>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblClrDefineObject> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblClrDefineObject> builder)
        {
            builder.ToTable(@"Tbl_Clr_DefineObject", @"dbo");
			
			builder.Property(x => x.AceNick).HasColumnName(@"AceNick").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"0");
            builder.Property(x => x.AceOrdered).HasColumnName(@"AceOrdered").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"0");
            builder.Property(x => x.Active).HasColumnName(@"Active").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.ActiveExDate).HasColumnName(@"ActiveExDate").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.ActivePrDate).HasColumnName(@"ActivePrDate").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.ActiveTolerance).HasColumnName(@"ActiveTolerance").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.AmountOrdered).HasColumnName(@"AmountOrdered").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"0");
            builder.Property(x => x.CodeObject).HasColumnName(@"CodeObject").HasColumnType(@"nvarchar(40)").IsRequired().ValueGeneratedNever().HasMaxLength(40);
            builder.Property(x => x.CodeSpecialObject).HasColumnName(@"CodeSpecialObject").HasColumnType(@"nvarchar(200)").ValueGeneratedNever().HasMaxLength(200);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.DefineObjectId).HasColumnName(@"DefineObjectId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.DesObject).HasColumnName(@"DesObject").HasColumnType(@"nvarchar(2000)").ValueGeneratedNever().HasMaxLength(2000);
            builder.Property(x => x.Each).HasColumnName(@"Each").HasColumnType(@"float").ValueGeneratedNever().HasPrecision(53);
            builder.Property(x => x.Equal).HasColumnName(@"Equal").HasColumnType(@"float").ValueGeneratedNever().HasPrecision(53);
            builder.Property(x => x.Height).HasColumnName(@"Height").HasColumnType(@"float").ValueGeneratedNever().HasPrecision(53);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.LatinNameObject).HasColumnName(@"LatinNameObject").HasColumnType(@"nvarchar(100)").ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.Length).HasColumnName(@"Length").HasColumnType(@"float").ValueGeneratedNever().HasPrecision(53);
            builder.Property(x => x.MaxBacklog).HasColumnName(@"MaxBacklog").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"0");
            builder.Property(x => x.MinBacklog).HasColumnName(@"MinBacklog").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"0");
            builder.Property(x => x.NameObject).HasColumnName(@"NameObject").HasColumnType(@"nvarchar(2000)").ValueGeneratedNever().HasMaxLength(2000);
            builder.Property(x => x.NationalCode).HasColumnName(@"NationalCode").HasColumnType(@"varchar(50)").ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.Pic).HasColumnName(@"Pic").HasColumnType(@"varbinary(max)").ValueGeneratedNever();
            builder.Property(x => x.RecNo).HasColumnName(@"RecNo").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RefMaster).HasColumnName(@"RefMaster").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RegistryKey).HasColumnName(@"RegistryKey").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RnGroup).HasColumnName(@"RnGroup").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RnKind).HasColumnName(@"RnKind").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RNScruple).HasColumnName(@"RNScruple").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RNSecScruple).HasColumnName(@"RNSecScruple").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Scale).HasColumnName(@"Scale").HasColumnType(@"float").ValueGeneratedNever().HasPrecision(53);
            builder.Property(x => x.Serial).HasColumnName(@"Serial").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.Standard).HasColumnName(@"Standard").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.StylePrice).HasColumnName(@"StylePrice").HasColumnType(@"tinyint").ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.TechnicalSpecs).HasColumnName(@"TechnicalSpecs").HasColumnType(@"nvarchar(50)").ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.Tolerance).HasColumnName(@"Tolerance").HasColumnType(@"tinyint").ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.ToleranceOut).HasColumnName(@"ToleranceOut").HasColumnType(@"tinyint").ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.TTMSGoodsType).HasColumnName(@"TTMS_GoodsType").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);

            builder.Property(x => x.Width).HasColumnName(@"Width").HasColumnType(@"float").ValueGeneratedNever().HasPrecision(53);
			builder.HasKey(@"DefineObjectId");
			builder.HasMany(x => x.TblLnsSphCyls).WithOne(op => op.TblClrDefineObject).HasForeignKey(@"DefineObjectId").IsRequired(false);
			builder.HasMany(x => x.TblLnsOrderItems).WithOne(op => op.TblClrDefineObject).HasForeignKey(@"DefineObjectId").IsRequired(false);

			//builder.HasIndex(@"CodeObject", @"Company").IsUnique(true).HasDatabaseName(@"IX_Tbl_Clr_DefineObject");
			//builder.HasIndex(@"DefineObjectId").IsUnique(true);

			CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblClrDefineObject> builder);

        #endregion
    }

}
