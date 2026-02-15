

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsCoatingDto
    {
        #region Constructors

        public TblLnsCoatingDto() {
        }

        public TblLnsCoatingDto(int coatingId, string code, int? company, string createDate, int? createdBy, string description, bool isActive, int? modifiedBy, string modifiedDate, string name, int orderId, List<TblLnsBrandCoatingDto> tblLnsBrandCoatings, List<TblLnsOrderDto> tblLnsOrders) {

          this.CoatingId = coatingId;
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
          this.TblLnsBrandCoatings = tblLnsBrandCoatings;
          this.TblLnsOrders = tblLnsOrders;
        }

        #endregion

        #region Properties

        public int CoatingId { get; set; }

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

        #endregion

        #region Navigation Properties

        public List<TblLnsBrandCoatingDto> TblLnsBrandCoatings { get; set; }

        public List<TblLnsOrderDto> TblLnsOrders { get; set; }

        #endregion
    }

}
