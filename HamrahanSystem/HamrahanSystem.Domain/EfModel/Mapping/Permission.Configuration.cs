
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
    public partial class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable(@"Permissions", @"dbo");
            builder.Property(x => x.PermissionId).HasColumnName(@"PermissionId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.PermissionKey).HasColumnName(@"PermissionKey").HasColumnType(@"nvarchar(50)").ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.Title).HasColumnName(@"Title").HasColumnType(@"nvarchar(50)").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            builder.HasKey(@"PermissionId");
            builder.HasMany(x => x.UserPermissions).WithOne(op => op.Permission).HasForeignKey(@"PermissionId").IsRequired(true);
            builder.HasMany(x => x.RolePermissions).WithOne(op => op.Permission).HasForeignKey(@"PermissionId").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<Permission> builder);

        #endregion
    }

}
