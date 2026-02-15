
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsTempConverter
    {

        public static TblLnsTempDto ToDto(this TblLnsTemp source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsTempDto ToDtoWithRelated(this TblLnsTemp source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsTempDto();

            // Properties
            target.C000 = source.C000;
            target.C025 = source.C025;
            target.C050 = source.C050;
            target.C075 = source.C075;
            target.C100 = source.C100;
            target.C125 = source.C125;
            target.C150 = source.C150;
            target.C175 = source.C175;
            target.C200 = source.C200;
            target.C225 = source.C225;
            target.C250 = source.C250;
            target.C275 = source.C275;
            target.C300 = source.C300;
            target.C325 = source.C325;
            target.C350 = source.C350;
            target.C375 = source.C375;
            target.C400 = source.C400;
            target.C425 = source.C425;
            target.C450 = source.C450;
            target.C475 = source.C475;
            target.C500 = source.C500;
            target.C525 = source.C525;
            target.C550 = source.C550;
            target.C575 = source.C575;
            target.C600 = source.C600;
            target.TempId = source.TempId;
            target.TempRow = source.TempRow;
            target.UserId = source.UserId;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsTemp ToEntity(this TblLnsTempDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsTemp();

            // Properties
            target.C000 = source.C000;
            target.C025 = source.C025;
            target.C050 = source.C050;
            target.C075 = source.C075;
            target.C100 = source.C100;
            target.C125 = source.C125;
            target.C150 = source.C150;
            target.C175 = source.C175;
            target.C200 = source.C200;
            target.C225 = source.C225;
            target.C250 = source.C250;
            target.C275 = source.C275;
            target.C300 = source.C300;
            target.C325 = source.C325;
            target.C350 = source.C350;
            target.C375 = source.C375;
            target.C400 = source.C400;
            target.C425 = source.C425;
            target.C450 = source.C450;
            target.C475 = source.C475;
            target.C500 = source.C500;
            target.C525 = source.C525;
            target.C550 = source.C550;
            target.C575 = source.C575;
            target.C600 = source.C600;
            target.TempId = source.TempId;
            target.TempRow = source.TempRow;
            target.UserId = source.UserId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsTempDto> ToDtos(this IEnumerable<TblLnsTemp> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblLnsTempDto> ToDtosWithRelated(this IEnumerable<TblLnsTemp> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblLnsTemp> ToEntities(this IEnumerable<TblLnsTempDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsTemp source, TblLnsTempDto target);

        static partial void OnEntityCreating(TblLnsTempDto source, TblLnsTemp target);

    }

}
