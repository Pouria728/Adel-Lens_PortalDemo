

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsLensTypeDto
    {
        #region Constructors

        public TblLnsLensTypeDto() {
        }

        public TblLnsLensTypeDto(string code, int? company, int? brandId, string createDate, int? createdBy, string description, bool isActive, bool isCorridor, bool isSpecial, int lensTypeId, int? modifiedBy, string modifiedDate, string name, int orderId, List<TblLnsBrandLensTypeDto> tblLnsBrandLensTypes, List<TblLnsOrderDto> tblLnsOrders) {

          this.Code = code;
          this.Company = company;
          this.BrandId = brandId;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.IsActive = isActive;
          this.IsCorridor = isCorridor;
          this.IsSpecial = isSpecial;
          this.LensTypeId = lensTypeId;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.OrderId = orderId;
          this.TblLnsBrandLensTypes = tblLnsBrandLensTypes;
          this.TblLnsOrders = tblLnsOrders;
        }

        #endregion

        #region Properties

        public string Code { get; set; }

        public int? Company { get; set; }

        public int? BrandId { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsCorridor { get; set; }

        public bool IsSpecial { get; set; }

        public int LensTypeId { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblLnsBrandLensTypeDto> TblLnsBrandLensTypes { get; set; }

        public List<TblLnsOrderDto> TblLnsOrders { get; set; }

        #endregion
    }

}
