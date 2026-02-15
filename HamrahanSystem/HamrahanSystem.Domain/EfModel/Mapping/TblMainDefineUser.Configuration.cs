

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
    /// There are no comments for TblMainDefineUserConfiguration in the schema.
    /// </summary>
    public partial class TblMainDefineUserConfiguration : IEntityTypeConfiguration<TblMainDefineUser>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblMainDefineUser> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblMainDefineUser> builder)
        {
            builder.ToTable(@"Tbl_Main_DefineUser", @"dbo", x => {
              x.HasTrigger(@"Tr_Tbl_Main_DefineUser");
            });
            builder.Property(x => x.Active).HasColumnName(@"Active").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.CodePersona).HasColumnName(@"CodePersona").HasColumnType(@"nvarchar(15)").ValueGeneratedNever().HasMaxLength(15);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.IsDisableTriggers).HasColumnName(@"IsDisableTriggers").HasColumnType(@"bit").IsRequired().ValueGeneratedNever().HasDefaultValueSql(@"0");
            builder.Property(x => x.PassWord).HasColumnName(@"PassWord").HasColumnType(@"nvarchar(300)").ValueGeneratedNever().HasMaxLength(300);
            builder.Property(x => x.PassWordCheck).HasColumnName(@"PassWordCheck").HasColumnType(@"bigint").ValueGeneratedNever().HasPrecision(19, 0);
            builder.Property(x => x.PictureNo).HasColumnName(@"PictureNo").HasColumnType(@"varchar(100)").ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.RNPersona).HasColumnName(@"RNPersona").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.SQLPassword).HasColumnName(@"SQLPassword").HasColumnType(@"nvarchar(50)").ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.SQLUserName).HasColumnName(@"SQLUserName").HasColumnType(@"nvarchar(500)").ValueGeneratedNever().HasMaxLength(500);
            builder.Property(x => x.UserID).HasColumnName(@"UserID").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.UserName).HasColumnName(@"UserName").HasColumnType(@"nvarchar(30)").ValueGeneratedNever().HasMaxLength(30);
            builder.Property(x => x.WebPassword).HasColumnName(@"WebPassword").HasColumnType(@"nvarchar(50)").ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.WebServer).HasColumnName(@"WebServer").HasColumnType(@"nvarchar(50)").ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.WebUserName).HasColumnName(@"WebUserName").HasColumnType(@"nvarchar(50)").ValueGeneratedNever().HasMaxLength(50);
            builder.HasKey(@"UserID", @"Company");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblMainDefineUser> builder);

        #endregion
    }

}
