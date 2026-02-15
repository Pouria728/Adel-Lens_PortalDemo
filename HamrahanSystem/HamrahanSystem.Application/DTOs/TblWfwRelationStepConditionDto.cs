

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblWfwRelationStepConditionDto
    {
        #region Constructors

        public TblWfwRelationStepConditionDto() {
        }

        public TblWfwRelationStepConditionDto(int? conditionId, string okResult, int relationStepConditionId, int? relationStepId) {

          this.ConditionId = conditionId;
          this.OkResult = okResult;
          this.RelationStepConditionId = relationStepConditionId;
          this.RelationStepId = relationStepId;
        }

        #endregion

        #region Properties

        public int? ConditionId { get; set; }

        public string OkResult { get; set; }

        public int RelationStepConditionId { get; set; }

        public int? RelationStepId { get; set; }

        #endregion
    }

}
