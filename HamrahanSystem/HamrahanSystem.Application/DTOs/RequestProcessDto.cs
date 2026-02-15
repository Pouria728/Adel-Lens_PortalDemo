

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class RequestProcessDto
    {
        #region Constructors

        public RequestProcessDto() {
        }

        public RequestProcessDto(DateTime createDate, DateTime? dateComplete, Guid requestId, Guid requestProcessId, int statusId, RequestDto request, List<RequestProcessStepDto> requestProcessSteps) {

          this.CreateDate = createDate;
          this.DateComplete = dateComplete;
          this.RequestId = requestId;
          this.RequestProcessId = requestProcessId;
          this.StatusId = statusId;
          this.Request = request;
          this.RequestProcessSteps = requestProcessSteps;
        }

        #endregion

        #region Properties

        public DateTime CreateDate { get; set; }

        public DateTime? DateComplete { get; set; }

        public Guid RequestId { get; set; }

        public Guid RequestProcessId { get; set; }

        public int StatusId { get; set; }

        #endregion

        #region Navigation Properties

        public RequestDto Request { get; set; }

        public List<RequestProcessStepDto> RequestProcessSteps { get; set; }

        #endregion
    }

}
