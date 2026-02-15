

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
    /// There are no comments for TblLnsBrandDesignTypeConfiguration in the schema.
    /// </summary>
    public partial class TblLnsBrandDesignTypeConfiguration : IEntityTypeConfiguration<TblLnsBrandDesignType>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsBrandDesignType> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsBrandDesignType> builder)
        {
            builder.ToTable(@"Tbl_Lns_BrandDesignType", @"dbo");
            builder.Property(x => x.BrandDesignTypeId).HasColumnName(@"BrandDesignTypeId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.BrandLensTypeId).HasColumnName(@"brandLensTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DesignTypeId).HasColumnName(@"DesignTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.HasKey(@"BrandDesignTypeId");
            builder.HasOne(x => x.TblLnsDesignType).WithMany(op => op.TblLnsBrandDesignTypes).HasForeignKey(@"DesignTypeId").IsRequired(false);
            builder.HasOne(x => x.TblLnsBrandLensType).WithMany(op => op.TblLnsBrandDesignTypes).HasForeignKey(@"BrandLensTypeId").IsRequired(false);
            builder.HasMany(x => x.TblLnsDesignTypeLensIndices).WithOne(op => op.TblLnsBrandDesignType).HasForeignKey(@"BrandDesignTypeId").IsRequired(false);
            builder.HasMany(x => x.TblLnsOrders).WithOne(op => op.TblLnsBrandDesignType).HasForeignKey(@"BrandDesignTypeId").IsRequired(false);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsBrandDesignType> builder);

        #endregion
    }

}
