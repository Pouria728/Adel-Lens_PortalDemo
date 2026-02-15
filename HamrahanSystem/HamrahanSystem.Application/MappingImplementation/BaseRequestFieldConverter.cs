

using System.Collections.Generic;
using System.Linq;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class BaseRequestFieldConverter
    {

        public static BaseRequestFieldDto ToDto(this BaseRequestField source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static BaseRequestFieldDto ToDtoWithRelated(this BaseRequestField source, int level)
        {
            if (source == null)
                return null;

            var target = new BaseRequestFieldDto();

            // Properties
            target.AllowNull = source.AllowNull;
            target.BaseRequestFieldId = source.BaseRequestFieldId;
            target.BaseRequestId = source.BaseRequestId;
            target.BaseRequestParentFieldId = source.BaseRequestParentFieldId;
            target.DataTypeId = source.DataTypeId;
            target.FieldType = source.FieldType;
            target.MaxRecord = source.MaxRecord;
            target.Title = source.Title;

            // Navigation Properties
            if (level > 0)
            {
                target.BaseRequestFieldes_BaseRequestParentFieldId = source.BaseRequestFieldes_BaseRequestParentFieldId.ToDtosWithRelated(level - 1);
                target.BaseRequestFielde_BaseRequestParentFieldId = source.BaseRequestFielde_BaseRequestParentFieldId.ToDtoWithRelated(level - 1);
                target.BaseRequeste = source.BaseRequeste.ToDtoWithRelated(level - 1);
                target.RequestFieldes = source.RequestFieldes.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static BaseRequestField ToEntity(this BaseRequestFieldDto source)
        {
            if (source == null)
                return null;

            var target = new BaseRequestField();

            // Properties
            target.AllowNull = source.AllowNull;
            target.BaseRequestFieldId = source.BaseRequestFieldId;
            target.BaseRequestId = source.BaseRequestId;
            target.BaseRequestParentFieldId = source.BaseRequestParentFieldId;
            target.DataTypeId = source.DataTypeId;
            target.FieldType = source.FieldType;
            target.MaxRecord = source.MaxRecord;
            target.Title = source.Title;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<BaseRequestFieldDto> ToDtos(this IEnumerable<BaseRequestField> source)
        {
            if (source == null)
                return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<BaseRequestFieldDto> ToDtosWithRelated(this IEnumerable<BaseRequestField> source, int level)
        {
            if (source == null)
                return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<BaseRequestField> ToEntities(this IEnumerable<BaseRequestFieldDto> source)
        {
            if (source == null)
                return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(BaseRequestField source, BaseRequestFieldDto target);

        static partial void OnEntityCreating(BaseRequestFieldDto source, BaseRequestField target);

    }

}

