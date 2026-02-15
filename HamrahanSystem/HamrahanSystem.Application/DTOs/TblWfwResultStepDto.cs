

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblWfwResultStepDto
    {
        #region Constructors

        public TblWfwResultStepDto() {
        }

        public TblWfwResultStepDto(string code, string command, short commandType, int? company, string createDate, int? createdBy, string description, short? isActive, int? modifiedBy, string modifiedDate, string name, int? relationStepId, int resultStepId) {

          this.Code = code;
          this.Command = command;
          this.CommandType = commandType;
          this.Company = company;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.IsActive = isActive;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.RelationStepId = relationStepId;
          this.ResultStepId = resultStepId;
        }

        #endregion

        #region Properties

        public string Code { get; set; }

        public string Command { get; set; }

        public short CommandType { get; set; }

        public int? Company { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public short? IsActive { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public int? RelationStepId { get; set; }

        public int ResultStepId { get; set; }

        #endregion
    }

}
