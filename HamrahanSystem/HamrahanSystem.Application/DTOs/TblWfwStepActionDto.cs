

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblWfwStepActionDto
    {
        #region Constructors

        public TblWfwStepActionDto() {
        }

        public TblWfwStepActionDto(int? processActionId, int? processStepId, int stepActionId) {

          this.ProcessActionId = processActionId;
          this.ProcessStepId = processStepId;
          this.StepActionId = stepActionId;
        }

        #endregion

        #region Properties

        public int? ProcessActionId { get; set; }

        public int? ProcessStepId { get; set; }

        public int StepActionId { get; set; }

        #endregion
    }

}
