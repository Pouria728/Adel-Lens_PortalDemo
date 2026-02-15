
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsLensTypeRDto
    {
        #region Constructors

        public TblLnsLensTypeRDto() {
        }

        public TblLnsLensTypeRDto(string code, int? company, string createDate, int? createdBy, string description, bool isActive, int lensTypeRId, int? modifiedBy, string modifiedDate, string name, int orderId, List<TblLnsBrandLensTypeRDto> tblLnsBrandLensTypeRs) {

          this.Code = code;
          this.Company = company;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.IsActive = isActive;
          this.LensTypeRId = lensTypeRId;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.OrderId = orderId;
          this.TblLnsBrandLensTypeRs = tblLnsBrandLensTypeRs;
        }

        #endregion

        #region Properties

        public string Code { get; set; }

        public int? Company { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int LensTypeRId { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblLnsBrandLensTypeRDto> TblLnsBrandLensTypeRs { get; set; }

        #endregion
    }

}
