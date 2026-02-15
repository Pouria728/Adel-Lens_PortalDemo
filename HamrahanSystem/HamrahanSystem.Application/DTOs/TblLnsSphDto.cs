

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsSphDto
    {
        #region Constructors

        public TblLnsSphDto() {
        }

        public TblLnsSphDto(string code, int? company, string createDate, int? createdBy, string description, bool isActive, int? modifiedBy, string modifiedDate, string name, int orderId, int sphId, List<TblLnsLensIndexRSphDto> tblLnsLensIndexRSphs, List<TblLnsOrderItemDto> tblLnsOrderItems) {

          this.Code = code;
          this.Company = company;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.IsActive = isActive;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.OrderId = orderId;
          this.SphId = sphId;
          this.TblLnsLensIndexRSphs = tblLnsLensIndexRSphs;
          this.TblLnsOrderItems = tblLnsOrderItems;
        }

        #endregion

        #region Properties

        public string Code { get; set; }

        public int? Company { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        public int SphId { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblLnsLensIndexRSphDto> TblLnsLensIndexRSphs { get; set; }

        public List<TblLnsOrderItemDto> TblLnsOrderItems { get; set; }

        #endregion
    }

}
