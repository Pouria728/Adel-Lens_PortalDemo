

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
    /// There are no comments for TblWfwCartableConfiguration in the schema.
    /// </summary>
    public partial class TblWfwCartableConfiguration : IEntityTypeConfiguration<TblWfwCartable>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblWfwCartable> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblWfwCartable> builder)
        {
            builder.ToTable(@"Tbl_Wfw_Cartable", @"dbo");
            builder.Property(x => x.AdvertNo).HasColumnName(@"AdvertNo").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CartableId).HasColumnName(@"CartableId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd().HasPrecision(19, 0);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CreateDate).HasColumnName(@"CreateDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.DaysNo).HasColumnName(@"DaysNo").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DocumentDate).HasColumnName(@"DocumentDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.DocumentId).HasColumnName(@"DocumentId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DocumentNo).HasColumnName(@"DocumentNo").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DocumentRecNo).HasColumnName(@"DocumentRecNo").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.FiscalYear).HasColumnName(@"FiscalYear").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.IndexDocument).HasColumnName(@"IndexDocument").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.ProcessStepId).HasColumnName(@"ProcessStepId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Status).HasColumnName(@"Status").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.UpdateDate).HasColumnName(@"UpdateDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"CartableId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblWfwCartable> builder);

        #endregion
    }

}
