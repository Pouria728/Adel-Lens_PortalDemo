

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
    /// There are no comments for TblWfwProcessStepConfiguration in the schema.
    /// </summary>
    public partial class TblWfwProcessStepConfiguration : IEntityTypeConfiguration<TblWfwProcessStep>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblWfwProcessStep> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblWfwProcessStep> builder)
        {
            builder.ToTable(@"Tbl_Wfw_ProcessStep", @"dbo");
            builder.Property(x => x.AllowNextStep).HasColumnName(@"AllowNextStep").HasColumnType(@"bit").IsRequired().ValueGeneratedNever().HasDefaultValueSql(@"0");
            builder.Property(x => x.Code).HasColumnName(@"Code").HasColumnType(@"nvarchar(50)").ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CreateDate).HasColumnName(@"CreateDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.IsPrint).HasColumnName(@"IsPrint").HasColumnType(@"bit").IsRequired().ValueGeneratedNever().HasDefaultValueSql(@"0");
            builder.Property(x => x.Leyout).HasColumnName(@"Leyout").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType(@"nvarchar(100)").ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.ProcessId).HasColumnName(@"ProcessId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ProcessStepId).HasColumnName(@"ProcessStepId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
			builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
			builder.Property(x => x.ProcedureId).HasColumnName(@"ProcedureId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"0");
			builder.Property(x => x.IsBarcode).HasColumnName(@"IsBarcode").HasColumnType(@"bit").IsRequired().ValueGeneratedNever().HasDefaultValueSql(@"0");
			builder.HasMany(x => x.TblWfwRoleStepes).WithOne(op => op.TblWfwProcessStep).HasForeignKey(@"ProcessStepId").IsRequired(true);
			builder.HasOne(x => x.TblWfwProcess).WithMany(op => op.TblWfwProcessSteps).HasForeignKey(@"ProcessId").IsRequired(true);
			builder.HasMany(x => x.TblWfwOrderProcessSteps).WithOne(op => op.TblWfwProcessStep).HasForeignKey(@"ProcessStepId").IsRequired(true);
			builder.HasKey(@"ProcessStepId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblWfwProcessStep> builder);

        #endregion
    }

}
