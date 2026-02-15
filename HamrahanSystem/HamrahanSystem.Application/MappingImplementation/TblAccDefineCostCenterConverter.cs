

using System.Collections.Generic;
using System.Linq;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblAccDefineCostCenterConverter
    {

        public static TblAccDefineCostCenterDto ToDto(this TblAccDefineCostCenter source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblAccDefineCostCenterDto ToDtoWithRelated(this TblAccDefineCostCenter source, int level)
        {
            if (source == null)
              return null;

            var target = new TblAccDefineCostCenterDto();

            // Properties
            target.AccDefineCostCenterId = source.AccDefineCostCenterId;
            target.Active = source.Active;
            target.CodeCostCenter = source.CodeCostCenter;
            target.Company = source.Company;
            target.IndexTypeCostCenter = source.IndexTypeCostCenter;
            target.IndexTypeTask = source.IndexTypeTask;
            target.IsActive = source.IsActive;
            target.NameCostCenter = source.NameCostCenter;
            target.NameCostCenterEN = source.NameCostCenterEN;
            target.RecNo = source.RecNo;
            target.RefMaster = source.RefMaster;
            target.RefMasterCompany = source.RefMasterCompany;
            target.RefMasterKey = source.RefMasterKey;
            target.RegistryKey = source.RegistryKey;
            target.RNGroupFormal = source.RNGroupFormal;
            target.StateFormal = source.StateFormal;

            // Navigation Properties
            if (level > 0) {
              target.TblAccDefineCostCenters_RefMasterKey_RefMasterCompany = source.TblAccDefineCostCenters_RefMasterKey_RefMasterCompany.ToDtosWithRelated(level - 1);
              target.TblAccDefineCostCenter_RefMasterKey_RefMasterCompany = source.TblAccDefineCostCenter_RefMasterKey_RefMasterCompany.ToDtoWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblAccDefineCostCenter ToEntity(this TblAccDefineCostCenterDto source)
        {
            if (source == null)
              return null;

            var target = new TblAccDefineCostCenter();

            // Properties
            target.AccDefineCostCenterId = source.AccDefineCostCenterId;
            target.Active = source.Active;
            target.CodeCostCenter = source.CodeCostCenter;
            target.Company = source.Company;
            target.IndexTypeCostCenter = source.IndexTypeCostCenter;
            target.IndexTypeTask = source.IndexTypeTask;
            target.IsActive = source.IsActive;
            target.NameCostCenter = source.NameCostCenter;
            target.NameCostCenterEN = source.NameCostCenterEN;
            target.RecNo = source.RecNo;
            target.RefMaster = source.RefMaster;
            target.RefMasterCompany = source.RefMasterCompany;
            target.RefMasterKey = source.RefMasterKey;
            target.RegistryKey = source.RegistryKey;
            target.RNGroupFormal = source.RNGroupFormal;
            target.StateFormal = source.StateFormal;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblAccDefineCostCenterDto> ToDtos(this IEnumerable<TblAccDefineCostCenter> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblAccDefineCostCenterDto> ToDtosWithRelated(this IEnumerable<TblAccDefineCostCenter> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblAccDefineCostCenter> ToEntities(this IEnumerable<TblAccDefineCostCenterDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblAccDefineCostCenter source, TblAccDefineCostCenterDto target);

        static partial void OnEntityCreating(TblAccDefineCostCenterDto source, TblAccDefineCostCenter target);

    }

}
