

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsSphCyl {

        public TblLnsSphCyl()
        {
            this.OrderId = 1;
            this.TblLnsOrderItems = new List<TblLnsOrderItem>();
            OnCreated();
        }

        public int? CylId { get; set; }

        public int? DefineObjectId { get; set; }

        public int? LensIndexRSphId { get; set; }

        public int OrderId { get; set; }

        public int SphCylId { get; set; }

        public int? Stock { get; set; }

        public virtual IList<TblLnsOrderItem> TblLnsOrderItems { get; set; }

        public virtual TblLnsCyl TblLnsCyl { get; set; }
		public virtual TblClrDefineObject TblClrDefineObject { get; set; }

		public virtual TblLnsLensIndexRSph TblLnsLensIndexRSph { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
