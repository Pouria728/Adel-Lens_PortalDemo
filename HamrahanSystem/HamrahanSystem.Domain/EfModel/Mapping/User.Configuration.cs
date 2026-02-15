

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
    /// There are no comments for UserConfiguration in the schema.
    /// </summary>
    public partial class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<User> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(@"Users", @"dbo");
            builder.Property(x => x.AllowForceSend).HasColumnName(@"AllowForceSend").HasColumnType(@"bit").IsRequired().ValueGeneratedNever().HasDefaultValueSql(@"0");
            builder.Property(x => x.CompanyId).HasColumnName(@"CompanyId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"3");
            builder.Property(x => x.DisplayName).HasColumnName(@"DisplayName").HasColumnType(@"nvarchar(100)").IsRequired().ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.Email).HasColumnName(@"Email").HasColumnType(@"nvarchar(100)").ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType(@"nvarchar(100)").ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.FiscalYear).HasColumnName(@"FiscalYear").HasColumnType(@"nvarchar(10)").HasMaxLength(10);
            builder.Property(x => x.InfoCustomerId).HasColumnName(@"InfoCustomerId").HasColumnType(@"int").HasPrecision(10, 0);
            builder.Property(x => x.InsertDate).HasColumnName(@"InsertDate").HasColumnType(@"datetime").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.InsertUserId).HasColumnName(@"InsertUserId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.LastDirectoryUpdate).HasColumnName(@"LastDirectoryUpdate").HasColumnType(@"datetime").ValueGeneratedNever();
            builder.Property(x => x.LastName).HasColumnName(@"LastName").HasColumnType(@"nvarchar(100)").HasMaxLength(100);
            builder.Property(x => x.Mobile).HasColumnName(@"Mobile").HasColumnType(@"nvarchar(14)").HasMaxLength(14);
            builder.Property(x => x.MobilePhoneNumber).HasColumnName(@"MobilePhoneNumber").HasColumnType(@"nvarchar(20)").HasMaxLength(20);
            builder.Property(x => x.MobilePhoneVerified).HasColumnName(@"MobilePhoneVerified").HasColumnType(@"bit").IsRequired().ValueGeneratedNever().HasDefaultValueSql(@"0");
            builder.Property(x => x.NationalCode).HasColumnName(@"NationalCode").HasColumnType(@"nvarchar(10)").HasMaxLength(10);
            builder.Property(x => x.PasswordHash).HasColumnName(@"PasswordHash").HasColumnType(@"nvarchar(86)").IsRequired().ValueGeneratedNever().HasMaxLength(86);
            builder.Property(x => x.PasswordSalt).HasColumnName(@"PasswordSalt").HasColumnType(@"nvarchar(10)").IsRequired().ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.Source).HasColumnName(@"Source").HasColumnType(@"nvarchar(4)").IsRequired().ValueGeneratedNever().HasMaxLength(4);
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.TwoFactorAuth).HasColumnName(@"TwoFactorAuth").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.UpdateDate).HasColumnName(@"UpdateDate").HasColumnType(@"datetime");
            builder.Property(x => x.UpdateUserId).HasColumnName(@"UpdateUserId").HasColumnType(@"int").HasPrecision(10, 0);
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.UserImage).HasColumnName(@"UserImage").HasColumnType(@"nvarchar(100)").HasMaxLength(100);
            builder.Property(x => x.Username).HasColumnName(@"Username").HasColumnType(@"nvarchar(100)").IsRequired().ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.WinId).HasColumnName(@"WinId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
			builder.Property(x => x.IsAdmin).HasColumnName(@"IsAdmin").HasColumnType(@"bit");
			builder.HasKey(@"UserId");
            builder.HasMany(x => x.UserPermissions).WithOne(op => op.User).HasForeignKey(@"UserId").IsRequired(true);
            builder.HasMany(x => x.UserRoles).WithOne(op => op.User).HasForeignKey(@"UserId").IsRequired(true);
			builder.HasMany(x => x.TblWfwOrderProcessSteps).WithOne(op => op.User).HasForeignKey(@"UserId").IsRequired(true);

			CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<User> builder);

        #endregion
    }

}
