

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
    /// There are no comments for TblWfwStepActionConfiguration in the schema.
    /// </summary>
    public partial class TblWfwStepActionConfiguration : IEntityTypeConfiguration<TblWfwStepAction>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblWfwStepAction> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblWfwStepAction> builder)
        {
            builder.ToTable(@"Tbl_Wfw_StepAction", @"dbo");
            builder.Property(x => x.ProcessActionId).HasColumnName(@"ProcessActionId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ProcessStepId).HasColumnName(@"ProcessStepId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.StepActionId).HasColumnName(@"StepActionId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.HasKey(@"StepActionId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblWfwStepAction> builder);

        #endregion
    }

}
