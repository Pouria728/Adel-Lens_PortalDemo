using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

	public static partial class TblLnsOrderItemConverter
	{

		public static TblLnsOrderItemDto ToDto(this TblLnsOrderItem source)
		{
			return source.ToDtoWithRelated(0);
		}

		public static TblLnsOrderItemDto ToDtoWithRelated(this TblLnsOrderItem source, int level)
		{
			if (source == null)
				return null;

			var target = new TblLnsOrderItemDto();

			// Properties
			target.Axis = source.Axis;
			target.BaseOfPrism = source.BaseOfPrism;
			target.BrandId = source.BrandId;
			target.BrandLensTypeRId = source.BrandLensTypeRId;
			target.ConsumerTitle = source.ConsumerTitle;
			target.CorrLRowNumber = source.CorrLRowNumber;
			target.CorrProductId = source.CorrProductId;
			target.Cutting = source.Cutting;
			target.CylId = source.CylId;
			target.Dc = source.Dc;
			target.DefineObjectId = source.DefineObjectId;
			target.Description = source.Description;
			target.Dia = source.Dia;
			target.Discount = source.Discount;
			target.EyeType = source.EyeType;
			target.Fee = source.Fee;
			target.Fitting = source.Fitting;
			target.Ipd = source.Ipd;
			target.IsRight = source.IsRight;
			target.ItemAdd = source.ItemAdd;
			target.LensIndexRSphId = source.LensIndexRSphId;
			target.LensTypeRLensIndexRId = source.LensTypeRLensIndexRId;
			target.NeedTools = source.NeedTools == null ? false : source.NeedTools.Value;
			target.Optician = source.Optician;
			target.OrderId = source.OrderId;
			target.OrderItemId = source.OrderItemId;
			target.Position = source.Position;
			target.Price = source.Price;
			target.Prism = source.Prism;
			target.ProvidedQuantity = source.ProvidedQuantity;
			target.Quantity = source.Quantity;
			target.RowNumber = source.RowNumber;
			target.RowVersion = source.RowVersion;
			target.SphCylId = source.SphCylId;
			target.SphId = source.SphId;

			// Navigation Properties
			if (level > 0)
			{
				target.TblLnsOrder = source.TblLnsOrder.ToDtoWithRelated(level - 1);
				target.TblLnsSph = source.TblLnsSph.ToDtoWithRelated(level - 1);
				target.TblLnsCyl = source.TblLnsCyl.ToDtoWithRelated(level - 1);
				target.TblLnsLensIndexRSph = source.TblLnsLensIndexRSph.ToDtoWithRelated(level - 1);
				target.TblLnsSphCyl = source.TblLnsSphCyl.ToDtoWithRelated(level - 1);
				target.TblLnsBrand = source.TblLnsBrand.ToDtoWithRelated(level - 1);
				target.TblLnsLensTypeRLensIndexR = source.TblLnsLensTypeRLensIndexR.ToDtoWithRelated(level - 1);
				target.TblLnsBrandLensTypeR = source.TblLnsBrandLensTypeR.ToDtoWithRelated(level - 1);
				target.TblClrDefineObject = source.TblClrDefineObject.ToDtoWithRelated(level - 1);
			}

			// User-defined partial method
			OnDtoCreating(source, target);

			return target;
		}

		public static TblLnsOrderItem ToEntity(this TblLnsOrderItemDto source)
		{
			if (source == null)
				return null;

			var target = new TblLnsOrderItem();

			// Properties
			target.Axis = source.Axis;
			target.BaseOfPrism = source.BaseOfPrism;
			target.BrandId = source.BrandId;
			target.BrandLensTypeRId = source.BrandLensTypeRId;
			target.ConsumerTitle = source.ConsumerTitle;
			target.CorrLRowNumber = source.CorrLRowNumber;
			target.CorrProductId = source.CorrProductId;
			target.Cutting = source.Cutting;
			target.CylId = source.CylId;
			target.Dc = source.Dc;
			target.DefineObjectId = source.DefineObjectId;
			target.Description = source.Description;
			target.Dia = source.Dia;
			target.Discount = source.Discount;
			target.EyeType = source.EyeType;
			target.Fee = source.Fee;
			target.Fitting = source.Fitting;
			target.Ipd = source.Ipd;
			target.IsRight = source.IsRight;
			target.ItemAdd = source.ItemAdd;
			target.LensIndexRSphId = source.LensIndexRSphId;
			target.LensTypeRLensIndexRId = source.LensTypeRLensIndexRId;
			target.NeedTools = source.NeedTools;
			target.Optician = source.Optician;
			target.OrderId = source.OrderId;
			target.OrderItemId = source.OrderItemId;
			target.Position = source.Position;
			target.Price = source.Price;
			target.Prism = source.Prism;
			target.ProvidedQuantity = source.ProvidedQuantity;
			target.Quantity = source.Quantity;
			target.RowNumber = source.RowNumber;
			target.RowVersion = source.RowVersion;
			target.SphCylId = source.SphCylId;
			target.SphId = source.SphId;

			// User-defined partial method
			OnEntityCreating(source, target);

			return target;
		}

		public static List<TblLnsOrderItemDto> ToDtos(this IEnumerable<TblLnsOrderItem> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblLnsOrderItemDto> ToDtosWithRelated(this IEnumerable<TblLnsOrderItem> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblLnsOrderItem> ToEntities(this IEnumerable<TblLnsOrderItemDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}

		static partial void OnDtoCreating(TblLnsOrderItem source, TblLnsOrderItemDto target);

		static partial void OnEntityCreating(TblLnsOrderItemDto source, TblLnsOrderItem target);

	}

}
