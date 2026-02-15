

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsOrderItem {

        public TblLnsOrderItem()
        {
            OnCreated();
        }

        public int? Axis { get; set; }

        public short? BaseOfPrism { get; set; }

        public int? BrandId { get; set; }

        public int? BrandLensTypeRId { get; set; }

        public string? ConsumerTitle { get; set; }

        public int? CorrLRowNumber { get; set; }

        public long? CorrProductId { get; set; }

        public int? Cutting { get; set; }

        public int? CylId { get; set; }

        public short? Dc { get; set; }

        public int? DefineObjectId { get; set; }

        public string? Description { get; set; }

        public short? Dia { get; set; }

        public decimal? Discount { get; set; }

        public int? EyeType { get; set; }

        public decimal? Fee { get; set; }

        public float? Fitting { get; set; }

        public float? Ipd { get; set; }

        public short? IsRight { get; set; }

        public short? ItemAdd { get; set; }

        public int? LensIndexRSphId { get; set; }

        public int? LensTypeRLensIndexRId { get; set; }

        public bool? NeedTools { get; set; }

        public string? Optician { get; set; }

        public long? OrderId { get; set; }

        public long OrderItemId { get; set; }

        public short? Position { get; set; }

        public decimal? Price { get; set; }

        public short? Prism { get; set; }

        public int? ProvidedQuantity { get; set; }

        public int? Quantity { get; set; }

        public string? RowNumber { get; set; }

        public DateTime? RowVersion { get; set; }

        public int? SphCylId { get; set; }

        public int? SphId { get; set; }

		public virtual TblClrDefineObject TblClrDefineObject { get; set; }
		public virtual TblLnsOrder TblLnsOrder { get; set; }

        public virtual TblLnsSph TblLnsSph { get; set; }

        public virtual TblLnsCyl TblLnsCyl { get; set; }

        public virtual TblLnsLensIndexRSph TblLnsLensIndexRSph { get; set; }

        public virtual TblLnsSphCyl TblLnsSphCyl { get; set; }

        public virtual TblLnsBrand TblLnsBrand { get; set; }

        public virtual TblLnsLensTypeRLensIndexR TblLnsLensTypeRLensIndexR { get; set; }

        public virtual TblLnsBrandLensTypeR TblLnsBrandLensTypeR { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
