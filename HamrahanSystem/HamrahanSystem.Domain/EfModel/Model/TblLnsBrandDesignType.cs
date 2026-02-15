

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsBrandDesignType {

        public TblLnsBrandDesignType()
        {
            this.OrderId = 1;
            this.TblLnsDesignTypeLensIndices = new List<TblLnsDesignTypeLensIndex>();
            this.TblLnsOrders = new List<TblLnsOrder>();
            OnCreated();
        }

        public int BrandDesignTypeId { get; set; }

        public int? BrandLensTypeId { get; set; }

        public int? DesignTypeId { get; set; }

        public int OrderId { get; set; }

        public virtual TblLnsDesignType TblLnsDesignType { get; set; }

        public virtual TblLnsBrandLensType TblLnsBrandLensType { get; set; }

        public virtual IList<TblLnsDesignTypeLensIndex> TblLnsDesignTypeLensIndices { get; set; }

        public virtual IList<TblLnsOrder> TblLnsOrders { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
