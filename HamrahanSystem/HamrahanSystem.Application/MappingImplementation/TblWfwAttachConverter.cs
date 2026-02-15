using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwAttachConverter
    {

        public static TblWfwAttachDto ToDto(this TblWfwAttach source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwAttachDto ToDtoWithRelated(this TblWfwAttach source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwAttachDto();

            // Properties
            target.AttachId = source.AttachId;
            target.CartableId = source.CartableId;
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.DocumentId = source.DocumentId;
            target.FileStock = source.FileStock;
            target.IndexDocument = source.IndexDocument;
            target.IsActive = source.IsActive;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.Suffix = source.Suffix;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwAttach ToEntity(this TblWfwAttachDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwAttach();

            // Properties
            target.AttachId = source.AttachId;
            target.CartableId = source.CartableId;
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.DocumentId = source.DocumentId;
            target.FileStock = source.FileStock;
            target.IndexDocument = source.IndexDocument;
            target.IsActive = source.IsActive;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.Suffix = source.Suffix;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwAttachDto> ToDtos(this IEnumerable<TblWfwAttach> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblWfwAttachDto> ToDtosWithRelated(this IEnumerable<TblWfwAttach> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblWfwAttach> ToEntities(this IEnumerable<TblWfwAttachDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblWfwAttach source, TblWfwAttachDto target);

        static partial void OnEntityCreating(TblWfwAttachDto source, TblWfwAttach target);

    }

}
