

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsFrameTypeDto
    {
        #region Constructors

        public TblLnsFrameTypeDto() {
        }

        public TblLnsFrameTypeDto(string code, int? company, string createDate, int? createdBy, string description, int frameTypeId, bool isActive, int? modifiedBy, string modifiedDate, string name, int orderId, List<TblLnsOrderDto> tblLnsOrders) {

          this.Code = code;
          this.Company = company;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.FrameTypeId = frameTypeId;
          this.IsActive = isActive;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.OrderId = orderId;
          this.TblLnsOrders = tblLnsOrders;
        }

        #endregion

        #region Properties

        public string Code { get; set; }

        public int? Company { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public int FrameTypeId { get; set; }

        public bool IsActive { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblLnsOrderDto> TblLnsOrders { get; set; }

        #endregion
    }

}
