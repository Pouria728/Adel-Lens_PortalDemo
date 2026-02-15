

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblWfwUserCartableDto
    {
        #region Constructors

        public TblWfwUserCartableDto() {
        }

        public TblWfwUserCartableDto(short? active, long? cartableId, string paraph, int? statusId, string updateDate, long userCartableId, int? userId) {

          this.Active = active;
          this.CartableId = cartableId;
          this.Paraph = paraph;
          this.StatusId = statusId;
          this.UpdateDate = updateDate;
          this.UserCartableId = userCartableId;
          this.UserId = userId;
        }

        #endregion

        #region Properties

        public short? Active { get; set; }

        public long? CartableId { get; set; }

        public string Paraph { get; set; }

        public int? StatusId { get; set; }

        public string UpdateDate { get; set; }

        public long UserCartableId { get; set; }

        public int? UserId { get; set; }

        #endregion
    }

}
