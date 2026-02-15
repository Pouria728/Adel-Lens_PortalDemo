
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
    /// There are no comments for RolePermissionConfiguration in the schema.
    /// </summary>
    public partial class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<RolePermission> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable(@"RolePermissions", @"dbo");
            builder.Property(x => x.PermissionKey).HasColumnName(@"PermissionKey").HasColumnType(@"nvarchar(100)").IsRequired().ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.RoleId).HasColumnName(@"RoleId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RolePermissionId).HasColumnName(@"RolePermissionId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd().HasPrecision(19, 0);
            builder.HasKey(@"RolePermissionId");
            builder.HasIndex(@"PermissionKey", @"RoleId").IsUnique(true).HasDatabaseName(@"UQ_RolePerm_RoleId_PermKey");
            builder.HasOne(x => x.Role).WithMany(op => op.RolePermissions).HasForeignKey(@"RoleId").IsRequired(true);
			builder.HasOne(x => x.Permission).WithMany(op => op.RolePermissions).HasForeignKey(@"PermissionId").IsRequired(true);

			CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<RolePermission> builder);

        #endregion
    }

}
