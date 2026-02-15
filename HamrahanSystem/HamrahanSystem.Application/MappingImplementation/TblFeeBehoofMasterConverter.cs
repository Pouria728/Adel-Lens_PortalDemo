

using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblFeeBehoofMasterConverter
    {

        public static TblFeeBehoofMasterDto ToDto(this TblFeeBehoofMaster source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblFeeBehoofMasterDto ToDtoWithRelated(this TblFeeBehoofMaster source, int level)
        {
            if (source == null)
              return null;

            var target = new TblFeeBehoofMasterDto();

            // Properties
            target.AgentTypeIndex = source.AgentTypeIndex;
            target.BehoofCode = source.BehoofCode;
            target.BehoofTypeIndex = source.BehoofTypeIndex;
            target.Company = source.Company;
            target.EnglishBehoof = source.EnglishBehoof;
            target.FarsiBehoof = source.FarsiBehoof;
            target.INTypeIndex = source.INTypeIndex;
            target.LatinBehoof = source.LatinBehoof;
            target.LinkedCodingTable = source.LinkedCodingTable;
            target.ListType = source.ListType;
            target.Param = source.Param;

            // Navigation Properties
            if (level > 0) {
              target.TblFeeBehoofDetails = source.TblFeeBehoofDetails.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblFeeBehoofMaster ToEntity(this TblFeeBehoofMasterDto source)
        {
            if (source == null)
              return null;

            var target = new TblFeeBehoofMaster();

            // Properties
            target.AgentTypeIndex = source.AgentTypeIndex;
            target.BehoofCode = source.BehoofCode;
            target.BehoofTypeIndex = source.BehoofTypeIndex;
            target.Company = source.Company;
            target.EnglishBehoof = source.EnglishBehoof;
            target.FarsiBehoof = source.FarsiBehoof;
            target.INTypeIndex = source.INTypeIndex;
            target.LatinBehoof = source.LatinBehoof;
            target.LinkedCodingTable = source.LinkedCodingTable;
            target.ListType = source.ListType;
            target.Param = source.Param;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblFeeBehoofMasterDto> ToDtos(this IEnumerable<TblFeeBehoofMaster> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblFeeBehoofMasterDto> ToDtosWithRelated(this IEnumerable<TblFeeBehoofMaster> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblFeeBehoofMaster> ToEntities(this IEnumerable<TblFeeBehoofMasterDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblFeeBehoofMaster source, TblFeeBehoofMasterDto target);

        static partial void OnEntityCreating(TblFeeBehoofMasterDto source, TblFeeBehoofMaster target);

    }

}
