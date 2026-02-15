

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
    /// There are no comments for TblLnsCylConfiguration in the schema.
    /// </summary>
    public partial class TblLnsCylConfiguration : IEntityTypeConfiguration<TblLnsCyl>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsCyl> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsCyl> builder)
        {
            builder.ToTable(@"Tbl_Lns_Cyl", @"dbo");
            builder.Property(x => x.Code).HasColumnName(@"Code").HasColumnType(@"nvarchar(30)").ValueGeneratedNever().HasMaxLength(30);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CreateDate).HasColumnName(@"CreateDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CylId).HasColumnName(@"CylId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.HasKey(@"CylId");
            builder.HasMany(x => x.TblLnsOrderItems).WithOne(op => op.TblLnsCyl).HasForeignKey(@"CylId").IsRequired(false);
            builder.HasMany(x => x.TblLnsSphCyls).WithOne(op => op.TblLnsCyl).HasForeignKey(@"CylId").IsRequired(false);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsCyl> builder);

        #endregion
    }

}
