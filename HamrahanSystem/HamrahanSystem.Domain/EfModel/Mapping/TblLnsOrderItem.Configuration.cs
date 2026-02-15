

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
    /// There are no comments for TblLnsOrderItemConfiguration in the schema.
    /// </summary>
    public partial class TblLnsOrderItemConfiguration : IEntityTypeConfiguration<TblLnsOrderItem>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsOrderItem> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsOrderItem> builder)
        {
            builder.ToTable(@"Tbl_Lns_OrderItem", @"dbo");
            builder.Property(x => x.Axis).HasColumnName(@"Axis").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.BaseOfPrism).HasColumnName(@"BaseOfPrism").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.BrandId).HasColumnName(@"BrandId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.BrandLensTypeRId).HasColumnName(@"BrandLensTypeRId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ConsumerTitle).HasColumnName(@"ConsumerTitle").HasColumnType(@"nvarchar(20)").ValueGeneratedNever().HasMaxLength(20);
            builder.Property(x => x.CorrLRowNumber).HasColumnName(@"CorrLRowNumber").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CorrProductId).HasColumnName(@"CorrProductId").HasColumnType(@"bigint").ValueGeneratedNever().HasPrecision(19, 0);
            builder.Property(x => x.Cutting).HasColumnName(@"Cutting").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CylId).HasColumnName(@"CylId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Dc).HasColumnName(@"Dc").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.DefineObjectId).HasColumnName(@"DefineObjectId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.Dia).HasColumnName(@"Dia").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.Discount).HasColumnName(@"Discount").HasColumnType(@"decimal(18,2)").ValueGeneratedNever().HasPrecision(18, 2);
            builder.Property(x => x.EyeType).HasColumnName(@"EyeType").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Fee).HasColumnName(@"Fee").HasColumnType(@"decimal(18,2)").ValueGeneratedNever().HasPrecision(18, 2);
            builder.Property(x => x.Fitting).HasColumnName(@"Fitting").HasColumnType(@"real").ValueGeneratedNever().HasPrecision(24);
            builder.Property(x => x.Ipd).HasColumnName(@"Ipd").HasColumnType(@"real").ValueGeneratedNever().HasPrecision(24);
            builder.Property(x => x.IsRight).HasColumnName(@"IsRight").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.ItemAdd).HasColumnName(@"ItemAdd").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.LensIndexRSphId).HasColumnName(@"LensIndexRSphId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.LensTypeRLensIndexRId).HasColumnName(@"LensTypeRLensIndexRId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.NeedTools).HasColumnName(@"NeedTools").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.Optician).HasColumnName(@"Optician").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"bigint").ValueGeneratedNever().HasPrecision(19, 0);
            builder.Property(x => x.OrderItemId).HasColumnName(@"OrderItemId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd().HasPrecision(19, 0);
            builder.Property(x => x.Position).HasColumnName(@"Position").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.Price).HasColumnName(@"Price").HasColumnType(@"decimal(18,2)").ValueGeneratedNever().HasPrecision(18, 2);
            builder.Property(x => x.Prism).HasColumnName(@"Prism").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.ProvidedQuantity).HasColumnName(@"ProvidedQuantity").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Quantity).HasColumnName(@"Quantity").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.RowNumber).HasColumnName(@"RowNumber").HasColumnType(@"nvarchar(20)").ValueGeneratedNever().HasMaxLength(20);
            builder.Property(x => x.RowVersion).HasColumnName(@"RowVersion").HasColumnType(@"datetime").ValueGeneratedNever();
            builder.Property(x => x.SphCylId).HasColumnName(@"SphCylId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.SphId).HasColumnName(@"SphId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.HasKey(@"OrderItemId");
            builder.HasOne(x => x.TblLnsOrder).WithMany(op => op.TblLnsOrderItems).HasForeignKey(@"OrderId").IsRequired(false);
            builder.HasOne(x => x.TblLnsSph).WithMany(op => op.TblLnsOrderItems).HasForeignKey(@"SphId").IsRequired(false);
            builder.HasOne(x => x.TblLnsCyl).WithMany(op => op.TblLnsOrderItems).HasForeignKey(@"CylId").IsRequired(false);
            builder.HasOne(x => x.TblLnsLensIndexRSph).WithMany(op => op.TblLnsOrderItems).HasForeignKey(@"LensIndexRSphId").IsRequired(false);
            builder.HasOne(x => x.TblLnsSphCyl).WithMany(op => op.TblLnsOrderItems).HasForeignKey(@"SphCylId").IsRequired(false);
            builder.HasOne(x => x.TblLnsBrand).WithMany(op => op.TblLnsOrderItems).HasForeignKey(@"BrandId").IsRequired(false);
            builder.HasOne(x => x.TblLnsLensTypeRLensIndexR).WithMany(op => op.TblLnsOrderItems).HasForeignKey(@"LensTypeRLensIndexRId").IsRequired(false);
            builder.HasOne(x => x.TblLnsBrandLensTypeR).WithMany(op => op.TblLnsOrderItems).HasForeignKey(@"BrandLensTypeRId").IsRequired(false);
			builder.HasOne(x => x.TblClrDefineObject).WithMany(op => op.TblLnsOrderItems).HasForeignKey(@"DefineObjectId").IsRequired(false);

			CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsOrderItem> builder);

        #endregion
    }

}
