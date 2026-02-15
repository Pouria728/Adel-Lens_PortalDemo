

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
    /// There are no comments for RequestProcessConfiguration in the schema.
    /// </summary>
    public partial class RequestProcessConfiguration : IEntityTypeConfiguration<RequestProcess>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<RequestProcess> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<RequestProcess> builder)
        {
            builder.ToTable(@"RequestProcesses", @"dbo");
            builder.Property(x => x.CreateDate).HasColumnName(@"CreateDate").HasColumnType(@"datetime").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.DateComplete).HasColumnName(@"DateComplete").HasColumnType(@"datetime").ValueGeneratedNever();
            builder.Property(x => x.RequestId).HasColumnName(@"RequestId").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RequestProcessId).HasColumnName(@"RequestProcessId").HasColumnType(@"uniqueidentifier").IsRequired().HasDefaultValueSql("NEWID()");
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"RequestProcessId");
            builder.HasOne(x => x.Request).WithMany(op => op.RequestProcesses).HasForeignKey(@"RequestId").IsRequired(true);
            builder.HasMany(x => x.RequestProcessSteps).WithOne(op => op.RequestProcess).HasForeignKey(@"RequestProcessId").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<RequestProcess> builder);

        #endregion
    }

}
