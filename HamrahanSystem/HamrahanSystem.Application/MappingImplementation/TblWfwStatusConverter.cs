using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwStatusConverter
    {

        public static TblWfwStatusDto ToDto(this TblWfwStatus source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwStatusDto ToDtoWithRelated(this TblWfwStatus source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwStatusDto();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = source.IsActive;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.StatusId = source.StatusId;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwStatus ToEntity(this TblWfwStatusDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwStatus();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = source.IsActive;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.StatusId = source.StatusId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwStatusDto> ToDtos(this IEnumerable<TblWfwStatus> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblWfwStatusDto> ToDtosWithRelated(this IEnumerable<TblWfwStatus> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblWfwStatus> ToEntities(this IEnumerable<TblWfwStatusDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblWfwStatus source, TblWfwStatusDto target);

        static partial void OnEntityCreating(TblWfwStatusDto source, TblWfwStatus target);

    }

}
