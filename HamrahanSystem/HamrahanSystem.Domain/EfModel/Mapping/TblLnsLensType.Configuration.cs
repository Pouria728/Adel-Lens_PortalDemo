

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
    /// There are no comments for TblLnsLensTypeConfiguration in the schema.
    /// </summary>
    public partial class TblLnsLensTypeConfiguration : IEntityTypeConfiguration<TblLnsLensType>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsLensType> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsLensType> builder)
        {
            builder.ToTable(@"Tbl_Lns_LensType", @"dbo");
            builder.Property(x => x.Code).HasColumnName(@"Code").HasColumnType(@"nvarchar(30)").ValueGeneratedNever().HasMaxLength(30);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.BrandId).HasColumnName(@"BrandId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CreateDate).HasColumnName(@"CreateDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.IsCorridor).HasColumnName(@"IsCorridor").HasColumnType(@"bit").IsRequired().ValueGeneratedNever().HasDefaultValueSql(@"0");
            builder.Property(x => x.IsSpecial).HasColumnName(@"IsSpecial").HasColumnType(@"bit").IsRequired().ValueGeneratedNever().HasDefaultValueSql(@"0");
            builder.Property(x => x.LensTypeId).HasColumnName(@"LensTypeId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.HasKey(@"LensTypeId");
            builder.HasMany(x => x.TblLnsBrandLensTypes).WithOne(op => op.TblLnsLensType).HasForeignKey(@"LensTypeId").IsRequired(false);
            builder.HasMany(x => x.TblLnsOrders).WithOne(op => op.TblLnsLensType).HasForeignKey(@"LensTypeId").IsRequired(false);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsLensType> builder);

        #endregion
    }

}
