
namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsCustomLensIndexDto
    {
        #region Constructors

        public TblLnsCustomLensIndexDto()
        {
        }

        public TblLnsCustomLensIndexDto(int customLensIndexId, int? designTypeId, string lensIndexName, int orderId, bool isActive, bool hasColoringType)
        {
            this.CustomLensIndexId = customLensIndexId;
            this.DesignTypeId = designTypeId;
            this.LensIndexName = lensIndexName;
            this.OrderId = orderId;
            this.IsActive = isActive;
            this.HasColoringType = hasColoringType;
        }

        #endregion

        #region Properties

        public int CustomLensIndexId { get; set; }

        public int? DesignTypeId { get; set; }

        public string LensIndexName { get; set; }

        public int OrderId { get; set; }

        public bool IsActive { get; set; }

        public bool HasColoringType { get; set; }

        #endregion
    }

}
