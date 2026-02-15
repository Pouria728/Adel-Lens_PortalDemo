

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsBrandLensTypeDto
    {
        #region Constructors

        public TblLnsBrandLensTypeDto() {
        }

        public TblLnsBrandLensTypeDto(int? brandId, int brandLensTypeId, int? lensTypeId, int orderId, List<TblLnsBrandDesignTypeDto> tblLnsBrandDesignTypes, TblLnsLensTypeDto tblLnsLensType, TblLnsBrandDto tblLnsBrand, List<TblLnsOrderDto> tblLnsOrders) {

          this.BrandId = brandId;
          this.BrandLensTypeId = brandLensTypeId;
          this.LensTypeId = lensTypeId;
          this.OrderId = orderId;
          this.TblLnsBrandDesignTypes = tblLnsBrandDesignTypes;
          this.TblLnsLensType = tblLnsLensType;
          this.TblLnsBrand = tblLnsBrand;
          this.TblLnsOrders = tblLnsOrders;
        }

        #endregion

        #region Properties

        public int? BrandId { get; set; }

        public int BrandLensTypeId { get; set; }

        public int? LensTypeId { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblLnsBrandDesignTypeDto> TblLnsBrandDesignTypes { get; set; }

        public TblLnsLensTypeDto TblLnsLensType { get; set; }

        public TblLnsBrandDto TblLnsBrand { get; set; }

        public List<TblLnsOrderDto> TblLnsOrders { get; set; }

        #endregion
    }

}
