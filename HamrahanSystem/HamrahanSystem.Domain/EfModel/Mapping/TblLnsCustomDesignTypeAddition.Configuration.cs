
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
    /// There are no comments for TblLnsCustomDesignTypeAdditionConfiguration in the schema.
    /// </summary>
    public partial class TblLnsCustomDesignTypeAdditionConfiguration : IEntityTypeConfiguration<TblLnsCustomDesignTypeAddition>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsCustomDesignTypeAddition> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsCustomDesignTypeAddition> builder)
        {
            builder.ToTable(@"Tbl_Lns_CustomDesignTypeAddition", @"dbo");
            builder.Property(x => x.CustomDesignTypeAdditionId).HasColumnName(@"CustomDesignTypeAdditionId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.DesignTypeId).HasColumnName(@"DesignTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DefineObjectId).HasColumnName(@"DefineObjectId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.AdditionValue).HasColumnName(@"AdditionValue").HasColumnType(@"decimal(18,2)").ValueGeneratedNever().HasPrecision(18, 2);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0).HasDefaultValueSql(@"1");

            builder.HasKey(@"CustomDesignTypeAdditionId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsCustomDesignTypeAddition> builder);

        #endregion
    }
}
