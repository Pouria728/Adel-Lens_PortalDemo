

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsLensIndexRSphDto
    {
        #region Constructors

        public TblLnsLensIndexRSphDto() {
        }

        public TblLnsLensIndexRSphDto(int lensIndexRSphId, int? lensTypeRLensIndexRId, int orderId, int? sphId, TblLnsSphDto tblLnsSph, TblLnsLensTypeRLensIndexRDto tblLnsLensTypeRLensIndexR, List<TblLnsOrderItemDto> tblLnsOrderItems, List<TblLnsSphCylDto> tblLnsSphCyls) {

          this.LensIndexRSphId = lensIndexRSphId;
          this.LensTypeRLensIndexRId = lensTypeRLensIndexRId;
          this.OrderId = orderId;
          this.SphId = sphId;
          this.TblLnsSph = tblLnsSph;
          this.TblLnsLensTypeRLensIndexR = tblLnsLensTypeRLensIndexR;
          this.TblLnsOrderItems = tblLnsOrderItems;
          this.TblLnsSphCyls = tblLnsSphCyls;
        }

        #endregion

        #region Properties

        public int LensIndexRSphId { get; set; }

        public int? LensTypeRLensIndexRId { get; set; }

        public int OrderId { get; set; }

        public int? SphId { get; set; }

        #endregion

        #region Navigation Properties

        public TblLnsSphDto TblLnsSph { get; set; }

        public TblLnsLensTypeRLensIndexRDto TblLnsLensTypeRLensIndexR { get; set; }

        public List<TblLnsOrderItemDto> TblLnsOrderItems { get; set; }

        public List<TblLnsSphCylDto> TblLnsSphCyls { get; set; }

        #endregion
    }

}
