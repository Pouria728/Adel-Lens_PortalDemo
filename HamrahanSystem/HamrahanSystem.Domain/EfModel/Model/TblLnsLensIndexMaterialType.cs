
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsLensIndexMaterialType {

        public TblLnsLensIndexMaterialType()
        {
            this.TblLnsOrders = new List<TblLnsOrder>();
            OnCreated();
        }

        public int? DefineObjectId { get; set; }

        public int? DesignTypeLensIndexId { get; set; }

        public int LensIndexMaterialTypeId { get; set; }

        public int? MaterialTypeId { get; set; }

        public virtual TblLnsDesignTypeLensIndex TblLnsDesignTypeLensIndex { get; set; }

        public virtual TblLnsMaterialType TblLnsMaterialType { get; set; }

        public virtual IList<TblLnsOrder> TblLnsOrders { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
