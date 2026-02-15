
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
    /// There are no comments for TblInfoCustomerConfiguration in the schema.
    /// </summary>
    public partial class TblInfoCustomerConfiguration : IEntityTypeConfiguration<TblInfoCustomer>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblInfoCustomer> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblInfoCustomer> builder)
        {
            builder.ToTable(@"Tbl_InfoCustomer", @"Sal");
            builder.Property(x => x.Address).HasColumnName(@"Address").HasColumnType(@"nvarchar(500)").IsRequired().ValueGeneratedNever().HasMaxLength(500);
            builder.Property(x => x.BirthDate).HasColumnName(@"BirthDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.BirthPlace).HasColumnName(@"BirthPlace").HasColumnType(@"nvarchar(500)").ValueGeneratedNever().HasMaxLength(500);
            builder.Property(x => x.BranchCode).HasColumnName(@"BranchCode").HasColumnType(@"nvarchar(20)").ValueGeneratedNever().HasMaxLength(20).HasDefaultValueSql(@"N''");
            builder.Property(x => x.BranchName).HasColumnName(@"BranchName").HasColumnType(@"nvarchar(100)").ValueGeneratedNever().HasMaxLength(100).HasDefaultValueSql(@"N''");
            builder.Property(x => x.Code).HasColumnName(@"Code").HasColumnType(@"nvarchar(15)").IsRequired().ValueGeneratedNever().HasMaxLength(15);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.Email).HasColumnName(@"Email").HasColumnType(@"nvarchar(100)").IsRequired().ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.ExportDate).HasColumnName(@"ExportDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.ExportPlace).HasColumnName(@"ExportPlace").HasColumnType(@"nvarchar(500)").ValueGeneratedNever().HasMaxLength(500);
            builder.Property(x => x.FatherName).HasColumnName(@"FatherName").HasColumnType(@"nvarchar(200)").ValueGeneratedNever().HasMaxLength(200);
            builder.Property(x => x.Fax).HasColumnName(@"Fax").HasColumnType(@"nvarchar(50)").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.ID).HasColumnName(@"ID").HasColumnType(@"nvarchar(15)").ValueGeneratedNever().HasMaxLength(15);
            builder.Property(x => x.InfoCustomerId).HasColumnName(@"InfoCustomerId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.MailBox).HasColumnName(@"MailBox").HasColumnType(@"nvarchar(40)").IsRequired().ValueGeneratedNever().HasMaxLength(40);
            builder.Property(x => x.Mobile).HasColumnName(@"Mobile").HasColumnType(@"nvarchar(50)").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.Original).HasColumnName(@"Original").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.PostalCode).HasColumnName(@"PostalCode").HasColumnType(@"nvarchar(40)").IsRequired().ValueGeneratedNever().HasMaxLength(40);
            builder.Property(x => x.RecNo).HasColumnName(@"RecNo").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RefCity).HasColumnName(@"RefCity").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RefCustomer).HasColumnName(@"RefCustomer").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RefPath).HasColumnName(@"RefPath").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RefProvince).HasColumnName(@"RefProvince").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RNContry).HasColumnName(@"RNContry").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Tableau).HasColumnName(@"Tableau").HasColumnType(@"nvarchar(100)").ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.Tel).HasColumnName(@"Tel").HasColumnType(@"nvarchar(50)").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.Transport).HasColumnName(@"Transport").HasColumnType(@"bit").ValueGeneratedNever();
            builder.HasKey(@"RecNo", @"Company");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblInfoCustomer> builder);

        #endregion
    }

}
