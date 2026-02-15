using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsOrderConverter
    {

        public static TblLnsOrderDto ToDto(this TblLnsOrder source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsOrderDto ToDtoWithRelated(this TblLnsOrder source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsOrderDto();

            // Properties
            target.FactorNo = source.FactorNo;
            target.AccDefineValutaId = source.AccDefineValutaId;
            target.BeforeLensSpec = source.BeforeLensSpec;
            target.BeforeLensType = source.BeforeLensType;
            target.BrandDesignTypeId = source.BrandDesignTypeId;
            target.BrandId = source.BrandId;
            target.BrandLensTypeId = source.BrandLensTypeId;
            target.BrandLensTypeRId = source.BrandLensTypeRId;
            target.CoatingId = source.CoatingId;
            target.Color = source.Color;
            target.ColorBottom = source.ColorBottom;
            target.ColoringTypeId = source.ColoringTypeId;
            target.ColorRatio = source.ColorRatio;
            target.ColorRatioTop = source.ColorRatioTop;
            target.Company = source.Company;
            target.Consumer = source.Consumer;
            target.Corridor = source.Corridor;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.DateMustDelivered = source.DateMustDelivered;
            target.Dbl = source.Dbl;
            target.DefineCustomerId = source.DefineCustomerId;
            target.DefineObjectId = source.DefineObjectId;
            target.Description = source.Description;
            target.DesignTypeId = source.DesignTypeId;
            target.DesignTypeLensIndexId = source.DesignTypeLensIndexId;
            target.EffectiveDiameter = source.EffectiveDiameter;
            target.Ffa = source.Ffa;
            target.FittingL = source.FittingL;
            target.FittingR = source.FittingR;
            target.FrameTypeId = source.FrameTypeId;
            target.HasCoating = source.HasCoating;
            target.HasColor = source.HasColor;
            target.HBox = source.HBox;
            target.IndexDocument = source.IndexDocument;
            target.InfoCustomerId = source.InfoCustomerId;
            target.IpdL = source.IpdL;
            target.IpdR = source.IpdR;
            target.ItemBase = source.ItemBase;
            target.LabelLPrint = source.LabelLPrint;
            target.LabelLRPrint = source.LabelLRPrint;
            target.LabelRPrint = source.LabelRPrint;
            target.LensIndexId = source.LensIndexId;
            target.LensIndexMaterialTypeId = source.LensIndexMaterialTypeId;
            target.LensTypeId = source.LensTypeId;
            target.LensTypeRLensIndexRId = source.LensTypeRLensIndexRId;
            target.MaterialTypeId = source.MaterialTypeId;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Number = source.Number;
            target.OrderClass = source.OrderClass;
            target.OrderId = source.OrderId;
            target.OrderPrint = source.OrderPrint;
            target.Panto = source.Panto;
            target.Price = source.Price;
            target.PrintLabel = source.PrintLabel;
            target.PrintWarranty = source.PrintWarranty;
            target.StoreName = source.StoreName;
            target.TrackingCode = source.TrackingCode;
            target.VBox = source.VBox;
            target.Vd = source.Vd;
            target.WarrantyLRPrint = source.WarrantyLRPrint;
			target.StatusId = source.StatusId;

			// Navigation Properties
			if (level > 0) {
              target.TblLnsBrandDesignType = source.TblLnsBrandDesignType.ToDtoWithRelated(level - 1);
              target.TblLnsDesignTypeLensIndex = source.TblLnsDesignTypeLensIndex.ToDtoWithRelated(level - 1);
              target.TblLnsLensIndexMaterialType = source.TblLnsLensIndexMaterialType.ToDtoWithRelated(level - 1);
              target.TblLnsBrandLensTypeR = source.TblLnsBrandLensTypeR.ToDtoWithRelated(level - 1);
              target.TblLnsLensTypeRLensIndexR = source.TblLnsLensTypeRLensIndexR.ToDtoWithRelated(level - 1);
              target.TblLnsFrameType = source.TblLnsFrameType.ToDtoWithRelated(level - 1);
              target.TblLnsBrand = source.TblLnsBrand.ToDtoWithRelated(level - 1);
              target.TblLnsLensType = source.TblLnsLensType.ToDtoWithRelated(level - 1);
              target.TblLnsDesignType = source.TblLnsDesignType.ToDtoWithRelated(level - 1);
              target.TblLnsLensIndex = source.TblLnsLensIndex.ToDtoWithRelated(level - 1);
              target.TblLnsMaterialType = source.TblLnsMaterialType.ToDtoWithRelated(level - 1);
              target.TblLnsCoating = source.TblLnsCoating.ToDtoWithRelated(level - 1);
              target.TblLnsColoringType = source.TblLnsColoringType.ToDtoWithRelated(level - 1);
              target.TblLnsBrandLensType = source.TblLnsBrandLensType.ToDtoWithRelated(level - 1);
              target.TblLnsOrderItems = source.TblLnsOrderItems.ToDtosWithRelated(level - 1);
              target.TblLnsOrderservices = source.TblLnsOrderservices.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsOrder ToEntity(this TblLnsOrderDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsOrder();

            // Properties
            target.FactorNo = source.FactorNo;
            target.AccDefineValutaId = source.AccDefineValutaId;
            target.BeforeLensSpec = source.BeforeLensSpec;
            target.BeforeLensType = source.BeforeLensType;
            target.BrandDesignTypeId = source.BrandDesignTypeId;
            target.BrandId = source.BrandId;
            target.BrandLensTypeId = source.BrandLensTypeId;
            target.BrandLensTypeRId = source.BrandLensTypeRId;
            target.CoatingId = source.CoatingId;
            target.Color = source.Color;
            target.ColorBottom = source.ColorBottom;
            target.ColoringTypeId = source.ColoringTypeId;
            target.ColorRatio = source.ColorRatio;
            target.ColorRatioTop = source.ColorRatioTop;
            target.Company = source.Company;
            target.Consumer = source.Consumer;
            target.Corridor = source.Corridor;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.DateMustDelivered = source.DateMustDelivered;
            target.Dbl = source.Dbl;
            target.DefineCustomerId = source.DefineCustomerId;
            target.DefineObjectId = source.DefineObjectId;
            target.Description = source.Description;
            target.DesignTypeId = source.DesignTypeId;
            target.DesignTypeLensIndexId = source.DesignTypeLensIndexId;
            target.EffectiveDiameter = source.EffectiveDiameter;
            target.Ffa = source.Ffa;
            target.FittingL = source.FittingL;
            target.FittingR = source.FittingR;
            target.FrameTypeId = source.FrameTypeId;
            target.HasCoating = source.HasCoating;
            target.HasColor = source.HasColor;
            target.HBox = source.HBox;
            target.IndexDocument = source.IndexDocument;
            target.InfoCustomerId = source.InfoCustomerId;
            target.IpdL = source.IpdL;
            target.IpdR = source.IpdR;
            target.ItemBase = source.ItemBase;
            target.LabelLPrint = source.LabelLPrint;
            target.LabelLRPrint = source.LabelLRPrint;
            target.LabelRPrint = source.LabelRPrint;
            target.LensIndexId = source.LensIndexId;
            target.LensIndexMaterialTypeId = source.LensIndexMaterialTypeId;
            target.LensTypeId = source.LensTypeId;
            target.LensTypeRLensIndexRId = source.LensTypeRLensIndexRId;
            target.MaterialTypeId = source.MaterialTypeId;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Number = source.Number;
            target.OrderClass = source.OrderClass;
            target.OrderId = source.OrderId;
            target.OrderPrint = source.OrderPrint;
            target.Panto = source.Panto;
            target.Price = source.Price;
            target.PrintLabel = source.PrintLabel;
            target.PrintWarranty = source.PrintWarranty;
            target.StoreName = source.StoreName;
            target.TrackingCode = source.TrackingCode;
            target.VBox = source.VBox;
            target.Vd = source.Vd;
            target.WarrantyLRPrint = source.WarrantyLRPrint;
			target.StatusId = source.StatusId;

			// User-defined partial method
			OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsOrderDto> ToDtos(this IEnumerable<TblLnsOrder> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblLnsOrderDto> ToDtos(this IList<TblLnsOrder> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblLnsOrderDto> ToDtosWithRelated(this IEnumerable<TblLnsOrder> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<TblLnsOrderDto> ToDtosWithRelated(this IList<TblLnsOrder> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblLnsOrder> ToEntities(this IEnumerable<TblLnsOrderDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }
		public static List<TblLnsOrder> ToEntities(this IList<TblLnsOrderDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}

		static partial void OnDtoCreating(TblLnsOrder source, TblLnsOrderDto target);

        static partial void OnEntityCreating(TblLnsOrderDto source, TblLnsOrder target);

    }

}
