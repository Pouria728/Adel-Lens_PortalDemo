

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsSph {

        public TblLnsSph()
        {
            this.OrderId = 1;
            this.TblLnsLensIndexRSphs = new List<TblLnsLensIndexRSph>();
            this.TblLnsOrderItems = new List<TblLnsOrderItem>();
            OnCreated();
        }

        public string? Code { get; set; }

        public int? Company { get; set; }

        public string? CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string? Description { get; set; }

        public short? IsActive { get; set; }

        public int? ModifiedBy { get; set; }

        public string? ModifiedDate { get; set; }

        public string? Name { get; set; }

        public int OrderId { get; set; }

        public int SphId { get; set; }

        public virtual IList<TblLnsLensIndexRSph> TblLnsLensIndexRSphs { get; set; }

        public virtual IList<TblLnsOrderItem> TblLnsOrderItems { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
