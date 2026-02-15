

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsOrderserviceDto
    {
        #region Constructors

        public TblLnsOrderserviceDto() {
        }

        public TblLnsOrderserviceDto(int? defineServiceId, long? orderId, long orderServicesId, TblLnsOrderDto tblLnsOrder) {

          this.DefineServiceId = defineServiceId;
          this.OrderId = orderId;
          this.OrderServicesId = orderServicesId;
          this.TblLnsOrder = tblLnsOrder;
        }

        #endregion

        #region Properties

        public int? DefineServiceId { get; set; }

        public long? OrderId { get; set; }

        public long OrderServicesId { get; set; }

        #endregion

        #region Navigation Properties

        public TblLnsOrderDto TblLnsOrder { get; set; }

        #endregion
    }

}
