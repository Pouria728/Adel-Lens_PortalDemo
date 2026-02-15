

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class BaseRequestStepDto
    {
        #region Constructors

        public BaseRequestStepDto() {
        }

        public BaseRequestStepDto(int baseRequestId, int baseRequestStepId, int orderId, int roleId, string title, BaseRequesteDto baseRequeste) {

          this.BaseRequestId = baseRequestId;
          this.BaseRequestStepId = baseRequestStepId;
          this.OrderId = orderId;
          this.RoleId = roleId;
          this.Title = title;
          this.BaseRequeste = baseRequeste;
        }

        #endregion

        #region Properties

        public int BaseRequestId { get; set; }

        public int BaseRequestStepId { get; set; }

        public int OrderId { get; set; }

        public int RoleId { get; set; }

        public string Title { get; set; }

        #endregion

        #region Navigation Properties

        public BaseRequesteDto BaseRequeste { get; set; }

        #endregion
    }

}
