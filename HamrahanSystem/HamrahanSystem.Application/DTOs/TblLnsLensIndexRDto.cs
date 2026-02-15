

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsLensIndexRDto
    {
        #region Constructors

        public TblLnsLensIndexRDto() {
        }

        public TblLnsLensIndexRDto(string code, int? company, string createDate, int? createdBy, string description, bool isActive, int lensIndexRId, int? modifiedBy, string modifiedDate, string name, int orderId, List<TblLnsLensTypeRLensIndexRDto> tblLnsLensTypeRLensIndexRs) {

          this.Code = code;
          this.Company = company;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.IsActive = isActive;
          this.LensIndexRId = lensIndexRId;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.OrderId = orderId;
          this.TblLnsLensTypeRLensIndexRs = tblLnsLensTypeRLensIndexRs;
        }

        #endregion

        #region Properties

        public string Code { get; set; }

        public int? Company { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int LensIndexRId { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblLnsLensTypeRLensIndexRDto> TblLnsLensTypeRLensIndexRs { get; set; }

        #endregion
    }

}
