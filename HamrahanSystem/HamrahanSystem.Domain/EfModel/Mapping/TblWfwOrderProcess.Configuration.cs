

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
  
    public partial class TblWfwOrderProcessConfiguration : IEntityTypeConfiguration<TblWfwOrderProcess>
    {
       
        public void Configure(EntityTypeBuilder<TblWfwOrderProcess> builder)
        {
            builder.ToTable(@"Tbl_wfw_OrderProcesses", @"dbo");
            builder.ToTable(tb => tb.UseSqlOutputClause(false));
            builder.Property(x => x.DateComplete).HasColumnName(@"DateComplete").HasColumnType(@"datetime").ValueGeneratedNever();
            builder.Property(x => x.DateCreate).HasColumnName(@"DateCreate").HasColumnType(@"datetime").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"bigint").IsRequired().ValueGeneratedNever().HasPrecision(19, 0);
            builder.Property(x => x.OrderProcessId).HasColumnName(@"OrderProcessId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd().HasPrecision(19, 0);
            builder.Property(x => x.ProcessId).HasColumnName(@"ProcessId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"OrderProcessId");
            builder.HasOne(x => x.TblWfwProcess).WithMany(op => op.TblWfwOrderProcesses).HasForeignKey(@"ProcessId").IsRequired(true);
            builder.HasOne(x => x.TblLnsOrder).WithMany(op => op.TblWfwOrderProcesses).HasForeignKey(@"OrderId").IsRequired(true);
            builder.HasMany(x => x.TblWfwOrderProcessSteps).WithOne(op => op.TblWfwOrderProcess).HasForeignKey(@"OrderProcessId").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblWfwOrderProcess> builder);

        #endregion
    }

}
