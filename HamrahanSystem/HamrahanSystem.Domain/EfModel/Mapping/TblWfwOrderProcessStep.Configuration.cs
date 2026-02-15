

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
    /// There are no comments for TblWfwOrderProcessStepConfiguration in the schema.
    /// </summary>
    public partial class TblWfwOrderProcessStepConfiguration : IEntityTypeConfiguration<TblWfwOrderProcessStep>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblWfwOrderProcessStep> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblWfwOrderProcessStep> builder)
        {
            builder.ToTable(@"Tbl_wfw_OrderProcessSteps", @"dbo");
            builder.ToTable(tb => tb.UseSqlOutputClause(false));
            builder.Property(x => x.DateComplete).HasColumnName(@"DateComplete").HasColumnType(@"datetime").ValueGeneratedNever();
            builder.Property(x => x.DateCreate).HasColumnName(@"DateCreate").HasColumnType(@"datetime").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.OrderProcessId).HasColumnName(@"OrderProcessId").HasColumnType(@"bigint").IsRequired().ValueGeneratedNever().HasPrecision(19, 0);
            builder.Property(x => x.OrderProcessStepId).HasColumnName(@"OrderProcessStepId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd().HasPrecision(19, 0);
            builder.Property(x => x.ProcessStepId).HasColumnName(@"ProcessStepId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"OrderProcessStepId");
            builder.HasOne(x => x.TblWfwOrderProcess).WithMany(op => op.TblWfwOrderProcessSteps).HasForeignKey(@"OrderProcessId").IsRequired(true);
            builder.HasOne(x => x.User).WithMany(op => op.TblWfwOrderProcessSteps).HasForeignKey(@"UserId").IsRequired(false);
            builder.HasOne(x => x.TblWfwProcessStep).WithMany(op => op.TblWfwOrderProcessSteps).HasForeignKey(@"ProcessStepId").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblWfwOrderProcessStep> builder);

        #endregion
    }

}
