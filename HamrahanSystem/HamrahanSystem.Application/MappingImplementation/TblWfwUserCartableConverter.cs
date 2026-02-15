using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwUserCartableConverter
    {

        public static TblWfwUserCartableDto ToDto(this TblWfwUserCartable source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwUserCartableDto ToDtoWithRelated(this TblWfwUserCartable source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwUserCartableDto();

            // Properties
            target.Active = source.Active;
            target.CartableId = source.CartableId;
            target.Paraph = source.Paraph;
            target.StatusId = source.StatusId;
            target.UpdateDate = source.UpdateDate;
            target.UserCartableId = source.UserCartableId;
            target.UserId = source.UserId;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwUserCartable ToEntity(this TblWfwUserCartableDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwUserCartable();

            // Properties
            target.Active = source.Active;
            target.CartableId = source.CartableId;
            target.Paraph = source.Paraph;
            target.StatusId = source.StatusId;
            target.UpdateDate = source.UpdateDate;
            target.UserCartableId = source.UserCartableId;
            target.UserId = source.UserId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwUserCartableDto> ToDtos(this IEnumerable<TblWfwUserCartable> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblWfwUserCartableDto> ToDtosWithRelated(this IEnumerable<TblWfwUserCartable> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblWfwUserCartable> ToEntities(this IEnumerable<TblWfwUserCartableDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblWfwUserCartable source, TblWfwUserCartableDto target);

        static partial void OnEntityCreating(TblWfwUserCartableDto source, TblWfwUserCartable target);

    }

}
