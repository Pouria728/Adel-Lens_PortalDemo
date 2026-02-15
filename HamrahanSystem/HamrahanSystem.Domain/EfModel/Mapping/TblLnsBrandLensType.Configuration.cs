
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
    /// There are no comments for TblLnsBrandLensTypeConfiguration in the schema.
    /// </summary>
    public partial class TblLnsBrandLensTypeConfiguration : IEntityTypeConfiguration<TblLnsBrandLensType>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsBrandLensType> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsBrandLensType> builder)
        {
            builder.ToTable(@"Tbl_Lns_BrandLensType", @"dbo");
            builder.Property(x => x.BrandId).HasColumnName(@"BrandId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.BrandLensTypeId).HasColumnName(@"brandLensTypeId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.LensTypeId).HasColumnName(@"LensTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.HasKey(@"BrandLensTypeId");
            builder.HasMany(x => x.TblLnsBrandDesignTypes).WithOne(op => op.TblLnsBrandLensType).HasForeignKey(@"BrandLensTypeId").IsRequired(false);
            builder.HasOne(x => x.TblLnsLensType).WithMany(op => op.TblLnsBrandLensTypes).HasForeignKey(@"LensTypeId").IsRequired(false);
            builder.HasOne(x => x.TblLnsBrand).WithMany(op => op.TblLnsBrandLensTypes).HasForeignKey(@"BrandId").IsRequired(false);
            builder.HasMany(x => x.TblLnsOrders).WithOne(op => op.TblLnsBrandLensType).HasForeignKey(@"BrandLensTypeId").IsRequired(false);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsBrandLensType> builder);

        #endregion
    }

}
