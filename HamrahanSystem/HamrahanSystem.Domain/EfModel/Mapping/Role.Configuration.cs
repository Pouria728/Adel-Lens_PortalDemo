
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
    public partial class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(@"Roles", @"dbo");
            builder.Property(x => x.RoleId).HasColumnName(@"RoleId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.RoleKey).HasColumnName(@"RoleKey").HasColumnType(@"nvarchar(100)").ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.RoleName).HasColumnName(@"RoleName").HasColumnType(@"nvarchar(100)").IsRequired().ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.HasKey(@"RoleId");
            builder.HasMany(x => x.RolePermissions).WithOne(op => op.Role).HasForeignKey(@"RoleId").IsRequired(true);
            builder.HasMany(x => x.RoleToRoles_RoleId).WithOne(op => op.Role_RoleId).HasForeignKey(@"RoleId").IsRequired(true);
            builder.HasMany(x => x.RoleToRoles_RolesId).WithOne(op => op.Role_RolesId).HasForeignKey(@"RolesId").IsRequired(true);
            builder.HasMany(x => x.UserRoles).WithOne(op => op.Role).HasForeignKey(@"RoleId").IsRequired(true);
			builder.HasMany(x => x.TblWfwRoleStepes).WithOne(op => op.Role).HasForeignKey(@"RoleId").IsRequired(true);

			CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<Role> builder);

        #endregion
    }

}
