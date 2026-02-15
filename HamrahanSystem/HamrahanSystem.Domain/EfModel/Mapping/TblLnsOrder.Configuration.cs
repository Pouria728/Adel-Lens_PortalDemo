

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
    /// There are no comments for TblLnsOrderConfiguration in the schema.
    /// </summary>
    public partial class TblLnsOrderConfiguration : IEntityTypeConfiguration<TblLnsOrder>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblLnsOrder> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblLnsOrder> builder)
        {
            builder.ToTable(@"Tbl_Lns_Order", @"dbo");
            builder.Property(x => x.FactorNo).HasColumnName(@"FactorNo").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.AccDefineValutaId).HasColumnName(@"Acc_DefineValutaId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.BeforeLensSpec).HasColumnName(@"BeforeLensSpec").HasColumnType(@"nvarchar(20)").ValueGeneratedNever().HasMaxLength(20);
            builder.Property(x => x.BeforeLensType).HasColumnName(@"BeforeLensType").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.BrandDesignTypeId).HasColumnName(@"BrandDesignTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.BrandId).HasColumnName(@"BrandId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.BrandLensTypeId).HasColumnName(@"brandLensTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.BrandLensTypeRId).HasColumnName(@"BrandLensTypeRId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CoatingId).HasColumnName(@"CoatingId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Color).HasColumnName(@"Color").HasColumnType(@"varchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.ColorBottom).HasColumnName(@"ColorBottom").HasColumnType(@"decimal(18)").ValueGeneratedNever().HasPrecision(18, 0);
            builder.Property(x => x.ColoringTypeId).HasColumnName(@"ColoringTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ColorRatio).HasColumnName(@"ColorRatio").HasColumnType(@"decimal(18)").ValueGeneratedNever().HasPrecision(18, 0);
            builder.Property(x => x.ColorRatioTop).HasColumnName(@"ColorRatioTop").HasColumnType(@"decimal(18)").ValueGeneratedNever().HasPrecision(18, 0);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Consumer).HasColumnName(@"Consumer").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.Corridor).HasColumnName(@"Corridor").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CreateDate).HasColumnName(@"CreateDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DateMustDelivered).HasColumnName(@"dateMustDelivered").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.Dbl).HasColumnName(@"Dbl").HasColumnType(@"real").ValueGeneratedNever().HasPrecision(24);
            builder.Property(x => x.DefineCustomerId).HasColumnName(@"DefineCustomerId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DefineObjectId).HasColumnName(@"DefineObjectId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.DesignTypeId).HasColumnName(@"DesignTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.DesignTypeLensIndexId).HasColumnName(@"DesignTypeLensIndexId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.EffectiveDiameter).HasColumnName(@"EffectiveDiameter").HasColumnType(@"real").ValueGeneratedNever().HasPrecision(24);
            builder.Property(x => x.Ffa).HasColumnName(@"Ffa").HasColumnType(@"real").ValueGeneratedNever().HasPrecision(24);
            builder.Property(x => x.FittingL).HasColumnName(@"FittingL").HasColumnType(@"decimal(18)").ValueGeneratedNever().HasPrecision(18, 0);
            builder.Property(x => x.FittingR).HasColumnName(@"FittingR").HasColumnType(@"decimal(18)").ValueGeneratedNever().HasPrecision(18, 0);
            builder.Property(x => x.FrameTypeId).HasColumnName(@"FrameTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.HasCoating).HasColumnName(@"HasCoating").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.HasColor).HasColumnName(@"HasColor").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.HBox).HasColumnName(@"HBox").HasColumnType(@"real").ValueGeneratedNever().HasPrecision(24);
            builder.Property(x => x.IndexDocument).HasColumnName(@"IndexDocument").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.InfoCustomerId).HasColumnName(@"InfoCustomerId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.IpdL).HasColumnName(@"IpdL").HasColumnType(@"decimal(18)").ValueGeneratedNever().HasPrecision(18, 0);
            builder.Property(x => x.IpdR).HasColumnName(@"IpdR").HasColumnType(@"decimal(18)").ValueGeneratedNever().HasPrecision(18, 0);
            builder.Property(x => x.ItemBase).HasColumnName(@"ItemBase").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.LabelLPrint).HasColumnName(@"LabelLPrint").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.LabelLRPrint).HasColumnName(@"LabelLRPrint").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.LabelRPrint).HasColumnName(@"LabelRPrint").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.LensIndexId).HasColumnName(@"LensIndexId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.LensIndexMaterialTypeId).HasColumnName(@"LensIndexMaterialTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.LensTypeId).HasColumnName(@"LensTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.LensTypeRLensIndexRId).HasColumnName(@"LensTypeRLensIndexRId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.MaterialTypeId).HasColumnName(@"MaterialTypeId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType(@"nvarchar(10)").ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.Number).HasColumnName(@"Number").HasColumnType(@"nvarchar(15)").ValueGeneratedNever().HasMaxLength(15);
            builder.Property(x => x.OrderClass).HasColumnName(@"OrderClass").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd().HasPrecision(19, 0);
            builder.Property(x => x.OrderPrint).HasColumnName(@"OrderPrint").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.Panto).HasColumnName(@"Panto").HasColumnType(@"real").ValueGeneratedNever().HasPrecision(24);
            builder.Property(x => x.Price).HasColumnName(@"Price").HasColumnType(@"decimal(18,2)").ValueGeneratedNever().HasPrecision(18, 2);
            builder.Property(x => x.PrintLabel).HasColumnName(@"PrintLabel").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.PrintWarranty).HasColumnName(@"PrintWarranty").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.StoreName).HasColumnName(@"StoreName").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.TrackingCode).HasColumnName(@"TrackingCode").HasColumnType(@"nvarchar(20)").ValueGeneratedNever().HasMaxLength(20);
            builder.Property(x => x.VBox).HasColumnName(@"VBox").HasColumnType(@"real").ValueGeneratedNever().HasPrecision(24);
            builder.Property(x => x.Vd).HasColumnName(@"Vd").HasColumnType(@"real").ValueGeneratedNever().HasPrecision(24);
            builder.Property(x => x.WarrantyLRPrint).HasColumnName(@"WarrantyLRPrint").HasColumnType(@"bit").ValueGeneratedNever();
			builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType(@"int").ValueGeneratedNever().HasPrecision(10, 0);
			builder.HasKey(@"OrderId");
            builder.HasOne(x => x.TblLnsBrandDesignType).WithMany(op => op.TblLnsOrders).HasForeignKey(@"BrandDesignTypeId").IsRequired(false);
            builder.HasOne(x => x.TblLnsDesignTypeLensIndex).WithMany(op => op.TblLnsOrders).HasForeignKey(@"DesignTypeLensIndexId").IsRequired(false);
            builder.HasOne(x => x.TblLnsLensIndexMaterialType).WithMany(op => op.TblLnsOrders).HasForeignKey(@"LensIndexMaterialTypeId").IsRequired(false);
            builder.HasOne(x => x.TblLnsBrandLensTypeR).WithMany(op => op.TblLnsOrders).HasForeignKey(@"BrandLensTypeRId").IsRequired(false);
            builder.HasOne(x => x.TblLnsLensTypeRLensIndexR).WithMany(op => op.TblLnsOrders).HasForeignKey(@"LensTypeRLensIndexRId").IsRequired(false);
            builder.HasOne(x => x.TblLnsFrameType).WithMany(op => op.TblLnsOrders).HasForeignKey(@"FrameTypeId").IsRequired(false);
            builder.HasOne(x => x.TblLnsBrand).WithMany(op => op.TblLnsOrders).HasForeignKey(@"BrandId").IsRequired(false);
            builder.HasOne(x => x.TblLnsLensType).WithMany(op => op.TblLnsOrders).HasForeignKey(@"LensTypeId").IsRequired(false);
            builder.HasOne(x => x.TblLnsDesignType).WithMany(op => op.TblLnsOrders).HasForeignKey(@"DesignTypeId").IsRequired(false);
            builder.HasOne(x => x.TblLnsLensIndex).WithMany(op => op.TblLnsOrders).HasForeignKey(@"LensIndexId").IsRequired(false);
            builder.HasOne(x => x.TblLnsMaterialType).WithMany(op => op.TblLnsOrders).HasForeignKey(@"MaterialTypeId").IsRequired(false);
            builder.HasOne(x => x.TblLnsCoating).WithMany(op => op.TblLnsOrders).HasForeignKey(@"CoatingId").IsRequired(false);
            builder.HasOne(x => x.TblLnsColoringType).WithMany(op => op.TblLnsOrders).HasForeignKey(@"ColoringTypeId").IsRequired(false);
            builder.HasOne(x => x.TblLnsBrandLensType).WithMany(op => op.TblLnsOrders).HasForeignKey(@"BrandLensTypeId").IsRequired(false);
            builder.HasMany(x => x.TblLnsOrderItems).WithOne(op => op.TblLnsOrder).HasForeignKey(@"OrderId").IsRequired(false);
            builder.HasMany(x => x.TblLnsOrderservices).WithOne(op => op.TblLnsOrder).HasForeignKey(@"OrderId").IsRequired(false);
			builder.HasMany(x => x.TblWfwOrderProcesses).WithOne(op => op.TblLnsOrder).HasForeignKey(@"OrderId").IsRequired(true);
			CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsOrder> builder);

        #endregion
    }

}
