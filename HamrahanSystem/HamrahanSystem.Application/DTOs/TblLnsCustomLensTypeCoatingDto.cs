
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsCustomLensTypeCoatingDto
    {
        #region Constructors

        public TblLnsCustomLensTypeCoatingDto()
        {
        }

        public TblLnsCustomLensTypeCoatingDto(int customLensTypeCoatingId, int? designTypeId, string lensTypeName, string coatingName, bool isDefault, int orderId, bool isActive)
        {
            this.CustomLensTypeCoatingId = customLensTypeCoatingId;
            this.DesignTypeId = designTypeId;
            this.LensTypeName = lensTypeName;
            this.CoatingName = coatingName;
            this.IsDefault = isDefault;
            this.OrderId = orderId;
            this.IsActive = isActive;
        }

        #endregion

        #region Properties

        public int CustomLensTypeCoatingId { get; set; }

        public int? DesignTypeId { get; set; }

        public string LensTypeName { get; set; }

        public string CoatingName { get; set; }

        public bool IsDefault { get; set; }

        public int OrderId { get; set; }

        public bool IsActive { get; set; }

        #endregion
    }

}

