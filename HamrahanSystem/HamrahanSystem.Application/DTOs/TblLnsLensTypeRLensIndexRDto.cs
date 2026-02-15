

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsLensTypeRLensIndexRDto
    {
        #region Constructors

        public TblLnsLensTypeRLensIndexRDto() {
        }

        public TblLnsLensTypeRLensIndexRDto(int? brandLensTypeRId, int? lensIndexRId, int lensTypeRLensIndexRId, int orderId, List<TblLnsLensIndexRSphDto> tblLnsLensIndexRSphs, TblLnsLensIndexRDto tblLnsLensIndexR, TblLnsBrandLensTypeRDto tblLnsBrandLensTypeR, List<TblLnsOrderDto> tblLnsOrders, List<TblLnsOrderItemDto> tblLnsOrderItems) {

          this.BrandLensTypeRId = brandLensTypeRId;
          this.LensIndexRId = lensIndexRId;
          this.LensTypeRLensIndexRId = lensTypeRLensIndexRId;
          this.OrderId = orderId;
          this.TblLnsLensIndexRSphs = tblLnsLensIndexRSphs;
          this.TblLnsLensIndexR = tblLnsLensIndexR;
          this.TblLnsBrandLensTypeR = tblLnsBrandLensTypeR;
          this.TblLnsOrders = tblLnsOrders;
          this.TblLnsOrderItems = tblLnsOrderItems;
        }

        #endregion

        #region Properties

        public int? BrandLensTypeRId { get; set; }

        public int? LensIndexRId { get; set; }

        public int LensTypeRLensIndexRId { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblLnsLensIndexRSphDto> TblLnsLensIndexRSphs { get; set; }

        public TblLnsLensIndexRDto TblLnsLensIndexR { get; set; }

        public TblLnsBrandLensTypeRDto TblLnsBrandLensTypeR { get; set; }

        public List<TblLnsOrderDto> TblLnsOrders { get; set; }

        public List<TblLnsOrderItemDto> TblLnsOrderItems { get; set; }

        #endregion
    }

}
