

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{ 


	public partial class TblWfwStatusDto
    {
        #region Constructors

        public TblWfwStatusDto() {
        }

        public TblWfwStatusDto(string code, int? company, string createDate, int? createdBy, string description, short? isActive, int? modifiedBy, string modifiedDate, string name, int statusId) {

          this.Code = code;
          this.Company = company;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.IsActive = isActive;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.StatusId = statusId;
        }

        #endregion

        #region Properties

        public string Code { get; set; }

        public int? Company { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public short? IsActive { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public int StatusId { get; set; }

        #endregion
    }

}
