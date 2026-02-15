using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblDefineServiceConverter
    {

        public static TblDefineServiceDto ToDto(this TblDefineService source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblDefineServiceDto ToDtoWithRelated(this TblDefineService source, int level)
        {
            if (source == null)
              return null;

            var target = new TblDefineServiceDto();

            // Properties
            target.Active = source.Active;
            target.Company = source.Company;
            target.DefineServiceId = source.DefineServiceId;
            target.IsActive = source.IsActive;
            target.KindService = source.KindService;
            target.RecNo = source.RecNo;
            target.RNScruple = source.RNScruple;
            target.ServiceCode = source.ServiceCode;
            target.ServiceName = source.ServiceName;
            target.ServiceNameEN = source.ServiceNameEN;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblDefineService ToEntity(this TblDefineServiceDto source)
        {
            if (source == null)
              return null;

            var target = new TblDefineService();

            // Properties
            target.Active = source.Active;
            target.Company = source.Company;
            target.DefineServiceId = source.DefineServiceId;
            target.IsActive = source.IsActive;
            target.KindService = source.KindService;
            target.RecNo = source.RecNo;
            target.RNScruple = source.RNScruple;
            target.ServiceCode = source.ServiceCode;
            target.ServiceName = source.ServiceName;
            target.ServiceNameEN = source.ServiceNameEN;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblDefineServiceDto> ToDtos(this IEnumerable<TblDefineService> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblDefineServiceDto> ToDtosWithRelated(this IEnumerable<TblDefineService> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblDefineService> ToEntities(this IEnumerable<TblDefineServiceDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblDefineService source, TblDefineServiceDto target);

        static partial void OnEntityCreating(TblDefineServiceDto source, TblDefineService target);

    }

}
