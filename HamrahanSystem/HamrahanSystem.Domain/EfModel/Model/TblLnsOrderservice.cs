

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsOrderservice {

        public TblLnsOrderservice()
        {
            OnCreated();
        }

        public int? DefineServiceId { get; set; }

        public long? OrderId { get; set; }

        public long OrderServicesId { get; set; }

        public virtual TblLnsOrder TblLnsOrder { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
