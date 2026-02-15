

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsBrandLensType {

        public TblLnsBrandLensType()
        {
            this.OrderId = 1;
            this.TblLnsBrandDesignTypes = new List<TblLnsBrandDesignType>();
            this.TblLnsOrders = new List<TblLnsOrder>();
            OnCreated();
        }

        public int? BrandId { get; set; }

        public int BrandLensTypeId { get; set; }

        public int? LensTypeId { get; set; }

        public int OrderId { get; set; }

        public virtual IList<TblLnsBrandDesignType> TblLnsBrandDesignTypes { get; set; }

        public virtual TblLnsLensType TblLnsLensType { get; set; }

        public virtual TblLnsBrand TblLnsBrand { get; set; }

        public virtual IList<TblLnsOrder> TblLnsOrders { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
