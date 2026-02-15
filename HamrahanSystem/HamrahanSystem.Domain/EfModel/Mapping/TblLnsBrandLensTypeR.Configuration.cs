

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
    /// There are no comments for TblLnsBrandLensTypeRConfiguration in the schema.
    /// </summary>
    public partial class TblLnsBrandLensTypeRConfiguration : IEntityTypeConfiguration<TblLnsBrandLensTypeR>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsBrandLensTypeR> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsBrandLensTypeR> builder)
        {
            builder.ToTable(@"Tbl_Lns_BrandLensTypeR", @"dbo");
            builder.Property(x => x.BrandId).HasColumnName(@"BrandId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.BrandLensTypeRId).HasColumnName(@"BrandLensTypeRId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.LensTypeRId).HasColumnName(@"LensTypeRId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.HasKey(@"BrandLensTypeRId");
            builder.HasOne(x => x.TblLnsBrand).WithMany(op => op.TblLnsBrandLensTypeRs).HasForeignKey(@"BrandId").IsRequired(false);
            builder.HasOne(x => x.TblLnsLensTypeR).WithMany(op => op.TblLnsBrandLensTypeRs).HasForeignKey(@"LensTypeRId").IsRequired(false);
            builder.HasMany(x => x.TblLnsLensTypeRLensIndexRs).WithOne(op => op.TblLnsBrandLensTypeR).HasForeignKey(@"BrandLensTypeRId").IsRequired(false);
            builder.HasMany(x => x.TblLnsOrders).WithOne(op => op.TblLnsBrandLensTypeR).HasForeignKey(@"BrandLensTypeRId").IsRequired(false);
            builder.HasMany(x => x.TblLnsOrderItems).WithOne(op => op.TblLnsBrandLensTypeR).HasForeignKey(@"BrandLensTypeRId").IsRequired(false);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsBrandLensTypeR> builder);

        #endregion
    }

}
