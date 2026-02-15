

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblFeeBehoofDetail {

        public TblFeeBehoofDetail()
        {
            this.ChangeIndex = 0;
            this.FixCostProject = false;
            OnCreated();
        }

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


        public virtual TblFeeBehoofMaster TblFeeBehoofMaster { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        public override bool Equals(object obj)
        {
          TblFeeBehoofDetail toCompare = obj as TblFeeBehoofDetail;
          if (toCompare == null)
          {
            return false;
          }

          if (!Object.Equals(this.BehoofCode, toCompare.BehoofCode))
            return false;
          if (!Object.Equals(this.CodeInsurance, toCompare.CodeInsurance))
            return false;
          if (!Object.Equals(this.Company, toCompare.Company))
            return false;
          if (!Object.Equals(this.EmployeeCode, toCompare.EmployeeCode))
            return false;
          if (!Object.Equals(this.FromDate, toCompare.FromDate))
            return false;

          return true;
        }

        public override int GetHashCode()
        {
          int hashCode = 13;
          hashCode = (hashCode * 7) + BehoofCode.GetHashCode();
          hashCode = (hashCode * 7) + CodeInsurance.GetHashCode();
          hashCode = (hashCode * 7) + Company.GetHashCode();
          hashCode = (hashCode * 7) + EmployeeCode.GetHashCode();
          hashCode = (hashCode * 7) + FromDate.GetHashCode();
          return hashCode;
        }

        #endregion
    }

}
