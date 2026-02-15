

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
    /// There are no comments for TblWfwRelationStepConditionConfiguration in the schema.
    /// </summary>
    public partial class TblWfwRelationStepConditionConfiguration : IEntityTypeConfiguration<TblWfwRelationStepCondition>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblWfwRelationStepCondition> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblWfwRelationStepCondition> builder)
        {
            builder.ToTable(@"Tbl_Wfw_RelationStepCondition", @"dbo");
            builder.Property(x => x.ConditionId).HasColumnName(@"ConditionId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.OkResult).HasColumnName(@"OkResult").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.RelationStepConditionId).HasColumnName(@"RelationStepConditionId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.RelationStepId).HasColumnName(@"RelationStepId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"RelationStepConditionId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblWfwRelationStepCondition> builder);

        #endregion
    }

}
