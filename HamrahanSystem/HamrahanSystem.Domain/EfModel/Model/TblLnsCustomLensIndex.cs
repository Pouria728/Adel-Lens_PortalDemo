using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsCustomLensIndex
    {
        public TblLnsCustomLensIndex()
        {
            this.OrderId = 1;
            this.ColoringTypeStatus = "ندارد";
            OnCreated();
        }

        public int CustomLensIndexId { get; set; }

        public int? DesignTypeId { get; set; }

        public string? LensIndexName { get; set; }

        public string? ColoringTypeStatus { get; set; }

        public int OrderId { get; set; }

        public short? IsActive { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }
}
