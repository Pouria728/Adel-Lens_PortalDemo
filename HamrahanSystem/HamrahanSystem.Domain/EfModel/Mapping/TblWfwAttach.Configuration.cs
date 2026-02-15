

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
    /// There are no comments for TblWfwAttachConfiguration in the schema.
    /// </summary>
    public partial class TblWfwAttachConfiguration : IEntityTypeConfiguration<TblWfwAttach>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblWfwAttach> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblWfwAttach> builder)
        {
            builder.ToTable(@"Tbl_Wfw_Attach", @"dbo");
            builder.Property(x => x.AttachId).HasColumnName(@"AttachId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd().HasPrecision(19, 0);
            builder.Property(x => x.CartableId).HasColumnName(@"CartableId").HasColumnType(@"bigint").ValueGeneratedNever().HasPrecision(19, 0);
            builder.Property(x => x.Code).HasColumnName(@"Code").HasColumnType(@"nvarchar(50)").ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CreateDate).HasColumnName(@"CreateDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.DocumentId).HasColumnName(@"DocumentId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.FileStock).HasColumnName(@"FileStock").HasColumnType(@"nvarchar(max)").ValueGeneratedNever();
            builder.Property(x => x.IndexDocument).HasColumnName(@"IndexDocument").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType(@"nvarchar(100)").ValueGeneratedNever().HasMaxLength(100);
            builder.Property(x => x.Suffix).HasColumnName(@"Suffix").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.HasKey(@"AttachId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblWfwAttach> builder);

        #endregion
    }

}
