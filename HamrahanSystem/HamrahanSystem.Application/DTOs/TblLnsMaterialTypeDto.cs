

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsMaterialTypeDto
    {
        #region Constructors

        public TblLnsMaterialTypeDto() {
        }

        public TblLnsMaterialTypeDto(string code, int? company, string createDate, int? createdBy, string description, bool isActive, int materialTypeId, int? modifiedBy, string modifiedDate, string name, int orderId, List<TblLnsLensIndexMaterialTypeDto> tblLnsLensIndexMaterialTypes, List<TblLnsOrderDto> tblLnsOrders) {

          this.Code = code;
          this.Company = company;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.IsActive = isActive;
          this.MaterialTypeId = materialTypeId;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.OrderId = orderId;
          this.TblLnsLensIndexMaterialTypes = tblLnsLensIndexMaterialTypes;
          this.TblLnsOrders = tblLnsOrders;
        }

        #endregion

        #region Properties

        public string Code { get; set; }

        public int? Company { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int MaterialTypeId { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblLnsLensIndexMaterialTypeDto> TblLnsLensIndexMaterialTypes { get; set; }

        public List<TblLnsOrderDto> TblLnsOrders { get; set; }

        #endregion
    }

}
