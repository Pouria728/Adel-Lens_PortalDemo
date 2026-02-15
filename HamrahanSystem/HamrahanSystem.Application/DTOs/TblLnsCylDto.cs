

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsCylDto
    {
        #region Constructors

        public TblLnsCylDto() {
        }

        public TblLnsCylDto(string code, int? company, string createDate, int? createdBy, int cylId, string description, bool isActive, int? modifiedBy, string modifiedDate, string name, int orderId, List<TblLnsOrderItemDto> tblLnsOrderItems, List<TblLnsSphCylDto> tblLnsSphCyls) {

          this.Code = code;
          this.Company = company;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.CylId = cylId;
          this.Description = description;
          this.IsActive = isActive;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.OrderId = orderId;
          this.TblLnsOrderItems = tblLnsOrderItems;
          this.TblLnsSphCyls = tblLnsSphCyls;
        }

        #endregion

        #region Properties

        public string Code { get; set; }

        public int? Company { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public int CylId { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblLnsOrderItemDto> TblLnsOrderItems { get; set; }

        public List<TblLnsSphCylDto> TblLnsSphCyls { get; set; }

        #endregion
    }

}
