

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
    /// There are no comments for TblWfwUserCartableConfiguration in the schema.
    /// </summary>
    public partial class TblWfwUserCartableConfiguration : IEntityTypeConfiguration<TblWfwUserCartable>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblWfwUserCartable> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblWfwUserCartable> builder)
        {
            builder.ToTable(@"Tbl_Wfw_UserCartable", @"dbo");
            builder.Property(x => x.Active).HasColumnName(@"Active").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.CartableId).HasColumnName(@"CartableId").HasColumnType(@"bigint").ValueGeneratedNever().HasPrecision(19, 0);
            builder.Property(x => x.Paraph).HasColumnName(@"Paraph").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.UpdateDate).HasColumnName(@"UpdateDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.UserCartableId).HasColumnName(@"UserCartableId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd().HasPrecision(19, 0);
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"UserCartableId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblWfwUserCartable> builder);

        #endregion
    }

}
