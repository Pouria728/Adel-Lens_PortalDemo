

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsOrder {

        public TblLnsOrder()
        {
            this.TblLnsOrderItems = new List<TblLnsOrderItem>();
            this.TblLnsOrderservices = new List<TblLnsOrderservice>();
			this.TblWfwOrderProcesses = new List<TblWfwOrderProcess>();
			OnCreated();
        }
        /// <summary>
        /// شماره فاکتور
        /// </summary>
        public string? FactorNo { get; set; }
        public int? AccDefineValutaId { get; set; }

        public string? BeforeLensSpec { get; set; }

        public int? BeforeLensType { get; set; }

        public int? BrandDesignTypeId { get; set; }

        public int? BrandId { get; set; }

        public int? BrandLensTypeId { get; set; }

        public int? BrandLensTypeRId { get; set; }

        public int? CoatingId { get; set; }

        public string? Color { get; set; }

        public decimal? ColorBottom { get; set; }

        public int? ColoringTypeId { get; set; }

        public decimal? ColorRatio { get; set; }

        public decimal? ColorRatioTop { get; set; }

        public int? Company { get; set; }

        public string? Consumer { get; set; }

        public int? Corridor { get; set; }

        public string? CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string? DateMustDelivered { get; set; }

        public float? Dbl { get; set; }

        public int? DefineCustomerId { get; set; }

        public int? DefineObjectId { get; set; }

        public string? Description { get; set; }

        public int? DesignTypeId { get; set; }

        public int? DesignTypeLensIndexId { get; set; }

        public float? EffectiveDiameter { get; set; }

        public float? Ffa { get; set; }

        public decimal? FittingL { get; set; }

        public decimal? FittingR { get; set; }

        public int? FrameTypeId { get; set; }

        public bool? HasCoating { get; set; }

        public bool? HasColor { get; set; }

        public float? HBox { get; set; }

        public int? IndexDocument { get; set; }

        public int? InfoCustomerId { get; set; }

        public decimal? IpdL { get; set; }

        public decimal? IpdR { get; set; }

        public int? ItemBase { get; set; }

        public bool? LabelLPrint { get; set; }

        public bool? LabelLRPrint { get; set; }

        public bool? LabelRPrint { get; set; }

        public int? LensIndexId { get; set; }

        public int? LensIndexMaterialTypeId { get; set; }

        public int? LensTypeId { get; set; }

        public int? LensTypeRLensIndexRId { get; set; }

        public int? MaterialTypeId { get; set; }

        public int? ModifiedBy { get; set; }

        public string? ModifiedDate { get; set; }

        public string? Number { get; set; }

        public int? OrderClass { get; set; }

        public long OrderId { get; set; }

        public bool? OrderPrint { get; set; }

        public float? Panto { get; set; }

        public decimal? Price { get; set; }

        public bool? PrintLabel { get; set; }

        public bool? PrintWarranty { get; set; }

        public string? StoreName { get; set; }

        public string? TrackingCode { get; set; }

        public float? VBox { get; set; }

        public float? Vd { get; set; }

        public bool? WarrantyLRPrint { get; set; }
		public int StatusId { get; set; }

		public virtual TblLnsBrandDesignType TblLnsBrandDesignType { get; set; }

        public virtual TblLnsDesignTypeLensIndex TblLnsDesignTypeLensIndex { get; set; }

        public virtual TblLnsLensIndexMaterialType TblLnsLensIndexMaterialType { get; set; }

        public virtual TblLnsBrandLensTypeR TblLnsBrandLensTypeR { get; set; }

        public virtual TblLnsLensTypeRLensIndexR TblLnsLensTypeRLensIndexR { get; set; }

        public virtual TblLnsFrameType TblLnsFrameType { get; set; }

        public virtual TblLnsBrand TblLnsBrand { get; set; }

        public virtual TblLnsLensType TblLnsLensType { get; set; }

        public virtual TblLnsDesignType TblLnsDesignType { get; set; }

        public virtual TblLnsLensIndex TblLnsLensIndex { get; set; }

        public virtual TblLnsMaterialType TblLnsMaterialType { get; set; }

        public virtual TblLnsCoating TblLnsCoating { get; set; }

        public virtual TblLnsColoringType TblLnsColoringType { get; set; }

        public virtual TblLnsBrandLensType TblLnsBrandLensType { get; set; }

        public virtual IList<TblLnsOrderItem> TblLnsOrderItems { get; set; }

        public virtual IList<TblLnsOrderservice> TblLnsOrderservices { get; set; }
		public virtual IList<TblWfwOrderProcess> TblWfwOrderProcesses { get; set; }

		#region Extensibility Method Definitions

		partial void OnCreated();

        #endregion
    }

}
