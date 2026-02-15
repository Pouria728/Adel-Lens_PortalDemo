using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsBrandConverter
    {

        public static TblLnsBrandDto ToDto(this TblLnsBrand source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsBrandDto ToDtoWithRelated(this TblLnsBrand source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsBrandDto();

            // Properties
            target.BrandId = source.BrandId;
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToBoolean(source.IsActive);
            target.IsSpecial = source.IsSpecial;
            target.IsStock = source.IsStock;
            target.IsStockGranty = source.IsStockGranty;

            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsBrandCoatings = source.TblLnsBrandCoatings.ToDtosWithRelated(level - 1);
              target.TblLnsBrandLensTypes = source.TblLnsBrandLensTypes.ToDtosWithRelated(level - 1);
              target.TblLnsBrandLensTypeRs = source.TblLnsBrandLensTypeRs.ToDtosWithRelated(level - 1);
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
              target.TblLnsOrderItems = source.TblLnsOrderItems.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsBrand ToEntity(this TblLnsBrandDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsBrand();

            // Properties
            target.BrandId = source.BrandId;
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToInt16(source.IsActive);
            target.IsSpecial = source.IsSpecial;
            target.IsStock = source.IsStock;
            target.IsStockGranty = source.IsStockGranty;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsBrandDto> ToDtos(this IEnumerable<TblLnsBrand> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblLnsBrandDto> ToDtos(this List<TblLnsBrand> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblLnsBrandDto> ToDtosWithRelated(this IEnumerable<TblLnsBrand> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<TblLnsBrandDto> ToDtosWithRelated(this List<TblLnsBrand> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblLnsBrand> ToEntities(this IEnumerable<TblLnsBrandDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsBrand source, TblLnsBrandDto target);

        static partial void OnEntityCreating(TblLnsBrandDto source, TblLnsBrand target);

    }

}
