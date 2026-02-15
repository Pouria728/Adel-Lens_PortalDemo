

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
    /// There are no comments for TblLnsSphCylConfiguration in the schema.
    /// </summary>
    public partial class TblLnsSphCylConfiguration : IEntityTypeConfiguration<TblLnsSphCyl>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsSphCyl> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsSphCyl> builder)
        {
            builder.ToTable(@"Tbl_Lns_SphCyl", @"dbo");
            builder.Property(x => x.CylId).HasColumnName(@"CylId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DefineObjectId).HasColumnName(@"DefineObjectId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.LensIndexRSphId).HasColumnName(@"LensIndexRSphId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.SphCylId).HasColumnName(@"SphCylId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.Stock).HasColumnName(@"Stock").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"SphCylId");
            builder.HasMany(x => x.TblLnsOrderItems).WithOne(op => op.TblLnsSphCyl).HasForeignKey(@"SphCylId").IsRequired(false);
            builder.HasOne(x => x.TblLnsCyl).WithMany(op => op.TblLnsSphCyls).HasForeignKey(@"CylId").IsRequired(false);
			builder.HasOne(x => x.TblClrDefineObject).WithMany(op => op.TblLnsSphCyls).HasForeignKey(@"DefineObjectId").IsRequired(false);
			builder.HasOne(x => x.TblLnsLensIndexRSph).WithMany(op => op.TblLnsSphCyls).HasForeignKey(@"LensIndexRSphId").IsRequired(false);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsSphCyl> builder);

        #endregion
    }

}
