
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{
    public partial class TblLnsCustomLensTypeMaterialDto
    {
        #region Constructors

        public TblLnsCustomLensTypeMaterialDto()
        {
        }

        public TblLnsCustomLensTypeMaterialDto(int customLensTypeMaterialId, int? designTypeId, int? lensIndexId, string lensTypeName, string materialName, int? defineObjectId, int orderId, bool isActive)
        {
            this.CustomLensTypeMaterialId = customLensTypeMaterialId;
            this.DesignTypeId = designTypeId;
            this.LensIndexId = lensIndexId;
            this.LensTypeName = lensTypeName;
            this.MaterialName = materialName;
            this.DefineObjectId = defineObjectId;
            this.OrderId = orderId;
            this.IsActive = isActive;
        }

        #endregion

        #region Properties

        public int CustomLensTypeMaterialId { get; set; }

        public int? DesignTypeId { get; set; }

        public int? LensIndexId { get; set; }

        public string LensTypeName { get; set; }

        public string MaterialName { get; set; }

        public int? DefineObjectId { get; set; }

        public int OrderId { get; set; }

        public bool IsActive { get; set; }

        #endregion
    }
}
