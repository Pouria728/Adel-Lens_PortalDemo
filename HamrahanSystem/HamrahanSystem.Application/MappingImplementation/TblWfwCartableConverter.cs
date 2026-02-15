using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwCartableConverter
    {

        public static TblWfwCartableDto ToDto(this TblWfwCartable source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwCartableDto ToDtoWithRelated(this TblWfwCartable source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwCartableDto();

            // Properties
            target.AdvertNo = source.AdvertNo;
            target.CartableId = source.CartableId;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.DaysNo = source.DaysNo;
            target.DocumentDate = source.DocumentDate;
            target.DocumentId = source.DocumentId;
            target.DocumentNo = source.DocumentNo;
            target.DocumentRecNo = source.DocumentRecNo;
            target.FiscalYear = source.FiscalYear;
            target.IndexDocument = source.IndexDocument;
            target.ProcessStepId = source.ProcessStepId;
            target.Status = source.Status;
            target.UpdateDate = source.UpdateDate;
            target.UserId = source.UserId;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwCartable ToEntity(this TblWfwCartableDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwCartable();

            // Properties
            target.AdvertNo = source.AdvertNo;
            target.CartableId = source.CartableId;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.DaysNo = source.DaysNo;
            target.DocumentDate = source.DocumentDate;
            target.DocumentId = source.DocumentId;
            target.DocumentNo = source.DocumentNo;
            target.DocumentRecNo = source.DocumentRecNo;
            target.FiscalYear = source.FiscalYear;
            target.IndexDocument = source.IndexDocument;
            target.ProcessStepId = source.ProcessStepId;
            target.Status = source.Status;
            target.UpdateDate = source.UpdateDate;
            target.UserId = source.UserId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwCartableDto> ToDtos(this IEnumerable<TblWfwCartable> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblWfwCartableDto> ToDtosWithRelated(this IEnumerable<TblWfwCartable> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblWfwCartable> ToEntities(this IEnumerable<TblWfwCartableDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblWfwCartable source, TblWfwCartableDto target);

        static partial void OnEntityCreating(TblWfwCartableDto source, TblWfwCartable target);

    }

}
