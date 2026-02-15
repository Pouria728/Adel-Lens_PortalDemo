
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
    /// There are no comments for TblLnsCustomLensTypeMaterialConfiguration in the schema.
    /// </summary>
    public partial class TblLnsCustomLensTypeMaterialConfiguration : IEntityTypeConfiguration<TblLnsCustomLensTypeMaterial>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsCustomLensTypeMaterial> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsCustomLensTypeMaterial> builder)
        {
            builder.ToTable(@"Tbl_Lns_CustomLensTypeMaterial", @"dbo");
            builder.Property(x => x.CustomLensTypeMaterialId).HasColumnName(@"CustomLensTypeMaterialId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.DesignTypeId).HasColumnName(@"DesignTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.LensIndexId).HasColumnName(@"LensIndexId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.LensTypeName).HasColumnName(@"LensTypeName").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.MaterialName).HasColumnName(@"MaterialName").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.DefineObjectId).HasColumnName(@"DefineObjectId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0).HasDefaultValueSql(@"1");

            builder.HasKey(@"CustomLensTypeMaterialId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsCustomLensTypeMaterial> builder);

        #endregion
    }
}
