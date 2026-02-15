

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
    /// There are no comments for TblDefineServiceConfiguration in the schema.
    /// </summary>
    public partial class TblDefineServiceConfiguration : IEntityTypeConfiguration<TblDefineService>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblDefineService> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblDefineService> builder)
        {
            builder.ToTable(@"Tbl_DefineService", @"Sal");
            builder.Property(x => x.Active).HasColumnName(@"Active").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.DefineServiceId).HasColumnName(@"DefineServiceId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.KindService).HasColumnName(@"KindService").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"2");
            builder.Property(x => x.RecNo).HasColumnName(@"RecNo").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RNScruple).HasColumnName(@"RNScruple").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ServiceCode).HasColumnName(@"ServiceCode").HasColumnType(@"nvarchar(30)").IsRequired().ValueGeneratedNever().HasMaxLength(30);
            builder.Property(x => x.ServiceName).HasColumnName(@"ServiceName").HasColumnType(@"nvarchar(150)").ValueGeneratedNever().HasMaxLength(150);
            builder.Property(x => x.ServiceNameEN).HasColumnName(@"ServiceNameEN").HasColumnType(@"nvarchar(100)").IsRequired().ValueGeneratedNever().HasMaxLength(100);
            builder.HasKey(@"RecNo", @"Company");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblDefineService> builder);

        #endregion
    }

}
