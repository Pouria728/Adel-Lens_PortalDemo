

using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsOrderDto
    {
        #region Constructors

        public TblLnsOrderDto() {
        }

        public TblLnsOrderDto(int? accDefineValutaId, string beforeLensSpec, int? beforeLensType, int? brandDesignTypeId, int? brandId, int? brandLensTypeId, int? brandLensTypeRId, int? coatingId, string color, decimal? colorBottom, int? coloringTypeId, decimal? colorRatio, decimal? colorRatioTop, int? company, string consumer, int? corridor, string createDate, int? createdBy, string dateMustDelivered, float? dbl, int? defineCustomerId, int? defineObjectId, string description, int? designTypeId, int? designTypeLensIndexId, float? effectiveDiameter, float? ffa, decimal? fittingL, decimal? fittingR, int? frameTypeId, bool? hasCoating, bool? hasColor, float? hBox, int? indexDocument, int? infoCustomerId, decimal? ipdL, decimal? ipdR, int? itemBase, bool? labelLPrint, bool? labelLRPrint, bool? labelRPrint, int? lensIndexId, int? lensIndexMaterialTypeId, int? lensTypeId, int? lensTypeRLensIndexRId, int? materialTypeId, int? modifiedBy, string modifiedDate, string number, int? orderClass, long orderId, bool? orderPrint, float? panto, decimal? price, bool? printLabel, bool? printWarranty, string storeName, string trackingCode, float? vBox, float? vd, bool? warrantyLRPrint, int statusId, TblLnsBrandDesignTypeDto tblLnsBrandDesignType, TblLnsDesignTypeLensIndexDto tblLnsDesignTypeLensIndex, TblLnsLensIndexMaterialTypeDto tblLnsLensIndexMaterialType, TblLnsBrandLensTypeRDto tblLnsBrandLensTypeR, TblLnsLensTypeRLensIndexRDto tblLnsLensTypeRLensIndexR, TblLnsFrameTypeDto tblLnsFrameType, TblLnsBrandDto tblLnsBrand, TblLnsLensTypeDto tblLnsLensType, TblLnsDesignTypeDto tblLnsDesignType, TblLnsLensIndexDto tblLnsLensIndex, TblLnsMaterialTypeDto tblLnsMaterialType, TblLnsCoatingDto tblLnsCoating, TblLnsColoringTypeDto tblLnsColoringType, TblLnsBrandLensTypeDto tblLnsBrandLensType, List<TblLnsOrderItemDto> tblLnsOrderItems, List<TblLnsOrderserviceDto> tblLnsOrderservices, List<TblWfwOrderProcessDto> tblWfwOrderProcesses, string factorNo) {

            this.FactorNo = factorNo;
          this.AccDefineValutaId = accDefineValutaId;
          this.BeforeLensSpec = beforeLensSpec;
          this.BeforeLensType = beforeLensType;
          this.BrandDesignTypeId = brandDesignTypeId;
          this.BrandId = brandId;
          this.BrandLensTypeId = brandLensTypeId;
          this.BrandLensTypeRId = brandLensTypeRId;
          this.CoatingId = coatingId;
          this.Color = color;
          this.ColorBottom = colorBottom;
          this.ColoringTypeId = coloringTypeId;
          this.ColorRatio = colorRatio;
          this.ColorRatioTop = colorRatioTop;
          this.Company = company;
          this.Consumer = consumer;
          this.Corridor = corridor;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.DateMustDelivered = dateMustDelivered;
          this.Dbl = dbl;
          this.DefineCustomerId = defineCustomerId;
          this.DefineObjectId = defineObjectId;
          this.Description = description;
          this.DesignTypeId = designTypeId;
          this.DesignTypeLensIndexId = designTypeLensIndexId;
          this.EffectiveDiameter = effectiveDiameter;
          this.Ffa = ffa;
          this.FittingL = fittingL;
          this.FittingR = fittingR;
          this.FrameTypeId = frameTypeId;
          this.HasCoating = hasCoating;
          this.HasColor = hasColor;
          this.HBox = hBox;
          this.IndexDocument = indexDocument;
          this.InfoCustomerId = infoCustomerId;
          this.IpdL = ipdL;
          this.IpdR = ipdR;
          this.ItemBase = itemBase;
          this.LabelLPrint = labelLPrint;
          this.LabelLRPrint = labelLRPrint;
          this.LabelRPrint = labelRPrint;
          this.LensIndexId = lensIndexId;
          this.LensIndexMaterialTypeId = lensIndexMaterialTypeId;
          this.LensTypeId = lensTypeId;
          this.LensTypeRLensIndexRId = lensTypeRLensIndexRId;
          this.MaterialTypeId = materialTypeId;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Number = number;
          this.OrderClass = orderClass;
          this.OrderId = orderId;
          this.OrderPrint = orderPrint;
          this.Panto = panto;
          this.Price = price;
          this.PrintLabel = printLabel;
          this.PrintWarranty = printWarranty;
          this.StoreName = storeName;
          this.TrackingCode = trackingCode;
          this.VBox = vBox;
          this.Vd = vd;
          this.WarrantyLRPrint = warrantyLRPrint;
          this.TblLnsBrandDesignType = tblLnsBrandDesignType;
          this.TblLnsDesignTypeLensIndex = tblLnsDesignTypeLensIndex;
          this.TblLnsLensIndexMaterialType = tblLnsLensIndexMaterialType;
          this.TblLnsBrandLensTypeR = tblLnsBrandLensTypeR;
          this.TblLnsLensTypeRLensIndexR = tblLnsLensTypeRLensIndexR;
          this.TblLnsFrameType = tblLnsFrameType;
          this.TblLnsBrand = tblLnsBrand;
          this.TblLnsLensType = tblLnsLensType;
          this.TblLnsDesignType = tblLnsDesignType;
          this.TblLnsLensIndex = tblLnsLensIndex;
          this.TblLnsMaterialType = tblLnsMaterialType;
          this.TblLnsCoating = tblLnsCoating;
          this.TblLnsColoringType = tblLnsColoringType;
          this.TblLnsBrandLensType = tblLnsBrandLensType;
          this.TblLnsOrderItems = tblLnsOrderItems;
          this.TblLnsOrderservices = tblLnsOrderservices;
            this.StatusId = statusId;
			this.TblWfwOrderProcesses = tblWfwOrderProcesses;
		}

        #endregion

        #region Properties
        /// <summary>
        /// شماره فاکتور
        /// </summary>
        public string? FactorNo { get; set; }
        public int? AccDefineValutaId { get; set; }

        public string BeforeLensSpec { get; set; }

        public int? BeforeLensType { get; set; }

        public int? BrandDesignTypeId { get; set; }

        public int? BrandId { get; set; }

        public int? BrandLensTypeId { get; set; }

        public int? BrandLensTypeRId { get; set; }

        public int? CoatingId { get; set; }

        public string Color { get; set; }

        public decimal? ColorBottom { get; set; }

        public int? ColoringTypeId { get; set; }

        public decimal? ColorRatio { get; set; }

        public decimal? ColorRatioTop { get; set; }

        public int? Company { get; set; }

        public string Consumer { get; set; }

        public int? Corridor { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string DateMustDelivered { get; set; }

        public float? Dbl { get; set; }

        public int? DefineCustomerId { get; set; }

        public int? DefineObjectId { get; set; }

        public string Description { get; set; }

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

        public string ModifiedDate { get; set; }

        public string Number { get; set; }

        public int? OrderClass { get; set; }

        public long OrderId { get; set; }

        public bool? OrderPrint { get; set; }

        public float? Panto { get; set; }

        public decimal? Price { get; set; }

        public bool? PrintLabel { get; set; }

        public bool? PrintWarranty { get; set; }

        public string StoreName { get; set; }

        public string TrackingCode { get; set; }

        public float? VBox { get; set; }

        public float? Vd { get; set; }

        public bool? WarrantyLRPrint { get; set; }
		public int StatusId { get; set; }

		#endregion

		#region Navigation Properties

		public TblLnsBrandDesignTypeDto TblLnsBrandDesignType { get; set; }

        public TblLnsDesignTypeLensIndexDto TblLnsDesignTypeLensIndex { get; set; }

        public TblLnsLensIndexMaterialTypeDto TblLnsLensIndexMaterialType { get; set; }

        public TblLnsBrandLensTypeRDto TblLnsBrandLensTypeR { get; set; }

        public TblLnsLensTypeRLensIndexRDto TblLnsLensTypeRLensIndexR { get; set; }

        public TblLnsFrameTypeDto TblLnsFrameType { get; set; }

        public TblLnsBrandDto TblLnsBrand { get; set; }

        public TblLnsLensTypeDto TblLnsLensType { get; set; }

        public TblLnsDesignTypeDto TblLnsDesignType { get; set; }

        public TblLnsLensIndexDto TblLnsLensIndex { get; set; }

        public TblLnsMaterialTypeDto TblLnsMaterialType { get; set; }

        public TblLnsCoatingDto TblLnsCoating { get; set; }

        public TblLnsColoringTypeDto TblLnsColoringType { get; set; }

        public TblLnsBrandLensTypeDto TblLnsBrandLensType { get; set; }

        public List<TblLnsOrderItemDto> TblLnsOrderItems { get; set; }

        public List<TblLnsOrderserviceDto> TblLnsOrderservices { get; set; }
		public List<TblWfwOrderProcessDto> TblWfwOrderProcesses { get; set; }

		#endregion
	}

}
