

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsBrandLensTypeRDto
    {
        #region Constructors

        public TblLnsBrandLensTypeRDto() {
        }

        public TblLnsBrandLensTypeRDto(int? brandId, int brandLensTypeRId, int? lensTypeRId, int orderId, TblLnsBrandDto tblLnsBrand, TblLnsLensTypeRDto tblLnsLensTypeR, List<TblLnsLensTypeRLensIndexRDto> tblLnsLensTypeRLensIndexRs, List<TblLnsOrderDto> tblLnsOrders, List<TblLnsOrderItemDto> tblLnsOrderItems) {

          this.BrandId = brandId;
          this.BrandLensTypeRId = brandLensTypeRId;
          this.LensTypeRId = lensTypeRId;
          this.OrderId = orderId;
          this.TblLnsBrand = tblLnsBrand;
          this.TblLnsLensTypeR = tblLnsLensTypeR;
          this.TblLnsLensTypeRLensIndexRs = tblLnsLensTypeRLensIndexRs;
          this.TblLnsOrders = tblLnsOrders;
          this.TblLnsOrderItems = tblLnsOrderItems;
        }

        #endregion

        #region Properties

        public int? BrandId { get; set; }

        public int BrandLensTypeRId { get; set; }

        public int? LensTypeRId { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public TblLnsBrandDto TblLnsBrand { get; set; }

        public TblLnsLensTypeRDto TblLnsLensTypeR { get; set; }

        public List<TblLnsLensTypeRLensIndexRDto> TblLnsLensTypeRLensIndexRs { get; set; }

        public List<TblLnsOrderDto> TblLnsOrders { get; set; }

        public List<TblLnsOrderItemDto> TblLnsOrderItems { get; set; }

        #endregion
    }

}
