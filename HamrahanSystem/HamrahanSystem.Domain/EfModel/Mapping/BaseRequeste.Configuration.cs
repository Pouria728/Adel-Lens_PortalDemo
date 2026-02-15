
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
    /// There are no comments for BaseRequesteConfiguration in the schema.
    /// </summary>
    public partial class BaseRequesteConfiguration : IEntityTypeConfiguration<BaseRequeste>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<BaseRequeste> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<BaseRequeste> builder)
        {
            builder.ToTable(@"BaseRequestes", @"dbo");
            builder.Property(x => x.BaseRequestId).HasColumnName(@"BaseRequestId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.DateCreate).HasColumnName(@"DateCreate").HasColumnType(@"datetime").ValueGeneratedNever();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Title).HasColumnName(@"Title").HasColumnType(@"nvarchar(200)").IsRequired().ValueGeneratedNever().HasMaxLength(200);
            builder.Property(x => x.TypeRequest).HasColumnName(@"TypeRequest").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"BaseRequestId");
            builder.HasMany(x => x.BaseRequestSteps).WithOne(op => op.BaseRequeste).HasForeignKey(@"BaseRequestId").IsRequired(true);
            builder.HasMany(x => x.Requests).WithOne(op => op.BaseRequeste).HasForeignKey(@"BaseRequestId").IsRequired(true);
            builder.HasMany(x => x.BaseRequestFieldes).WithOne(op => op.BaseRequeste).HasForeignKey(@"BaseRequestId").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<BaseRequeste> builder);

        #endregion
    }

}
