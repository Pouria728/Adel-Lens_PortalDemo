
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
    /// There are no comments for UserPermissionConfiguration in the schema.
    /// </summary>
    public partial class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<UserPermission> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable(@"UserPermissions", @"dbo");
            builder.Property(x => x.Granted).HasColumnName(@"Granted").HasColumnType(@"bit").IsRequired().ValueGeneratedNever().HasDefaultValueSql(@"1");
            builder.Property(x => x.PermissionKey).HasColumnName(@"PermissionKey").HasColumnType(@"nvarchar(100)").IsRequired().ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.UserPermissionId).HasColumnName(@"UserPermissionId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd().HasPrecision(19, 0);
            builder.HasKey(@"UserPermissionId");
            builder.HasIndex(@"PermissionKey", @"UserId").IsUnique(true).HasDatabaseName(@"UQ_UserPerm_UserId_PermKey");
            builder.HasOne(x => x.User).WithMany(op => op.UserPermissions).HasForeignKey(@"UserId").IsRequired(true);
			builder.HasOne(x => x.Permission).WithMany(op => op.UserPermissions).HasForeignKey(@"PermissionId").IsRequired(true);

			CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<UserPermission> builder);

        #endregion
    }

}
