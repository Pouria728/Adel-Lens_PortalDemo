

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblWfwRelationStepDto
    {
        #region Constructors

        public TblWfwRelationStepDto() {
        }

        public TblWfwRelationStepDto(int? fromProcessStepId, int relationStepId, int? stepActionId, int? toProcessStepId) {

          this.FromProcessStepId = fromProcessStepId;
          this.RelationStepId = relationStepId;
          this.StepActionId = stepActionId;
          this.ToProcessStepId = toProcessStepId;
        }

        #endregion

        #region Properties

        public int? FromProcessStepId { get; set; }

        public int RelationStepId { get; set; }

        public int? StepActionId { get; set; }

        public int? ToProcessStepId { get; set; }

        #endregion
    }

}
