using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsSphConverter
    {

        public static TblLnsSphDto ToDto(this TblLnsSph source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsSphDto ToDtoWithRelated(this TblLnsSph source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsSphDto();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToBoolean(source.IsActive);
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;
            target.SphId = source.SphId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsLensIndexRSphs = source.TblLnsLensIndexRSphs.ToDtosWithRelated(level - 1);
              target.TblLnsOrderItems = source.TblLnsOrderItems.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsSph ToEntity(this TblLnsSphDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsSph();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToInt16(source.IsActive);
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;
            target.SphId = source.SphId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsSphDto> ToDtos(this IEnumerable<TblLnsSph> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblLnsSphDto> ToDtos(this List<TblLnsSph> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblLnsSphDto> ToDtosWithRelated(this IEnumerable<TblLnsSph> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<TblLnsSphDto> ToDtosWithRelated(this List<TblLnsSph> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblLnsSph> ToEntities(this IEnumerable<TblLnsSphDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }
		public static List<TblLnsSph> ToEntities(this List<TblLnsSphDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}

		static partial void OnDtoCreating(TblLnsSph source, TblLnsSphDto target);

        static partial void OnEntityCreating(TblLnsSphDto source, TblLnsSph target);

    }

}
