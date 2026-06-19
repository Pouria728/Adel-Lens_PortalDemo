
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsCustomLensTypeCoating
    {
        public TblLnsCustomLensTypeCoating()
        {
            this.OrderId = 1;
            OnCreated();
        }

        public int CustomLensTypeCoatingId { get; set; }

        public int? DesignTypeId { get; set; }

        public string? LensTypeName { get; set; }

        public string? CoatingName { get; set; }

        public bool? IsDefault { get; set; }

        public int OrderId { get; set; }

        public short? IsActive { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }
}
