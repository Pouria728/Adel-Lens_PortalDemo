using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwProcessActionConverter
    {

        public static TblWfwProcessActionDto ToDto(this TblWfwProcessAction source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwProcessActionDto ToDtoWithRelated(this TblWfwProcessAction source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwProcessActionDto();

            // Properties
            target.Code = source.Code;
            target.Color = source.Color;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.Icon = source.Icon;
            target.IsActive = source.IsActive;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.ProcessActionId = source.ProcessActionId;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwProcessAction ToEntity(this TblWfwProcessActionDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwProcessAction();

            // Properties
            target.Code = source.Code;
            target.Color = source.Color;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.Icon = source.Icon;
            target.IsActive = source.IsActive;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.ProcessActionId = source.ProcessActionId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwProcessActionDto> ToDtos(this IEnumerable<TblWfwProcessAction> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblWfwProcessActionDto> ToDtosWithRelated(this IEnumerable<TblWfwProcessAction> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblWfwProcessAction> ToEntities(this IEnumerable<TblWfwProcessActionDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblWfwProcessAction source, TblWfwProcessActionDto target);

        static partial void OnEntityCreating(TblWfwProcessActionDto source, TblWfwProcessAction target);

    }

}
