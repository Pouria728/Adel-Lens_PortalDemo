

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsDesignType {

        public TblLnsDesignType()
        {
            this.OrderId = 1;
            this.IsSpecial = false;
            this.SphPlus = false;
            this.SphMinus = false;
            this.Addition = false;
            this.AdditionValue = null;
            this.TblLnsBrandDesignTypes = new List<TblLnsBrandDesignType>();
            this.TblLnsOrders = new List<TblLnsOrder>();
            OnCreated();
        }

        public string? Code { get; set; }

        public int? Company { get; set; }

        public int? LensTypeId { get; set; }

        public string? CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string? Description { get; set; }

        public int DesignTypeId { get; set; }

        public short? IsActive { get; set; }

        public bool IsSpecial { get; set; }

        public bool SphPlus { get; set; }

        public bool SphMinus { get; set; }

        public bool Addition { get; set; }

        public decimal? AdditionValue { get; set; }

        public int? ModifiedBy { get; set; }

        public string? ModifiedDate { get; set; }

        public string? Name { get; set; }

        public int OrderId { get; set; }

        public virtual IList<TblLnsBrandDesignType> TblLnsBrandDesignTypes { get; set; }

        public virtual IList<TblLnsOrder> TblLnsOrders { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
