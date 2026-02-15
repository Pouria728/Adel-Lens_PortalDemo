
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{
    public partial class TblLnsCustomDesignTypeAdditionDto
    {
        #region Constructors

        public TblLnsCustomDesignTypeAdditionDto()
        {
        }

        public TblLnsCustomDesignTypeAdditionDto(int customDesignTypeAdditionId, int? designTypeId, decimal? additionValue, int orderId, bool isActive)
        {
            this.CustomDesignTypeAdditionId = customDesignTypeAdditionId;
            this.DesignTypeId = designTypeId;
            this.AdditionValue = additionValue;
            this.OrderId = orderId;
            this.IsActive = isActive;
        }

        #endregion

        #region Properties

        public int CustomDesignTypeAdditionId { get; set; }

        public int? DesignTypeId { get; set; }

        public int? DefineObjectId { get; set; }

        public decimal? AdditionValue { get; set; }

        public int OrderId { get; set; }

        public bool IsActive { get; set; }

        #endregion
    }
}
