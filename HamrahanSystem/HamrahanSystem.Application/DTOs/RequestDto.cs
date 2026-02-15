

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class RequestDto
    {
        #region Constructors

        public RequestDto() {
        }

        public RequestDto(int baseRequestId, DateTime dateCreate, Guid requestId, int statusId, int userId, List<RequestFieldeDto> requestFieldes, List<RequestProcessDto> requestProcesses, BaseRequesteDto baseRequeste) {

          this.BaseRequestId = baseRequestId;
          this.DateCreate = dateCreate;
          this.RequestId = requestId;
          this.StatusId = statusId;
          this.UserId = userId;
          this.RequestFieldes = requestFieldes;
          this.RequestProcesses = requestProcesses;
          this.BaseRequeste = baseRequeste;
        }

        #endregion

        #region Properties

        public int BaseRequestId { get; set; }

        public DateTime DateCreate { get; set; }

        public Guid RequestId { get; set; }

        public int StatusId { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Navigation Properties

        public List<RequestFieldeDto> RequestFieldes { get; set; }

        public List<RequestProcessDto> RequestProcesses { get; set; }

        public BaseRequesteDto BaseRequeste { get; set; }

        #endregion
    }

}
