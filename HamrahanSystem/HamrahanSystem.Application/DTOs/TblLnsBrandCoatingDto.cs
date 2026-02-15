

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsBrandCoatingDto
    {
        #region Constructors

        public TblLnsBrandCoatingDto() {
        }

        public TblLnsBrandCoatingDto(int brandCoatingId, int? brandId, int? coatingId, bool? isDefault, int orderId, TblLnsBrandDto tblLnsBrand, TblLnsCoatingDto tblLnsCoating) {

          this.BrandCoatingId = brandCoatingId;
          this.BrandId = brandId;
          this.CoatingId = coatingId;
          this.IsDefault = isDefault;
          this.OrderId = orderId;
          this.TblLnsBrand = tblLnsBrand;
          this.TblLnsCoating = tblLnsCoating;
        }

        #endregion

        #region Properties

        public int BrandCoatingId { get; set; }

        public int? BrandId { get; set; }

        public int? CoatingId { get; set; }

        public bool? IsDefault { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public TblLnsBrandDto TblLnsBrand { get; set; }

        public TblLnsCoatingDto TblLnsCoating { get; set; }

        #endregion
    }

}
