

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsLensType {

        public TblLnsLensType()
        {
            this.OrderId = 1;
            this.IsCorridor = false;
            this.IsSpecial = false;
            this.TblLnsBrandLensTypes = new List<TblLnsBrandLensType>();
            this.TblLnsOrders = new List<TblLnsOrder>();
            OnCreated();
        }

        public string? Code { get; set; }

        public int? Company { get; set; }

        public int? BrandId { get; set; }

        public string? CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string? Description { get; set; }

        public short? IsActive { get; set; }

        public bool IsCorridor { get; set; }

        public bool IsSpecial { get; set; }

        public int LensTypeId { get; set; }

        public int? ModifiedBy { get; set; }

        public string? ModifiedDate { get; set; }

        public string? Name { get; set; }

        public int OrderId { get; set; }

        public virtual IList<TblLnsBrandLensType> TblLnsBrandLensTypes { get; set; }

        public virtual IList<TblLnsOrder> TblLnsOrders { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
