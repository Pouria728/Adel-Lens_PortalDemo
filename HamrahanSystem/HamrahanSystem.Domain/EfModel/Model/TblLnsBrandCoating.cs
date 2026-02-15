

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsBrandCoating {

        public TblLnsBrandCoating()
        {
            this.OrderId = 1;
            OnCreated();
        }

        public int BrandCoatingId { get; set; }

        public int? BrandId { get; set; }

        public int? CoatingId { get; set; }

        public bool? IsDefault { get; set; }

        public int OrderId { get; set; }

        public virtual TblLnsBrand TblLnsBrand { get; set; }

        public virtual TblLnsCoating TblLnsCoating { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
