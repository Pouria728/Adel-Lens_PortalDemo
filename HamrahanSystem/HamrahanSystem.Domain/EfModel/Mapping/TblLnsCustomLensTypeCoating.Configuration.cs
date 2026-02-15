
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
    /// There are no comments for TblLnsCustomLensTypeCoatingConfiguration in the schema.
    /// </summary>
    public partial class TblLnsCustomLensTypeCoatingConfiguration : IEntityTypeConfiguration<TblLnsCustomLensTypeCoating>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsCustomLensTypeCoating> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsCustomLensTypeCoating> builder)
        {
            builder.ToTable(@"Tbl_Lns_CustomLensTypeCoating", @"dbo");
            builder.Property(x => x.CustomLensTypeCoatingId).HasColumnName(@"CustomLensTypeCoatingId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.DesignTypeId).HasColumnName(@"DesignTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.LensTypeName).HasColumnName(@"LensTypeName").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.CoatingName).HasColumnName(@"CoatingName").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0).HasDefaultValueSql(@"1");

            builder.HasKey(@"CustomLensTypeCoatingId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsCustomLensTypeCoating> builder);

        #endregion
    }

}

