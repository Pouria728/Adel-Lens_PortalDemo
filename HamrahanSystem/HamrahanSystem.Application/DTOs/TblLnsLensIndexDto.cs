
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsLensIndexDto
    {
        #region Constructors

        public TblLnsLensIndexDto() {
        }

        public TblLnsLensIndexDto(string code, int? company, string createDate, int? createdBy, string description, bool isActive, int lensIndexId, int? modifiedBy, string modifiedDate, string name, int orderId, List<TblLnsDesignTypeLensIndexDto> tblLnsDesignTypeLensIndices, List<TblLnsOrderDto> tblLnsOrders) {

          this.Code = code;
          this.Company = company;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.IsActive = isActive;
          this.LensIndexId = lensIndexId;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.OrderId = orderId;
          this.TblLnsDesignTypeLensIndices = tblLnsDesignTypeLensIndices;
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

        public int LensIndexId { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblLnsDesignTypeLensIndexDto> TblLnsDesignTypeLensIndices { get; set; }

        public List<TblLnsOrderDto> TblLnsOrders { get; set; }

        #endregion
    }

}
