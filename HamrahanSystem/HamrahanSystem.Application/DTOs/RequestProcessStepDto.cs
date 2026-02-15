

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class RequestProcessStepDto
    {
        #region Constructors

        public RequestProcessStepDto() {
        }

        public RequestProcessStepDto(DateTime datecreate, Guid requestProcessId, Guid requestProcessStepId, int roleId, int statusId, int? userId, RequestProcessDto requestProcess) {

          this.Datecreate = datecreate;
          this.RequestProcessId = requestProcessId;
          this.RequestProcessStepId = requestProcessStepId;
          this.RoleId = roleId;
          this.StatusId = statusId;
          this.UserId = userId;
          this.RequestProcess = requestProcess;
        }

        #endregion

        #region Properties

        public DateTime Datecreate { get; set; }

        public Guid RequestProcessId { get; set; }

        public Guid RequestProcessStepId { get; set; }

        public int RoleId { get; set; }

        public int StatusId { get; set; }

        public int? UserId { get; set; }

        #endregion

        #region Navigation Properties

        public RequestProcessDto RequestProcess { get; set; }

        #endregion
    }

}
