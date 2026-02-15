

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
    /// There are no comments for ViewSalListCustomerConfiguration in the schema.
    /// </summary>
    public partial class ViewSalListCustomerConfiguration : IEntityTypeConfiguration<ViewSalListCustomer>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<ViewSalListCustomer> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<ViewSalListCustomer> builder)
        {
            builder.HasNoKey();
            builder.ToView(@"View_Sal_ListCustomer", @"dbo");
            builder.Property(x => x.Active).HasColumnName(@"Active").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.Caption).HasColumnName(@"Caption").HasColumnType(@"nvarchar(50)").ValueGeneratedNever().HasMaxLength(50);
            builder.Property(x => x.CodeCompany).HasColumnName(@"CodeCompany").HasColumnType(@"nvarchar(15)").IsRequired().ValueGeneratedNever().HasMaxLength(15);
            builder.Property(x => x.CodeValuta).HasColumnName(@"CodeValuta").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.DateOpen).HasColumnName(@"DateOpen").HasColumnType(@"varchar(1)").IsRequired().ValueGeneratedNever().HasMaxLength(1);
            builder.Property(x => x.DefineCustomerId).HasColumnName(@"DefineCustomerId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Expr1).HasColumnName(@"Expr1").HasColumnType(@"nvarchar(15)").IsRequired().ValueGeneratedNever().HasMaxLength(15);
            builder.Property(x => x.NameFormal).HasColumnName(@"NameFormal").HasColumnType(@"nvarchar(153)").ValueGeneratedNever().HasMaxLength(153);
            builder.Property(x => x.NameFormalEN).HasColumnName(@"NameFormalEN").HasColumnType(@"nvarchar(101)").ValueGeneratedNever().HasMaxLength(101);
            builder.Property(x => x.RecNo).HasColumnName(@"RecNo").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RefCustomer).HasColumnName(@"RefCustomer").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RefMasterCompany).HasColumnName(@"RefMasterCompany").HasColumnType(@"tinyint").ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.RefMasterKey).HasColumnName(@"RefMasterKey").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RNGroupFormal).HasColumnName(@"RNGroupFormal").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RNValuta).HasColumnName(@"RNValuta").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.SActive).HasColumnName(@"SActive").HasColumnType(@"varchar(8)").ValueGeneratedNever().HasMaxLength(8);
            builder.Property(x => x.StateFormal).HasColumnName(@"StateFormal").HasColumnType(@"tinyint").ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.TypeFormal).HasColumnName(@"TypeFormal").HasColumnType(@"nvarchar(50)").ValueGeneratedNever().HasMaxLength(50);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<ViewSalListCustomer> builder);

        #endregion
    }

}
