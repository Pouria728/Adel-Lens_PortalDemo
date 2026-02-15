

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
    /// There are no comments for TblLnsLensTypeRLensIndexRConfiguration in the schema.
    /// </summary>
    public partial class TblLnsLensTypeRLensIndexRConfiguration : IEntityTypeConfiguration<TblLnsLensTypeRLensIndexR>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsLensTypeRLensIndexR> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsLensTypeRLensIndexR> builder)
        {
            builder.ToTable(@"Tbl_Lns_LensTypeRLensIndexR", @"dbo");
            builder.Property(x => x.BrandLensTypeRId).HasColumnName(@"BrandLensTypeRId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.LensIndexRId).HasColumnName(@"LensIndexRId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.LensTypeRLensIndexRId).HasColumnName(@"LensTypeRLensIndexRId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.HasKey(@"LensTypeRLensIndexRId");
            builder.HasMany(x => x.TblLnsLensIndexRSphs).WithOne(op => op.TblLnsLensTypeRLensIndexR).HasForeignKey(@"LensTypeRLensIndexRId").IsRequired(false);
            builder.HasOne(x => x.TblLnsLensIndexR).WithMany(op => op.TblLnsLensTypeRLensIndexRs).HasForeignKey(@"LensIndexRId").IsRequired(false);
            builder.HasOne(x => x.TblLnsBrandLensTypeR).WithMany(op => op.TblLnsLensTypeRLensIndexRs).HasForeignKey(@"BrandLensTypeRId").IsRequired(false);
            builder.HasMany(x => x.TblLnsOrders).WithOne(op => op.TblLnsLensTypeRLensIndexR).HasForeignKey(@"LensTypeRLensIndexRId").IsRequired(false);
            builder.HasMany(x => x.TblLnsOrderItems).WithOne(op => op.TblLnsLensTypeRLensIndexR).HasForeignKey(@"LensTypeRLensIndexRId").IsRequired(false);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsLensTypeRLensIndexR> builder);

        #endregion
    }

}
