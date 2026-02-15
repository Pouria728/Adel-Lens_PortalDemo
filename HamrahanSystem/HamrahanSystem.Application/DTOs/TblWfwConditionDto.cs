

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblWfwConditionDto
    {
        #region Constructors

        public TblWfwConditionDto() {
        }

        public TblWfwConditionDto(string code, int? company, int conditionId, string createDate, int? createdBy, string description, short? isActive, int? modifiedBy, string modifiedDate, string name) {

          this.Code = code;
          this.Company = company;
          this.ConditionId = conditionId;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.IsActive = isActive;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
        }

        #endregion

        #region Properties

        public string Code { get; set; }

        public int? Company { get; set; }

        public int ConditionId { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public short? IsActive { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        #endregion
    }

}
