

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsDesignTypeDto
    {
        #region Constructors

        public TblLnsDesignTypeDto() {
        }

        public TblLnsDesignTypeDto(string code, int? company, int? lensTypeId, string createDate, int? createdBy, string description, int designTypeId, bool isActive, bool isSpecial, int? modifiedBy, string modifiedDate, string name, int orderId, List<TblLnsBrandDesignTypeDto> tblLnsBrandDesignTypes, List<TblLnsOrderDto> tblLnsOrders) {

          this.Code = code;
          this.Company = company;
          this.LensTypeId = lensTypeId;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.DesignTypeId = designTypeId;
          this.IsActive = isActive;
          this.IsSpecial = isSpecial;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.OrderId = orderId;
          this.TblLnsBrandDesignTypes = tblLnsBrandDesignTypes;
          this.TblLnsOrders = tblLnsOrders;
        }

        #endregion

        #region Properties

        public string Code { get; set; }

        public int? Company { get; set; }

        public int? LensTypeId { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public int DesignTypeId { get; set; }

        public bool IsActive { get; set; }

        public bool IsSpecial { get; set; }

        public bool SphPlus { get; set; }

        public bool SphMinus { get; set; }

        public bool Addition { get; set; }

        public decimal? AdditionValue { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblLnsBrandDesignTypeDto> TblLnsBrandDesignTypes { get; set; }

        public List<TblLnsOrderDto> TblLnsOrders { get; set; }

        #endregion
    }

}
