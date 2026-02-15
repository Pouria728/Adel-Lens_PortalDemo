
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
    /// There are no comments for TblLnsCustomLensIndexConfiguration in the schema.
    /// </summary>
    public partial class TblLnsCustomLensIndexConfiguration : IEntityTypeConfiguration<TblLnsCustomLensIndex>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsCustomLensIndex> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsCustomLensIndex> builder)
        {
            builder.ToTable(@"Tbl_Lns_CustomLensIndex", @"dbo");
            builder.Property(x => x.CustomLensIndexId).HasColumnName(@"CustomLensIndexId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.DesignTypeId).HasColumnName(@"DesignTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.LensIndexName).HasColumnName(@"LensIndexName").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.ColoringTypeStatus).HasColumnName(@"ColoringTypeStatus").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10).HasDefaultValueSql(@"N'ندارد'");
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0).HasDefaultValueSql(@"1");

            builder.HasKey(@"CustomLensIndexId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsCustomLensIndex> builder);

        #endregion
    }

}
