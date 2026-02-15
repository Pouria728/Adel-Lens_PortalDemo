

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsLensIndexRSph {

        public TblLnsLensIndexRSph()
        {
            this.OrderId = 1;
            this.TblLnsOrderItems = new List<TblLnsOrderItem>();
            this.TblLnsSphCyls = new List<TblLnsSphCyl>();
            OnCreated();
        }

        public int LensIndexRSphId { get; set; }

        public int? LensTypeRLensIndexRId { get; set; }

        public int OrderId { get; set; }

        public int? SphId { get; set; }

        public virtual TblLnsSph TblLnsSph { get; set; }

        public virtual TblLnsLensTypeRLensIndexR TblLnsLensTypeRLensIndexR { get; set; }

        public virtual IList<TblLnsOrderItem> TblLnsOrderItems { get; set; }

        public virtual IList<TblLnsSphCyl> TblLnsSphCyls { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
