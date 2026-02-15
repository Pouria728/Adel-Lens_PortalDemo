
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsDesignTypeLensIndex {

        public TblLnsDesignTypeLensIndex()
        {
            this.OrderId = 1;
            this.TblLnsLensIndexMaterialTypes = new List<TblLnsLensIndexMaterialType>();
            this.TblLnsOrders = new List<TblLnsOrder>();
            OnCreated();
        }

        public int? BrandDesignTypeId { get; set; }

        public int DesignTypeLensIndexId { get; set; }

        public int? LensIndexId { get; set; }

        public int OrderId { get; set; }

        public virtual TblLnsLensIndex TblLnsLensIndex { get; set; }

        public virtual TblLnsBrandDesignType TblLnsBrandDesignType { get; set; }

        public virtual IList<TblLnsLensIndexMaterialType> TblLnsLensIndexMaterialTypes { get; set; }

        public virtual IList<TblLnsOrder> TblLnsOrders { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
