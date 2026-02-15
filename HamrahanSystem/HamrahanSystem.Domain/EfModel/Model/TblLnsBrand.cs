

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsBrand {

        public TblLnsBrand()
        {
            this.IsSpecial = false;
            this.IsStock = false;
            this.IsStockGranty = false;
            this.OrderId = 1;
            this.TblLnsBrandCoatings = new List<TblLnsBrandCoating>();
            this.TblLnsBrandLensTypes = new List<TblLnsBrandLensType>();
            this.TblLnsBrandLensTypeRs = new List<TblLnsBrandLensTypeR>();
            this.TblLnsOrders = new List<TblLnsOrder>();
            this.TblLnsOrderItems = new List<TblLnsOrderItem>();
            OnCreated();
        }

        public int BrandId { get; set; }

        public string? Code { get; set; }

        public int? Company { get; set; }

        public string? CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string? Description { get; set; }

        public short? IsActive { get; set; }
        public bool IsSpecial { get; set; }

        public bool IsStock { get; set; }

        public bool IsStockGranty { get; set; }

        public int? ModifiedBy { get; set; }

        public string? ModifiedDate { get; set; }

        public string? Name { get; set; }

        public int OrderId { get; set; }

        public virtual IList<TblLnsBrandCoating> TblLnsBrandCoatings { get; set; }

        public virtual IList<TblLnsBrandLensType> TblLnsBrandLensTypes { get; set; }

        public virtual IList<TblLnsBrandLensTypeR> TblLnsBrandLensTypeRs { get; set; }

        public virtual IList<TblLnsOrder> TblLnsOrders { get; set; }

        public virtual IList<TblLnsOrderItem> TblLnsOrderItems { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
