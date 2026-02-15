

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
    /// There are no comments for BaseRequestStepConfiguration in the schema.
    /// </summary>
    public partial class BaseRequestStepConfiguration : IEntityTypeConfiguration<BaseRequestStep>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<BaseRequestStep> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<BaseRequestStep> builder)
        {
            builder.ToTable(@"BaseRequestSteps", @"dbo");
            builder.Property(x => x.BaseRequestId).HasColumnName(@"BaseRequestId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.BaseRequestStepId).HasColumnName(@"BaseRequestStepId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RoleId).HasColumnName(@"RoleId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Title).HasColumnName(@"Title").HasColumnType(@"nvarchar(200)").IsRequired().ValueGeneratedNever().HasMaxLength(200);
            builder.HasKey(@"BaseRequestStepId");
            builder.HasOne(x => x.BaseRequeste).WithMany(op => op.BaseRequestSteps).HasForeignKey(@"BaseRequestId").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<BaseRequestStep> builder);

        #endregion
    }

}
