

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
    /// There are no comments for UserRoleConfiguration in the schema.
    /// </summary>
    public partial class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<UserRole> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable(@"UserRoles", @"dbo");
            builder.Property(x => x.RoleId).HasColumnName(@"RoleId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.UserRoleId).HasColumnName(@"UserRoleId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd().HasPrecision(19, 0);
            builder.HasKey(@"UserRoleId");
            builder.HasIndex(@"RoleId", @"UserId").IsUnique(true).HasDatabaseName(@"UQ_UserRoles_UserId_RoleId");
            builder.HasOne(x => x.User).WithMany(op => op.UserRoles).HasForeignKey(@"UserId").IsRequired(true);
            builder.HasOne(x => x.Role).WithMany(op => op.UserRoles).HasForeignKey(@"RoleId").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<UserRole> builder);

        #endregion
    }

}
