using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwResultStepConverter
    {

        public static TblWfwResultStepDto ToDto(this TblWfwResultStep source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwResultStepDto ToDtoWithRelated(this TblWfwResultStep source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwResultStepDto();

            // Properties
            target.Code = source.Code;
            target.Command = source.Command;
            target.CommandType = source.CommandType;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = source.IsActive;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.RelationStepId = source.RelationStepId;
            target.ResultStepId = source.ResultStepId;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwResultStep ToEntity(this TblWfwResultStepDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwResultStep();

            // Properties
            target.Code = source.Code;
            target.Command = source.Command;
            target.CommandType = source.CommandType;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = source.IsActive;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.RelationStepId = source.RelationStepId;
            target.ResultStepId = source.ResultStepId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwResultStepDto> ToDtos(this IEnumerable<TblWfwResultStep> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblWfwResultStepDto> ToDtosWithRelated(this IEnumerable<TblWfwResultStep> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblWfwResultStep> ToEntities(this IEnumerable<TblWfwResultStepDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblWfwResultStep source, TblWfwResultStepDto target);

        static partial void OnEntityCreating(TblWfwResultStepDto source, TblWfwResultStep target);

    }

}
