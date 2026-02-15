

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
    /// There are no comments for RequestFieldeConfiguration in the schema.
    /// </summary>
    public partial class RequestFieldeConfiguration : IEntityTypeConfiguration<RequestFielde>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<RequestFielde> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<RequestFielde> builder)
        {
            builder.ToTable(@"RequestFieldes", @"dbo");
            builder.Property(x => x.BaseRequestFieldId).HasColumnName(@"BaseRequestFieldId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DataValue).HasColumnName(@"DataValue").HasColumnType(@"nvarchar(200)").IsRequired().ValueGeneratedNever().HasMaxLength(200);
            builder.Property(x => x.RequestFieldId).HasColumnName(@"RequestFieldId").HasColumnType(@"uniqueidentifier").IsRequired().HasDefaultValueSql("NEWID()"); 
            builder.Property(x => x.RequestId).HasColumnName(@"RequestId").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.HasKey(@"RequestFieldId");
            builder.HasOne(x => x.Request).WithMany(op => op.RequestFieldes).HasForeignKey(@"RequestId").IsRequired(true);
            builder.HasOne(x => x.BaseRequestFielded).WithMany(op => op.RequestFieldes).HasForeignKey(@"BaseRequestFieldId").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<RequestFielde> builder);

        #endregion
    }

}
