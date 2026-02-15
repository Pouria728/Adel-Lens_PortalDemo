

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
    /// There are no comments for TblAccDefineCostCenterConfiguration in the schema.
    /// </summary>
    public partial class TblAccDefineCostCenterConfiguration : IEntityTypeConfiguration<TblAccDefineCostCenter>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblAccDefineCostCenter> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblAccDefineCostCenter> builder)
        {

            builder.ToTable(@"Tbl_Acc_DefineCostCenter", @"dbo");
            builder.Property(x => x.AccDefineCostCenterId).HasColumnName(@"AccDefineCostCenterId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.Active).HasColumnName(@"Active").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.CodeCostCenter).HasColumnName(@"CodeCostCenter").HasColumnType(@"nvarchar(30)").IsRequired().ValueGeneratedNever().HasMaxLength(30);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.IndexTypeCostCenter).HasColumnName(@"IndexTypeCostCenter").HasColumnType(@"tinyint").ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.IndexTypeTask).HasColumnName(@"IndexTypeTask").HasColumnType(@"tinyint").ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.NameCostCenter).HasColumnName(@"NameCostCenter").HasColumnType(@"nvarchar(200)").ValueGeneratedNever().HasMaxLength(200);
            builder.Property(x => x.NameCostCenterEN).HasColumnName(@"NameCostCenterEN").HasColumnType(@"nvarchar(50)").ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.RecNo).HasColumnName(@"RecNo").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RefMaster).HasColumnName(@"RefMaster").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RefMasterCompany).HasColumnName(@"RefMasterCompany").HasColumnType(@"tinyint").ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.RefMasterKey).HasColumnName(@"RefMasterKey").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RegistryKey).HasColumnName(@"RegistryKey").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RNGroupFormal).HasColumnName(@"RNGroupFormal").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.StateFormal).HasColumnName(@"StateFormal").HasColumnType(@"tinyint").ValueGeneratedNever().HasPrecision(3, 0).HasDefaultValueSql(@"4");
            builder.HasKey(@"RecNo", @"Company");
            builder.HasIndex(@"CodeCostCenter", @"Company").IsUnique(true).HasDatabaseName(@"IX_Tbl_Acc_DefineCostCenter");
            builder.HasMany(x => x.TblAccDefineCostCenters_RefMasterKey_RefMasterCompany).WithOne(op => op.TblAccDefineCostCenter_RefMasterKey_RefMasterCompany).HasForeignKey(@"RefMasterKey", @"RefMasterCompany").IsRequired(false);
            builder.HasOne(x => x.TblAccDefineCostCenter_RefMasterKey_RefMasterCompany).WithMany(op => op.TblAccDefineCostCenters_RefMasterKey_RefMasterCompany).HasForeignKey(@"RefMasterKey", @"RefMasterCompany").IsRequired(false);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblAccDefineCostCenter> builder);

        #endregion
    }

}
