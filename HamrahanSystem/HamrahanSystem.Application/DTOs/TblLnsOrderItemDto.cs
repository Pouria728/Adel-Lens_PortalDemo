

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsOrderItemDto
    {
        #region Constructors

        public TblLnsOrderItemDto() {
        }

        public TblLnsOrderItemDto(int? axis, short? baseOfPrism, int? brandId, int? brandLensTypeRId, string consumerTitle, int? corrLRowNumber, long? corrProductId, int? cutting, int? cylId, short? dc, int? defineObjectId, string description, short? dia, decimal? discount, int? eyeType, decimal? fee, float? fitting, float? ipd, short? isRight, short? itemAdd, int? lensIndexRSphId, int? lensTypeRLensIndexRId, bool? needTools, string optician, long? orderId, long orderItemId, short? position, decimal? price, short? prism, int? providedQuantity, int? quantity, string rowNumber, System.DateTime? rowVersion, int? sphCylId, int? sphId, TblLnsOrderDto tblLnsOrder, TblLnsSphDto tblLnsSph, TblLnsCylDto tblLnsCyl, TblLnsLensIndexRSphDto tblLnsLensIndexRSph, TblLnsSphCylDto tblLnsSphCyl, TblLnsBrandDto tblLnsBrand, TblLnsLensTypeRLensIndexRDto tblLnsLensTypeRLensIndexR, TblLnsBrandLensTypeRDto tblLnsBrandLensTypeR, TblClrDefineObjectDto tblClrDefineObject) {

          this.Axis = axis;
          this.BaseOfPrism = baseOfPrism;
          this.BrandId = brandId;
          this.BrandLensTypeRId = brandLensTypeRId;
          this.ConsumerTitle = consumerTitle;
          this.CorrLRowNumber = corrLRowNumber;
          this.CorrProductId = corrProductId;
          this.Cutting = cutting;
          this.CylId = cylId;
          this.Dc = dc;
          this.DefineObjectId = defineObjectId;
          this.Description = description;
          this.Dia = dia;
          this.Discount = discount;
          this.EyeType = eyeType;
          this.Fee = fee;
          this.Fitting = fitting;
          this.Ipd = ipd;
          this.IsRight = isRight;
          this.ItemAdd = itemAdd;
          this.LensIndexRSphId = lensIndexRSphId;
          this.LensTypeRLensIndexRId = lensTypeRLensIndexRId;
          this.NeedTools = needTools==null?false:needTools.Value;
          this.Optician = optician;
          this.OrderId = orderId;
          this.OrderItemId = orderItemId;
          this.Position = position;
          this.Price = price;
          this.Prism = prism;
          this.ProvidedQuantity = providedQuantity;
          this.Quantity = quantity;
          this.RowNumber = rowNumber;
          this.RowVersion = rowVersion;
          this.SphCylId = sphCylId;
          this.SphId = sphId;
          this.TblLnsOrder = tblLnsOrder;
          this.TblLnsSph = tblLnsSph;
          this.TblLnsCyl = tblLnsCyl;
          this.TblLnsLensIndexRSph = tblLnsLensIndexRSph;
          this.TblLnsSphCyl = tblLnsSphCyl;
          this.TblLnsBrand = tblLnsBrand;
          this.TblLnsLensTypeRLensIndexR = tblLnsLensTypeRLensIndexR;
          this.TblLnsBrandLensTypeR = tblLnsBrandLensTypeR;
			this.TblClrDefineObject = tblClrDefineObject;
		}

        #endregion

        #region Properties

        public int? Axis { get; set; }

        public short? BaseOfPrism { get; set; }

        public int? BrandId { get; set; }

        public int? BrandLensTypeRId { get; set; }

        public string ConsumerTitle { get; set; }

        public int? CorrLRowNumber { get; set; }

        public long? CorrProductId { get; set; }

        public int? Cutting { get; set; }

        public int? CylId { get; set; }

        public short? Dc { get; set; }

        public int? DefineObjectId { get; set; }

        public string Description { get; set; }

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

        public bool NeedTools { get; set; }

        public string Optician { get; set; }

        public long? OrderId { get; set; }

        public long OrderItemId { get; set; }

        public short? Position { get; set; }

        public decimal? Price { get; set; }

        public short? Prism { get; set; }

        public int? ProvidedQuantity { get; set; }

        public int? Quantity { get; set; }

        public string RowNumber { get; set; }

        public System.DateTime? RowVersion { get; set; }

        public int? SphCylId { get; set; }

        public int? SphId { get; set; }

        #endregion

        #region Navigation Properties

        public TblLnsOrderDto TblLnsOrder { get; set; }

        public TblLnsSphDto TblLnsSph { get; set; }

        public TblLnsCylDto TblLnsCyl { get; set; }

        public TblLnsLensIndexRSphDto TblLnsLensIndexRSph { get; set; }

        public TblLnsSphCylDto TblLnsSphCyl { get; set; }

        public TblLnsBrandDto TblLnsBrand { get; set; }

        public TblLnsLensTypeRLensIndexRDto TblLnsLensTypeRLensIndexR { get; set; }

        public TblLnsBrandLensTypeRDto TblLnsBrandLensTypeR { get; set; }
		public TblClrDefineObjectDto  TblClrDefineObject { get; set; }

		#endregion
	}

}
