
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsBrandLensTypeR {

        public TblLnsBrandLensTypeR()
        {
            this.OrderId = 1;
            this.TblLnsLensTypeRLensIndexRs = new List<TblLnsLensTypeRLensIndexR>();
            this.TblLnsOrders = new List<TblLnsOrder>();
            this.TblLnsOrderItems = new List<TblLnsOrderItem>();
            OnCreated();
        }

        public int? BrandId { get; set; }

        public int BrandLensTypeRId { get; set; }

        public int? LensTypeRId { get; set; }

        public int OrderId { get; set; }

        public virtual TblLnsBrand TblLnsBrand { get; set; }

        public virtual TblLnsLensTypeR TblLnsLensTypeR { get; set; }

        public virtual IList<TblLnsLensTypeRLensIndexR> TblLnsLensTypeRLensIndexRs { get; set; }

        public virtual IList<TblLnsOrder> TblLnsOrders { get; set; }

        public virtual IList<TblLnsOrderItem> TblLnsOrderItems { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
