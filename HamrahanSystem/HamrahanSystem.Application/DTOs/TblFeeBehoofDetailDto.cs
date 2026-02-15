

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblFeeBehoofDetailDto
    {
        #region Constructors

        public TblFeeBehoofDetailDto() {
        }

        public TblFeeBehoofDetailDto(bool? aggregation, bool? aggregationScore, int behoofCode, short? calculateNO, DateTime changeDate, byte changeIndex, string changeUser, byte codeInsurance, byte company, bool? dayCounter, bool? depository, byte employeeCode, bool? fiatPrintView, bool? fiatView, bool? fishView, short? fishViewNO, bool? fixCostProject, string formulCeil, string formulCeilDes, string formulCeilMerge, string formulDesList, string formulList, string formulMerge, short? formViewNO, string fromDate, bool? hasCoef, bool? incomeNo, bool? insurancable, bool? marriage, bool? notCash, bool? oNOFF, bool? selectAccount, bool? subable, bool? sumable, bool? sumFishNoView, bool? taxable, bool? timeshit, bool? yearsBase, TblFeeBehoofMasterDto tblFeeBehoofMaster) {

          this.Aggregation = aggregation;
          this.AggregationScore = aggregationScore;
          this.BehoofCode = behoofCode;
          this.CalculateNO = calculateNO;
          this.ChangeDate = changeDate;
          this.ChangeIndex = changeIndex;
          this.ChangeUser = changeUser;
          this.CodeInsurance = codeInsurance;
          this.Company = company;
          this.DayCounter = dayCounter;
          this.Depository = depository;
          this.EmployeeCode = employeeCode;
          this.FiatPrintView = fiatPrintView;
          this.FiatView = fiatView;
          this.FishView = fishView;
          this.FishViewNO = fishViewNO;
          this.FixCostProject = fixCostProject;
          this.FormulCeil = formulCeil;
          this.FormulCeilDes = formulCeilDes;
          this.FormulCeilMerge = formulCeilMerge;
          this.FormulDesList = formulDesList;
          this.FormulList = formulList;
          this.FormulMerge = formulMerge;
          this.FormViewNO = formViewNO;
          this.FromDate = fromDate;
          this.HasCoef = hasCoef;
          this.IncomeNo = incomeNo;
          this.Insurancable = insurancable;
          this.Marriage = marriage;
          this.NotCash = notCash;
          this.ONOFF = oNOFF;
          this.SelectAccount = selectAccount;
          this.Subable = subable;
          this.Sumable = sumable;
          this.SumFishNoView = sumFishNoView;
          this.Taxable = taxable;
          this.Timeshit = timeshit;
          this.YearsBase = yearsBase;
          this.TblFeeBehoofMaster = tblFeeBehoofMaster;
        }

        #endregion

        #region Properties

        public bool? Aggregation { get; set; }

        public bool? AggregationScore { get; set; }

        public int BehoofCode { get; set; }

        public short? CalculateNO { get; set; }

        public DateTime ChangeDate { get; set; }

        public byte ChangeIndex { get; set; }

        public string ChangeUser { get; set; }

        public byte CodeInsurance { get; set; }

        public byte Company { get; set; }

        public bool? DayCounter { get; set; }

        public bool? Depository { get; set; }

        public byte EmployeeCode { get; set; }

        public bool? FiatPrintView { get; set; }

        public bool? FiatView { get; set; }

        public bool? FishView { get; set; }

        public short? FishViewNO { get; set; }

        public bool? FixCostProject { get; set; }

        public string FormulCeil { get; set; }

        public string FormulCeilDes { get; set; }

        public string FormulCeilMerge { get; set; }

        public string FormulDesList { get; set; }

        public string FormulList { get; set; }

        public string FormulMerge { get; set; }

        public short? FormViewNO { get; set; }

        public string FromDate { get; set; }

        public bool? HasCoef { get; set; }

        public bool? IncomeNo { get; set; }

        public bool? Insurancable { get; set; }

        public bool? Marriage { get; set; }

        public bool? NotCash { get; set; }

        public bool? ONOFF { get; set; }

        public bool? SelectAccount { get; set; }

        public bool? Subable { get; set; }

        public bool? Sumable { get; set; }

        public bool? SumFishNoView { get; set; }

        public bool? Taxable { get; set; }

        public bool? Timeshit { get; set; }

        public bool? YearsBase { get; set; }

        #endregion

        #region Navigation Properties

        public TblFeeBehoofMasterDto TblFeeBehoofMaster { get; set; }

        #endregion
    }

}
