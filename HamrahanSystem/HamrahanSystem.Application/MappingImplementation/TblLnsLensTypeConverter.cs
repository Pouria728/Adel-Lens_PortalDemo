using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsLensTypeConverter
    {

        public static TblLnsLensTypeDto ToDto(this TblLnsLensType source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsLensTypeDto ToDtoWithRelated(this TblLnsLensType source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensTypeDto();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.BrandId = source.BrandId;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToBoolean(source.IsActive);
            target.IsCorridor = source.IsCorridor;
            target.IsSpecial = source.IsSpecial;
            target.LensTypeId = source.LensTypeId;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsBrandLensTypes = source.TblLnsBrandLensTypes.ToDtosWithRelated(level - 1);
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsLensType ToEntity(this TblLnsLensTypeDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensType();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.BrandId = source.BrandId;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToInt16(source.IsActive);
            target.IsCorridor = source.IsCorridor;
            target.IsSpecial = source.IsSpecial;
            target.LensTypeId = source.LensTypeId;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsLensTypeDto> ToDtos(this IEnumerable<TblLnsLensType> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblLnsLensTypeDto> ToDtos(this List<TblLnsLensType> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblLnsLensTypeDto> ToDtosWithRelated(this IEnumerable<TblLnsLensType> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

		public static List<TblLnsLensTypeDto> ToDtosWithRelated(this List<TblLnsLensType> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblLnsLensType> ToEntities(this IEnumerable<TblLnsLensTypeDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsLensType source, TblLnsLensTypeDto target);

        static partial void OnEntityCreating(TblLnsLensTypeDto source, TblLnsLensType target);

    }

}
