

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
    /// There are no comments for TblWfwRelationStepConfiguration in the schema.
    /// </summary>
    public partial class TblWfwRelationStepConfiguration : IEntityTypeConfiguration<TblWfwRelationStep>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblWfwRelationStep> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblWfwRelationStep> builder)
        {
            builder.ToTable(@"Tbl_Wfw_RelationStep", @"dbo");
            builder.Property(x => x.FromProcessStepId).HasColumnName(@"FromProcessStepId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RelationStepId).HasColumnName(@"RelationStepId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.StepActionId).HasColumnName(@"StepActionId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ToProcessStepId).HasColumnName(@"ToProcessStepId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"RelationStepId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblWfwRelationStep> builder);

        #endregion
    }

}
