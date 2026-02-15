

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
    /// There are no comments for TblFeeBehoofMasterConfiguration in the schema.
    /// </summary>
    public partial class TblFeeBehoofMasterConfiguration : IEntityTypeConfiguration<TblFeeBehoofMaster>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblFeeBehoofMaster> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblFeeBehoofMaster> builder)
        {
            builder.ToTable(@"Tbl_Fee_BehoofMaster", @"dbo");
            builder.Property(x => x.AgentTypeIndex).HasColumnName(@"AgentTypeIndex").HasColumnType(@"tinyint").ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.BehoofCode).HasColumnName(@"BehoofCode").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.BehoofTypeIndex).HasColumnName(@"BehoofTypeIndex").HasColumnType(@"tinyint").ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.EnglishBehoof).HasColumnName(@"EnglishBehoof").HasColumnType(@"nvarchar(30)").ValueGeneratedNever().HasMaxLength(30);
            builder.Property(x => x.FarsiBehoof).HasColumnName(@"FarsiBehoof").HasColumnType(@"nvarchar(100)").ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.INTypeIndex).HasColumnName(@"INTypeIndex").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.LatinBehoof).HasColumnName(@"LatinBehoof").HasColumnType(@"nvarchar(20)").ValueGeneratedNever().HasMaxLength(20);
            builder.Property(x => x.LinkedCodingTable).HasColumnName(@"LinkedCodingTable").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ListType).HasColumnName(@"ListType").HasColumnType(@"tinyint").ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.Param).HasColumnName(@"Param").HasColumnType(@"nvarchar(20)").ValueGeneratedNever().HasMaxLength(20);
            builder.HasKey(@"BehoofCode", @"Company");
            builder.HasMany(x => x.TblFeeBehoofDetails).WithOne(op => op.TblFeeBehoofMaster).HasForeignKey(@"BehoofCode", @"Company").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblFeeBehoofMaster> builder);

        #endregion
    }

}
