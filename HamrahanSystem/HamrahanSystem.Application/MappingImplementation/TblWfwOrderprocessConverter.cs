using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwOrderProcessConverter
    {

        public static TblWfwOrderProcessDto ToDto(this TblWfwOrderProcess source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwOrderProcessDto ToDtoWithRelated(this TblWfwOrderProcess source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwOrderProcessDto();

            // Properties
            target.DateComplete = source.DateComplete;
            target.DateCreate = source.DateCreate;
            target.OrderId = source.OrderId;
            target.OrderProcessId = source.OrderProcessId;
            target.ProcessId = source.ProcessId;
            target.StatusId = source.StatusId;

            // Navigation Properties
            if (level > 0) {
              target.TblWfwProcess = source.TblWfwProcess.ToDtoWithRelated(level - 1);
              target.TblLnsOrder = source.TblLnsOrder.ToDtoWithRelated(level - 1);
              target.TblWfwOrderProcessSteps = source.TblWfwOrderProcessSteps.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwOrderProcess ToEntity(this TblWfwOrderProcessDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwOrderProcess();

            // Properties
            target.DateComplete = source.DateComplete;
            target.DateCreate = source.DateCreate;
            target.OrderId = source.OrderId;
            target.OrderProcessId = source.OrderProcessId;
            target.ProcessId = source.ProcessId;
            target.StatusId = source.StatusId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwOrderProcessDto> ToDtos(this IEnumerable<TblWfwOrderProcess> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblWfwOrderProcessDto> ToDtosWithRelated(this IEnumerable<TblWfwOrderProcess> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblWfwOrderProcess> ToEntities(this IEnumerable<TblWfwOrderProcessDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblWfwOrderProcess source, TblWfwOrderProcessDto target);

        static partial void OnEntityCreating(TblWfwOrderProcessDto source, TblWfwOrderProcess target);

    }

}
