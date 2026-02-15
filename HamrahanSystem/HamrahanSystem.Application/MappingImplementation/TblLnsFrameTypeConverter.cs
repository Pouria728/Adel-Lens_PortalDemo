using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsFrameTypeConverter
    {

        public static TblLnsFrameTypeDto ToDto(this TblLnsFrameType source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsFrameTypeDto ToDtoWithRelated(this TblLnsFrameType source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsFrameTypeDto();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.FrameTypeId = source.FrameTypeId;
            target.IsActive = Convert.ToBoolean(source.IsActive);
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsFrameType ToEntity(this TblLnsFrameTypeDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsFrameType();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.FrameTypeId = source.FrameTypeId;
            target.IsActive = Convert.ToInt16(source.IsActive);
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsFrameTypeDto> ToDtos(this IEnumerable<TblLnsFrameType> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblLnsFrameTypeDto> ToDtos(this List<TblLnsFrameType> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblLnsFrameTypeDto> ToDtosWithRelated(this IEnumerable<TblLnsFrameType> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<TblLnsFrameTypeDto> ToDtosWithRelated(this List<TblLnsFrameType> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblLnsFrameType> ToEntities(this IEnumerable<TblLnsFrameTypeDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsFrameType source, TblLnsFrameTypeDto target);

        static partial void OnEntityCreating(TblLnsFrameTypeDto source, TblLnsFrameType target);

    }

}
