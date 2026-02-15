

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
    /// There are no comments for TblLnsLensIndexMaterialTypeConfiguration in the schema.
    /// </summary>
    public partial class TblLnsLensIndexMaterialTypeConfiguration : IEntityTypeConfiguration<TblLnsLensIndexMaterialType>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsLensIndexMaterialType> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsLensIndexMaterialType> builder)
        {
            builder.ToTable(@"Tbl_Lns_LensIndexMaterialType", @"dbo");
            builder.Property(x => x.DefineObjectId).HasColumnName(@"DefineObjectId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DesignTypeLensIndexId).HasColumnName(@"DesignTypeLensIndexId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.LensIndexMaterialTypeId).HasColumnName(@"LensIndexMaterialTypeId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.MaterialTypeId).HasColumnName(@"MaterialTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"LensIndexMaterialTypeId");
            builder.HasOne(x => x.TblLnsDesignTypeLensIndex).WithMany(op => op.TblLnsLensIndexMaterialTypes).HasForeignKey(@"DesignTypeLensIndexId").IsRequired(false);
            builder.HasOne(x => x.TblLnsMaterialType).WithMany(op => op.TblLnsLensIndexMaterialTypes).HasForeignKey(@"MaterialTypeId").IsRequired(false);
            builder.HasMany(x => x.TblLnsOrders).WithOne(op => op.TblLnsLensIndexMaterialType).HasForeignKey(@"LensIndexMaterialTypeId").IsRequired(false);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsLensIndexMaterialType> builder);

        #endregion
    }

}
