

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
    /// There are no comments for TblLnsOrderserviceConfiguration in the schema.
    /// </summary>
    public partial class TblLnsOrderserviceConfiguration : IEntityTypeConfiguration<TblLnsOrderservice>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsOrderservice> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsOrderservice> builder)
        {
            builder.ToTable(@"Tbl_Lns_OrderServices", @"dbo");
            builder.Property(x => x.DefineServiceId).HasColumnName(@"DefineServiceId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"bigint").ValueGeneratedNever().HasPrecision(19, 0);
            builder.Property(x => x.OrderServicesId).HasColumnName(@"orderServicesId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd().HasPrecision(19, 0);
            builder.HasKey(@"OrderServicesId");
            builder.HasOne(x => x.TblLnsOrder).WithMany(op => op.TblLnsOrderservices).HasForeignKey(@"OrderId").IsRequired(false);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsOrderservice> builder);

        #endregion
    }

}
