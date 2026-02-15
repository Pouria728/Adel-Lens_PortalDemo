

using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsBrandDto
    {
        #region Constructors

        public TblLnsBrandDto() {
            this.CreateDate = "";
            Code = "";
            Company = 3;
            Name = "";
			

		}


		public TblLnsBrandDto(int brandId, string code, int? company, string createDate, int? createdBy, string description, bool isActive, bool isSpecial, bool isStock, bool isStockGranty, int? modifiedBy, string modifiedDate, string name, int orderId, List<TblLnsBrandCoatingDto> tblLnsBrandCoatings, List<TblLnsBrandLensTypeDto> tblLnsBrandLensTypes, List<TblLnsBrandLensTypeRDto> tblLnsBrandLensTypeRs, List<TblLnsOrderDto> tblLnsOrders, List<TblLnsOrderItemDto> tblLnsOrderItems) {

          this.BrandId = brandId;
          this.Code = code;
          this.Company = company;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.IsActive = isActive;
            this.IsSpecial = isSpecial;
            this.IsStock = isStock;
            this.IsStockGranty = isStockGranty;
            this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.OrderId = orderId;
          this.TblLnsBrandCoatings = tblLnsBrandCoatings;
          this.TblLnsBrandLensTypes = tblLnsBrandLensTypes;
          this.TblLnsBrandLensTypeRs = tblLnsBrandLensTypeRs;
          this.TblLnsOrders = tblLnsOrders;
          this.TblLnsOrderItems = tblLnsOrderItems;
        }

        #endregion

        #region Properties

        public int BrandId { get; set; }

        public string Code { get; set; }

        public int? Company { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
        public bool IsSpecial { get; set; }

        public bool IsStock { get; set; }

        public bool IsStockGranty { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblLnsBrandCoatingDto> TblLnsBrandCoatings { get; set; }

        public List<TblLnsBrandLensTypeDto> TblLnsBrandLensTypes { get; set; }

        public List<TblLnsBrandLensTypeRDto> TblLnsBrandLensTypeRs { get; set; }

        public List<TblLnsOrderDto> TblLnsOrders { get; set; }

        public List<TblLnsOrderItemDto> TblLnsOrderItems { get; set; }

        #endregion
    }

}
