

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
    /// There are no comments for BaseRequestFieldedConfiguration in the schema.
    /// </summary>
    public partial class BaseRequestFieldConfiguration : IEntityTypeConfiguration<BaseRequestField>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<BaseRequestFielded> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<BaseRequestField> builder)
        {
            builder.ToTable(@"BaseRequestFieldes", @"dbo");
            builder.Property(x => x.AllowNull).HasColumnName(@"AllowNull").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.BaseRequestFieldId).HasColumnName(@"BaseRequestFieldId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.BaseRequestId).HasColumnName(@"BaseRequestId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.BaseRequestParentFieldId).HasColumnName(@"BaseRequestParentFieldId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DataTypeId).HasColumnName(@"DataTypeId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.FieldType).HasColumnName(@"FieldType").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.MaxRecord).HasColumnName(@"MaxRecord").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Title).HasColumnName(@"Title").HasColumnType(@"nvarchar(100)").IsRequired().ValueGeneratedNever().HasMaxLength(100);
            builder.HasKey(@"BaseRequestFieldId");
            builder.HasMany(x => x.BaseRequestFieldes_BaseRequestParentFieldId).WithOne(op => op.BaseRequestFielde_BaseRequestParentFieldId).HasForeignKey(@"BaseRequestParentFieldId").IsRequired(false);
            builder.HasOne(x => x.BaseRequestFielde_BaseRequestParentFieldId).WithMany(op => op.BaseRequestFieldes_BaseRequestParentFieldId).HasForeignKey(@"BaseRequestParentFieldId").IsRequired(false);
            builder.HasOne(x => x.BaseRequeste).WithMany(op => op.BaseRequestFieldes).HasForeignKey(@"BaseRequestId").IsRequired(true);
            builder.HasMany(x => x.RequestFieldes).WithOne(op => op.BaseRequestFielded).HasForeignKey(@"BaseRequestFieldId").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<BaseRequestField> builder);

        #endregion
    }

}
