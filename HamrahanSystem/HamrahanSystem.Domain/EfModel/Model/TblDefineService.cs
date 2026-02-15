

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblDefineService {

        public TblDefineService()
        {
            this.IsActive = 1;
            this.KindService = 2;
            OnCreated();
        }

        public bool Active { get; set; }

        public byte Company { get; set; }

        public int DefineServiceId { get; set; }

        public short? IsActive { get; set; }

        public int KindService { get; set; }

        public int RecNo { get; set; }

        public int? RNScruple { get; set; }

        public string? ServiceCode { get; set; }

        public string? ServiceName { get; set; }

        public string? ServiceNameEN { get; set; }

		

		#region Extensibility Method Definitions

		partial void OnCreated();

        public override bool Equals(object obj)
        {
          TblDefineService toCompare = obj as TblDefineService;
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
