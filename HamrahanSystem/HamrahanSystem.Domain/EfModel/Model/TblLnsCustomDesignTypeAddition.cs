
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsCustomDesignTypeAddition
    {
        public TblLnsCustomDesignTypeAddition()
        {
            this.OrderId = 1;
            OnCreated();
        }

        public int CustomDesignTypeAdditionId { get; set; }

        public int? DesignTypeId { get; set; }

        public int? DefineObjectId { get; set; }

        public decimal? AdditionValue { get; set; }

        public int OrderId { get; set; }

        public short? IsActive { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }
}
