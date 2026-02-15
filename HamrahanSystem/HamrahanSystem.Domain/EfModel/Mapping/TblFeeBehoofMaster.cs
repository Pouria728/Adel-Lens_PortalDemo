

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblFeeBehoofMaster {

        public TblFeeBehoofMaster()
        {
            this.TblFeeBehoofDetails = new List<TblFeeBehoofDetail>();
            OnCreated();
        }

        public byte? AgentTypeIndex { get; set; }

        public int BehoofCode { get; set; }

        public byte? BehoofTypeIndex { get; set; }

        public byte Company { get; set; }

        public string EnglishBehoof { get; set; }

        public string FarsiBehoof { get; set; }

        public short? INTypeIndex { get; set; }

        public string LatinBehoof { get; set; }

        public int? LinkedCodingTable { get; set; }

        public byte? ListType { get; set; }

        public string Param { get; set; }


        public virtual IList<TblFeeBehoofDetail> TblFeeBehoofDetails { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        public override bool Equals(object obj)
        {
          TblFeeBehoofMaster toCompare = obj as TblFeeBehoofMaster;
          if (toCompare == null)
          {
            return false;
          }

          if (!Object.Equals(this.BehoofCode, toCompare.BehoofCode))
            return false;
          if (!Object.Equals(this.Company, toCompare.Company))
            return false;

          return true;
        }

        public override int GetHashCode()
        {
          int hashCode = 13;
          hashCode = (hashCode * 7) + BehoofCode.GetHashCode();
          hashCode = (hashCode * 7) + Company.GetHashCode();
          return hashCode;
        }

        #endregion
    }

}
