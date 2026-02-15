

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsLensTypeRLensIndexR {

        public TblLnsLensTypeRLensIndexR()
        {
            this.OrderId = 1;
            this.TblLnsLensIndexRSphs = new List<TblLnsLensIndexRSph>();
            this.TblLnsOrders = new List<TblLnsOrder>();
            this.TblLnsOrderItems = new List<TblLnsOrderItem>();
            OnCreated();
        }

        public int? BrandLensTypeRId { get; set; }

        public int? LensIndexRId { get; set; }

        public int LensTypeRLensIndexRId { get; set; }

        public int OrderId { get; set; }

        public virtual IList<TblLnsLensIndexRSph> TblLnsLensIndexRSphs { get; set; }

        public virtual TblLnsLensIndexR TblLnsLensIndexR { get; set; }

        public virtual TblLnsBrandLensTypeR TblLnsBrandLensTypeR { get; set; }

        public virtual IList<TblLnsOrder> TblLnsOrders { get; set; }

        public virtual IList<TblLnsOrderItem> TblLnsOrderItems { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
