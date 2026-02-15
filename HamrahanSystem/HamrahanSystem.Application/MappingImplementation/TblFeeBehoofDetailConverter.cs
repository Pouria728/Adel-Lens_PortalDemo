

using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblFeeBehoofDetailConverter
    {

        public static TblFeeBehoofDetailDto ToDto(this TblFeeBehoofDetail source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblFeeBehoofDetailDto ToDtoWithRelated(this TblFeeBehoofDetail source, int level)
        {
            if (source == null)
              return null;

            var target = new TblFeeBehoofDetailDto();

            // Properties
            target.Aggregation = source.Aggregation;
            target.AggregationScore = source.AggregationScore;
            target.BehoofCode = source.BehoofCode;
            target.CalculateNO = source.CalculateNO;
            target.ChangeDate = source.ChangeDate;
            target.ChangeIndex = source.ChangeIndex;
            target.ChangeUser = source.ChangeUser;
            target.CodeInsurance = source.CodeInsurance;
            target.Company = source.Company;
            target.DayCounter = source.DayCounter;
            target.Depository = source.Depository;
            target.EmployeeCode = source.EmployeeCode;
            target.FiatPrintView = source.FiatPrintView;
            target.FiatView = source.FiatView;
            target.FishView = source.FishView;
            target.FishViewNO = source.FishViewNO;
            target.FixCostProject = source.FixCostProject;
            target.FormulCeil = source.FormulCeil;
            target.FormulCeilDes = source.FormulCeilDes;
            target.FormulCeilMerge = source.FormulCeilMerge;
            target.FormulDesList = source.FormulDesList;
            target.FormulList = source.FormulList;
            target.FormulMerge = source.FormulMerge;
            target.FormViewNO = source.FormViewNO;
            target.FromDate = source.FromDate;
            target.HasCoef = source.HasCoef;
            target.IncomeNo = source.IncomeNo;
            target.Insurancable = source.Insurancable;
            target.Marriage = source.Marriage;
            target.NotCash = source.NotCash;
            target.ONOFF = source.ONOFF;
            target.SelectAccount = source.SelectAccount;
            target.Subable = source.Subable;
            target.Sumable = source.Sumable;
            target.SumFishNoView = source.SumFishNoView;
            target.Taxable = source.Taxable;
            target.Timeshit = source.Timeshit;
            target.YearsBase = source.YearsBase;

            // Navigation Properties
            if (level > 0) {
              target.TblFeeBehoofMaster = source.TblFeeBehoofMaster.ToDtoWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblFeeBehoofDetail ToEntity(this TblFeeBehoofDetailDto source)
        {
            if (source == null)
              return null;

            var target = new TblFeeBehoofDetail();

            // Properties
            target.Aggregation = source.Aggregation;
            target.AggregationScore = source.AggregationScore;
            target.BehoofCode = source.BehoofCode;
            target.CalculateNO = source.CalculateNO;
            target.ChangeDate = source.ChangeDate;
            target.ChangeIndex = source.ChangeIndex;
            target.ChangeUser = source.ChangeUser;
            target.CodeInsurance = source.CodeInsurance;
            target.Company = source.Company;
            target.DayCounter = source.DayCounter;
            target.Depository = source.Depository;
            target.EmployeeCode = source.EmployeeCode;
            target.FiatPrintView = source.FiatPrintView;
            target.FiatView = source.FiatView;
            target.FishView = source.FishView;
            target.FishViewNO = source.FishViewNO;
            target.FixCostProject = source.FixCostProject;
            target.FormulCeil = source.FormulCeil;
            target.FormulCeilDes = source.FormulCeilDes;
            target.FormulCeilMerge = source.FormulCeilMerge;
            target.FormulDesList = source.FormulDesList;
            target.FormulList = source.FormulList;
            target.FormulMerge = source.FormulMerge;
            target.FormViewNO = source.FormViewNO;
            target.FromDate = source.FromDate;
            target.HasCoef = source.HasCoef;
            target.IncomeNo = source.IncomeNo;
            target.Insurancable = source.Insurancable;
            target.Marriage = source.Marriage;
            target.NotCash = source.NotCash;
            target.ONOFF = source.ONOFF;
            target.SelectAccount = source.SelectAccount;
            target.Subable = source.Subable;
            target.Sumable = source.Sumable;
            target.SumFishNoView = source.SumFishNoView;
            target.Taxable = source.Taxable;
            target.Timeshit = source.Timeshit;
            target.YearsBase = source.YearsBase;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblFeeBehoofDetailDto> ToDtos(this IEnumerable<TblFeeBehoofDetail> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblFeeBehoofDetailDto> ToDtosWithRelated(this IEnumerable<TblFeeBehoofDetail> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblFeeBehoofDetail> ToEntities(this IEnumerable<TblFeeBehoofDetailDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblFeeBehoofDetail source, TblFeeBehoofDetailDto target);

        static partial void OnEntityCreating(TblFeeBehoofDetailDto source, TblFeeBehoofDetail target);

    }

}
