

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
    /// There are no comments for RoleToRoleConfiguration in the schema.
    /// </summary>
    public partial class RoleToRoleConfiguration : IEntityTypeConfiguration<RoleToRole>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<RoleToRole> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<RoleToRole> builder)
        {
            builder.ToTable(@"RoleToRoles", @"dbo");
            builder.Property(x => x.RoleId).HasColumnName(@"RoleId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RolesId).HasColumnName(@"RolesId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RoleToRolesId).HasColumnName(@"RoleToRolesId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd().HasPrecision(19, 0);
            builder.HasKey(@"RoleToRolesId");
            builder.HasOne(x => x.Role_RoleId).WithMany(op => op.RoleToRoles_RoleId).HasForeignKey(@"RoleId").IsRequired(true);
            builder.HasOne(x => x.Role_RolesId).WithMany(op => op.RoleToRoles_RolesId).HasForeignKey(@"RolesId").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<RoleToRole> builder);

        #endregion
    }

}
