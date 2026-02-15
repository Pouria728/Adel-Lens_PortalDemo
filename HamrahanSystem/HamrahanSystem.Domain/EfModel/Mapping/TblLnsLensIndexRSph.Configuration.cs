

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
    /// There are no comments for TblLnsLensIndexRSphConfiguration in the schema.
    /// </summary>
    public partial class TblLnsLensIndexRSphConfiguration : IEntityTypeConfiguration<TblLnsLensIndexRSph>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsLensIndexRSph> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsLensIndexRSph> builder)
        {
            builder.ToTable(@"Tbl_Lns_LensIndexRSph", @"dbo");
            builder.Property(x => x.LensIndexRSphId).HasColumnName(@"LensIndexRSphId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.LensTypeRLensIndexRId).HasColumnName(@"LensTypeRLensIndexRId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.SphId).HasColumnName(@"SphId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"LensIndexRSphId");
            builder.HasOne(x => x.TblLnsSph).WithMany(op => op.TblLnsLensIndexRSphs).HasForeignKey(@"SphId").IsRequired(false);
            builder.HasOne(x => x.TblLnsLensTypeRLensIndexR).WithMany(op => op.TblLnsLensIndexRSphs).HasForeignKey(@"LensTypeRLensIndexRId").IsRequired(false);
            builder.HasMany(x => x.TblLnsOrderItems).WithOne(op => op.TblLnsLensIndexRSph).HasForeignKey(@"LensIndexRSphId").IsRequired(false);
            builder.HasMany(x => x.TblLnsSphCyls).WithOne(op => op.TblLnsLensIndexRSph).HasForeignKey(@"LensIndexRSphId").IsRequired(false);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsLensIndexRSph> builder);

        #endregion
    }

}
