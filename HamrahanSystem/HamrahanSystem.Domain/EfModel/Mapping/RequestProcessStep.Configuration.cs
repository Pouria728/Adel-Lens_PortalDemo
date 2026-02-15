

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
    /// There are no comments for RequestProcessStepConfiguration in the schema.
    /// </summary>
    public partial class RequestProcessStepConfiguration : IEntityTypeConfiguration<RequestProcessStep>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<RequestProcessStep> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<RequestProcessStep> builder)
        {
            builder.ToTable(@"RequestProcessSteps", @"dbo");
            builder.Property(x => x.Datecreate).HasColumnName(@"Datecreate").HasColumnType(@"datetime").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RequestProcessId).HasColumnName(@"RequestProcessId").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RequestProcessStepId).HasColumnName(@"RequestProcessStepId").HasColumnType(@"uniqueidentifier").IsRequired().HasDefaultValueSql("NEWID()"); 
            builder.Property(x => x.RoleId).HasColumnName(@"RoleId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"RequestProcessStepId");
            builder.HasOne(x => x.RequestProcess).WithMany(op => op.RequestProcessSteps).HasForeignKey(@"RequestProcessId").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<RequestProcessStep> builder);

        #endregion
    }

}
