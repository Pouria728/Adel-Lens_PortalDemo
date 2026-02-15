

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class RequestFieldeDto
    {
        #region Constructors

        public RequestFieldeDto() {
        }

        public RequestFieldeDto(int baseRequestFieldId, string dataValue, Guid requestFieldId, Guid requestId, RequestDto request, BaseRequestFieldDto baseRequestFielded) {

          this.BaseRequestFieldId = baseRequestFieldId;
          this.DataValue = dataValue;
          this.RequestFieldId = requestFieldId;
          this.RequestId = requestId;
          this.Request = request;
          this.BaseRequestFielded = baseRequestFielded;
        }

        #endregion

        #region Properties

        public int BaseRequestFieldId { get; set; }

        public string DataValue { get; set; }

        public Guid RequestFieldId { get; set; }

        public Guid RequestId { get; set; }

        #endregion

        #region Navigation Properties

        public RequestDto Request { get; set; }

        public BaseRequestFieldDto BaseRequestFielded { get; set; }

        #endregion
    }

}
