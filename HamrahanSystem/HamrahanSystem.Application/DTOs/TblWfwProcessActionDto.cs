

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblWfwProcessActionDto
    {
        #region Constructors

        public TblWfwProcessActionDto() {
        }

        public TblWfwProcessActionDto(string code, string color, int? company, string createDate, int? createdBy, string description, string icon, short? isActive, int? modifiedBy, string modifiedDate, string name, int processActionId) {

          this.Code = code;
          this.Color = color;
          this.Company = company;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.Icon = icon;
          this.IsActive = isActive;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.ProcessActionId = processActionId;
        }

        #endregion

        #region Properties

        public string Code { get; set; }

        public string Color { get; set; }

        public int? Company { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public short? IsActive { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public int ProcessActionId { get; set; }

        #endregion
    }

}
