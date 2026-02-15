

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
    /// There are no comments for TblLnsBrandCoatingConfiguration in the schema.
    /// </summary>
    public partial class TblLnsBrandCoatingConfiguration : IEntityTypeConfiguration<TblLnsBrandCoating>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsBrandCoating> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsBrandCoating> builder)
        {
            builder.ToTable(@"Tbl_Lns_BrandCoating", @"dbo");
            builder.Property(x => x.BrandCoatingId).HasColumnName(@"BrandCoatingId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.BrandId).HasColumnName(@"BrandId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CoatingId).HasColumnName(@"CoatingId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.IsDefault).HasColumnName(@"IsDefault").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.HasKey(@"BrandCoatingId");
            builder.HasOne(x => x.TblLnsBrand).WithMany(op => op.TblLnsBrandCoatings).HasForeignKey(@"BrandId").IsRequired(false);
            builder.HasOne(x => x.TblLnsCoating).WithMany(op => op.TblLnsBrandCoatings).HasForeignKey(@"CoatingId").IsRequired(false);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsBrandCoating> builder);

        #endregion
    }

}
