using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwConditionConverter
    {

        public static TblWfwConditionDto ToDto(this TblWfwCondition source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwConditionDto ToDtoWithRelated(this TblWfwCondition source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwConditionDto();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.ConditionId = source.ConditionId;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = source.IsActive;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwCondition ToEntity(this TblWfwConditionDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwCondition();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.ConditionId = source.ConditionId;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = source.IsActive;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwConditionDto> ToDtos(this IEnumerable<TblWfwCondition> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblWfwConditionDto> ToDtosWithRelated(this IEnumerable<TblWfwCondition> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblWfwCondition> ToEntities(this IEnumerable<TblWfwConditionDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblWfwCondition source, TblWfwConditionDto target);

        static partial void OnEntityCreating(TblWfwConditionDto source, TblWfwCondition target);

    }

}
