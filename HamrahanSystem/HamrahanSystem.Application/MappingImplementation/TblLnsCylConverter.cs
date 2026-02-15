using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsCylConverter
    {

        public static TblLnsCylDto ToDto(this TblLnsCyl source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsCylDto ToDtoWithRelated(this TblLnsCyl source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsCylDto();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.CylId = source.CylId;
            target.Description = source.Description;
            target.IsActive = Convert.ToBoolean(source.IsActive);
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsOrderItems = source.TblLnsOrderItems.ToDtosWithRelated(level - 1);
              target.TblLnsSphCyls = source.TblLnsSphCyls.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsCyl ToEntity(this TblLnsCylDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsCyl();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.CylId = source.CylId;
            target.Description = source.Description;
            target.IsActive = Convert.ToInt16(source.IsActive);
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsCylDto> ToDtos(this IEnumerable<TblLnsCyl> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblLnsCylDto> ToDtos(this List<TblLnsCyl> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblLnsCylDto> ToDtosWithRelated(this IEnumerable<TblLnsCyl> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<TblLnsCylDto> ToDtosWithRelated(this List<TblLnsCyl> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblLnsCyl> ToEntities(this IEnumerable<TblLnsCylDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }
		public static List<TblLnsCyl> ToEntities(this List<TblLnsCylDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}

		static partial void OnDtoCreating(TblLnsCyl source, TblLnsCylDto target);

        static partial void OnEntityCreating(TblLnsCylDto source, TblLnsCyl target);

    }

}
