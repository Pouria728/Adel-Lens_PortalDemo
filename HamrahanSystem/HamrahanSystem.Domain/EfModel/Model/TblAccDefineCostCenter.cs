
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblAccDefineCostCenter {

        public TblAccDefineCostCenter()
        {
            this.IsActive = 1;
            this.StateFormal = 4;
            this.TblAccDefineCostCenters_RefMasterKey_RefMasterCompany = new List<TblAccDefineCostCenter>();
            OnCreated();
        }

        public int AccDefineCostCenterId { get; set; }

        /// <summary>
        /// فعال
        /// </summary>
        public bool? Active { get; set; }

        /// <summary>
        /// کد مرکز هزینه
        /// </summary>
        public string CodeCostCenter { get; set; }

        public byte Company { get; set; }

        public byte? IndexTypeCostCenter { get; set; }

        public byte? IndexTypeTask { get; set; }

        public short? IsActive { get; set; }

        /// <summary>
        /// نام مرکز هزینه
        /// </summary>
        public string NameCostCenter { get; set; }

        /// <summary>
        /// نام مرکز هزینه
        /// </summary>
        public string NameCostCenterEN { get; set; }

        public int RecNo { get; set; }

        public int? RefMaster { get; set; }

        public byte? RefMasterCompany { get; set; }

        public int? RefMasterKey { get; set; }

        public int? RegistryKey { get; set; }

        public int? RNGroupFormal { get; set; }

        /// <summary>
        /// مرکز هزینه = 4
        /// </summary>
        public byte? StateFormal { get; set; }


        public virtual IList<TblAccDefineCostCenter> TblAccDefineCostCenters_RefMasterKey_RefMasterCompany { get; set; }


        public virtual TblAccDefineCostCenter TblAccDefineCostCenter_RefMasterKey_RefMasterCompany { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        public override bool Equals(object obj)
        {
          TblAccDefineCostCenter toCompare = obj as TblAccDefineCostCenter;
          if (toCompare == null)
          {
            return false;
          }

          if (!Object.Equals(this.Company, toCompare.Company))
            return false;
          if (!Object.Equals(this.RecNo, toCompare.RecNo))
            return false;

          return true;
        }

        public override int GetHashCode()
        {
          int hashCode = 13;
          hashCode = (hashCode * 7) + Company.GetHashCode();
          hashCode = (hashCode * 7) + RecNo.GetHashCode();
          return hashCode;
        }

        #endregion
    }

}
