
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
    /// There are no comments for TblLnsTempConfiguration in the schema.
    /// </summary>
    public partial class TblLnsTempConfiguration : IEntityTypeConfiguration<TblLnsTemp>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsTemp> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsTemp> builder)
        {
            builder.ToTable(@"Tbl_Lns_Temp", @"dbo");
            builder.Property(x => x.C000).HasColumnName(@"C000").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C025).HasColumnName(@"C025").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C050).HasColumnName(@"C050").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C075).HasColumnName(@"C075").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C100).HasColumnName(@"C100").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C125).HasColumnName(@"C125").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C150).HasColumnName(@"C150").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C175).HasColumnName(@"C175").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C200).HasColumnName(@"C200").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C225).HasColumnName(@"C225").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C250).HasColumnName(@"C250").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C275).HasColumnName(@"C275").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C300).HasColumnName(@"C300").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C325).HasColumnName(@"C325").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C350).HasColumnName(@"C350").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C375).HasColumnName(@"C375").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C400).HasColumnName(@"C400").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C425).HasColumnName(@"C425").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C450).HasColumnName(@"C450").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C475).HasColumnName(@"C475").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C500).HasColumnName(@"C500").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C525).HasColumnName(@"C525").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C550).HasColumnName(@"C550").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C575).HasColumnName(@"C575").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.C600).HasColumnName(@"C600").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.TempId).HasColumnName(@"TempId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd().HasPrecision(19, 0);
            builder.Property(x => x.TempRow).HasColumnName(@"TempRow").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"TempId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsTemp> builder);

        #endregion
    }

}
