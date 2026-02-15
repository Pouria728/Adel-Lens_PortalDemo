

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsCyl {

        public TblLnsCyl()
        {
            this.OrderId = 1;
            this.TblLnsOrderItems = new List<TblLnsOrderItem>();
            this.TblLnsSphCyls = new List<TblLnsSphCyl>();
            OnCreated();
        }

        public string? Code { get; set; }

        public int? Company { get; set; }

        public string? CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public int CylId { get; set; }

        public string? Description { get; set; }

        public short? IsActive { get; set; }

        public int? ModifiedBy { get; set; }

        public string? ModifiedDate { get; set; }

        public string? Name { get; set; }

        public int OrderId { get; set; }

        public virtual IList<TblLnsOrderItem> TblLnsOrderItems { get; set; }

        public virtual IList<TblLnsSphCyl> TblLnsSphCyls { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
