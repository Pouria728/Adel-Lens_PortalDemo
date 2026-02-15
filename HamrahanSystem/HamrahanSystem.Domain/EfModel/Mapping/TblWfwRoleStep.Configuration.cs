

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
    /// There are no comments for TblWfwRoleStepConfiguration in the schema.
    /// </summary>
    public partial class TblWfwRoleStepConfiguration : IEntityTypeConfiguration<TblWfwRoleStep>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblWfwRoleStep> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblWfwRoleStep> builder)
        {
            builder.ToTable(@"Tbl_Wfw_RoleStep", @"dbo");
            builder.Property(x => x.ProcessStepId).HasColumnName(@"ProcessStepId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RoleId).HasColumnName(@"RoleId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RoleStepId).HasColumnName(@"RoleStepId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
			builder.HasOne(x => x.Role).WithMany(op => op.TblWfwRoleStepes).HasForeignKey(@"RoleId").IsRequired(true);
			builder.HasOne(x => x.TblWfwProcessStep).WithMany(op => op.TblWfwRoleStepes).HasForeignKey(@"ProcessStepId").IsRequired(true);
			builder.HasKey(@"RoleStepId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblWfwRoleStep> builder);

        #endregion
    }

}
