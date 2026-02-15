

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
    /// There are no comments for TblLnsDesignTypeLensIndexConfiguration in the schema.
    /// </summary>
    public partial class TblLnsDesignTypeLensIndexConfiguration : IEntityTypeConfiguration<TblLnsDesignTypeLensIndex>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsDesignTypeLensIndex> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsDesignTypeLensIndex> builder)
        {
            builder.ToTable(@"Tbl_Lns_DesignTypeLensIndex", @"dbo");
            builder.Property(x => x.BrandDesignTypeId).HasColumnName(@"BrandDesignTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DesignTypeLensIndexId).HasColumnName(@"DesignTypeLensIndexId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.LensIndexId).HasColumnName(@"LensIndexId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.HasKey(@"DesignTypeLensIndexId");
            builder.HasOne(x => x.TblLnsLensIndex).WithMany(op => op.TblLnsDesignTypeLensIndices).HasForeignKey(@"LensIndexId").IsRequired(false);
            builder.HasOne(x => x.TblLnsBrandDesignType).WithMany(op => op.TblLnsDesignTypeLensIndices).HasForeignKey(@"BrandDesignTypeId").IsRequired(false);
            builder.HasMany(x => x.TblLnsLensIndexMaterialTypes).WithOne(op => op.TblLnsDesignTypeLensIndex).HasForeignKey(@"DesignTypeLensIndexId").IsRequired(false);
            builder.HasMany(x => x.TblLnsOrders).WithOne(op => op.TblLnsDesignTypeLensIndex).HasForeignKey(@"DesignTypeLensIndexId").IsRequired(false);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsDesignTypeLensIndex> builder);

        #endregion
    }

}
