

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsDesignTypeLensIndexDto
    {
        #region Constructors

        public TblLnsDesignTypeLensIndexDto() {
        }

        public TblLnsDesignTypeLensIndexDto(int? brandDesignTypeId, int designTypeLensIndexId, int? lensIndexId, int orderId, TblLnsLensIndexDto tblLnsLensIndex, TblLnsBrandDesignTypeDto tblLnsBrandDesignType, List<TblLnsLensIndexMaterialTypeDto> tblLnsLensIndexMaterialTypes, List<TblLnsOrderDto> tblLnsOrders) {

          this.BrandDesignTypeId = brandDesignTypeId;
          this.DesignTypeLensIndexId = designTypeLensIndexId;
          this.LensIndexId = lensIndexId;
          this.OrderId = orderId;
          this.TblLnsLensIndex = tblLnsLensIndex;
          this.TblLnsBrandDesignType = tblLnsBrandDesignType;
          this.TblLnsLensIndexMaterialTypes = tblLnsLensIndexMaterialTypes;
          this.TblLnsOrders = tblLnsOrders;
        }

        #endregion

        #region Properties

        public int? BrandDesignTypeId { get; set; }

        public int DesignTypeLensIndexId { get; set; }

        public int? LensIndexId { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public TblLnsLensIndexDto TblLnsLensIndex { get; set; }

        public TblLnsBrandDesignTypeDto TblLnsBrandDesignType { get; set; }

        public List<TblLnsLensIndexMaterialTypeDto> TblLnsLensIndexMaterialTypes { get; set; }

        public List<TblLnsOrderDto> TblLnsOrders { get; set; }

        #endregion
    }

}
