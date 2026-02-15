
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsCustomLensTypeMaterial
    {
        public TblLnsCustomLensTypeMaterial()
        {
            this.OrderId = 1;
            OnCreated();
        }

        public int CustomLensTypeMaterialId { get; set; }

        public int? DesignTypeId { get; set; }

        public int? LensIndexId { get; set; }

        public string? LensTypeName { get; set; }

        public string? MaterialName { get; set; }

        public int? DefineObjectId { get; set; }

        public int OrderId { get; set; }

        public short? IsActive { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }
}
