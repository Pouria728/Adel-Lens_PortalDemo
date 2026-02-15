

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    /// <summary>
    /// There are no comments for RequestConfiguration in the schema.
    /// </summary>
    public partial class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<Request> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.ToTable(@"Requests", @"dbo");
            builder.Property(x => x.BaseRequestId).HasColumnName(@"BaseRequestId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DateCreate).HasColumnName(@"DateCreate").HasColumnType(@"datetime").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RequestId).HasColumnName(@"RequestId").HasColumnType(@"uniqueidentifier").IsRequired().HasDefaultValueSql("NEWID()"); 
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"RequestId");
            builder.HasMany(x => x.RequestFieldes).WithOne(op => op.Request).HasForeignKey(@"RequestId").IsRequired(true);
            builder.HasMany(x => x.RequestProcesses).WithOne(op => op.Request).HasForeignKey(@"RequestId").IsRequired(true);
            builder.HasOne(x => x.BaseRequeste).WithMany(op => op.Requests).HasForeignKey(@"BaseRequestId").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<Request> builder);

        #endregion
    }

}
