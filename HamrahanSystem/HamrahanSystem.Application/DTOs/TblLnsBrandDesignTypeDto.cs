

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsBrandDesignTypeDto
    {
        #region Constructors

        public TblLnsBrandDesignTypeDto() {
        }

        public TblLnsBrandDesignTypeDto(int brandDesignTypeId, int? brandLensTypeId, int? designTypeId, int orderId, TblLnsDesignTypeDto tblLnsDesignType, TblLnsBrandLensTypeDto tblLnsBrandLensType, List<TblLnsDesignTypeLensIndexDto> tblLnsDesignTypeLensIndices, List<TblLnsOrderDto> tblLnsOrders) {

          this.BrandDesignTypeId = brandDesignTypeId;
          this.BrandLensTypeId = brandLensTypeId;
          this.DesignTypeId = designTypeId;
          this.OrderId = orderId;
          this.TblLnsDesignType = tblLnsDesignType;
          this.TblLnsBrandLensType = tblLnsBrandLensType;
          this.TblLnsDesignTypeLensIndices = tblLnsDesignTypeLensIndices;
          this.TblLnsOrders = tblLnsOrders;
        }

        #endregion

        #region Properties

        public int BrandDesignTypeId { get; set; }

        public int? BrandLensTypeId { get; set; }

        public int? DesignTypeId { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public TblLnsDesignTypeDto TblLnsDesignType { get; set; }

        public TblLnsBrandLensTypeDto TblLnsBrandLensType { get; set; }

        public List<TblLnsDesignTypeLensIndexDto> TblLnsDesignTypeLensIndices { get; set; }

        public List<TblLnsOrderDto> TblLnsOrders { get; set; }

        #endregion
    }

}
