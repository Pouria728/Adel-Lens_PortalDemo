

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
    /// There are no comments for TblWfwProcessConfiguration in the schema.
    /// </summary>
    public partial class TblWfwProcessConfiguration : IEntityTypeConfiguration<TblWfwProcess>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblWfwProcess> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblWfwProcess> builder)
        {
            builder.ToTable(@"Tbl_Wfw_Process", @"dbo");
            builder.Property(x => x.Code).HasColumnName(@"Code").HasColumnType(@"nvarchar(50)").ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.CodeSystem).HasColumnName(@"CodeSystem").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CreateDate).HasColumnName(@"CreateDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.IndexDocument).HasColumnName(@"IndexDocument").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType(@"nvarchar(100)").ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.ProcessId).HasColumnName(@"ProcessId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
			builder.HasMany(x => x.TblWfwProcessSteps).WithOne(op => op.TblWfwProcess).HasForeignKey(@"ProcessId").IsRequired(true);
			builder.HasMany(x => x.TblWfwOrderProcesses).WithOne(op => op.TblWfwProcess).HasForeignKey(@"ProcessId").IsRequired(true);
			builder.HasKey(@"ProcessId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblWfwProcess> builder);

        #endregion
    }

}
