

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsLensIndexMaterialTypeDto
    {
        #region Constructors

        public TblLnsLensIndexMaterialTypeDto() {
        }

        public TblLnsLensIndexMaterialTypeDto(int? defineObjectId, int? designTypeLensIndexId, int lensIndexMaterialTypeId, int? materialTypeId, TblLnsDesignTypeLensIndexDto tblLnsDesignTypeLensIndex, TblLnsMaterialTypeDto tblLnsMaterialType, List<TblLnsOrderDto> tblLnsOrders) {

          this.DefineObjectId = defineObjectId;
          this.DesignTypeLensIndexId = designTypeLensIndexId;
          this.LensIndexMaterialTypeId = lensIndexMaterialTypeId;
          this.MaterialTypeId = materialTypeId;
          this.TblLnsDesignTypeLensIndex = tblLnsDesignTypeLensIndex;
          this.TblLnsMaterialType = tblLnsMaterialType;
          this.TblLnsOrders = tblLnsOrders;
        }

        #endregion

        #region Properties

        public int? DefineObjectId { get; set; }

        public int? DesignTypeLensIndexId { get; set; }

        public int LensIndexMaterialTypeId { get; set; }

        public int? MaterialTypeId { get; set; }

        #endregion

        #region Navigation Properties

        public TblLnsDesignTypeLensIndexDto TblLnsDesignTypeLensIndex { get; set; }

        public TblLnsMaterialTypeDto TblLnsMaterialType { get; set; }

        public List<TblLnsOrderDto> TblLnsOrders { get; set; }

        #endregion
    }

}
