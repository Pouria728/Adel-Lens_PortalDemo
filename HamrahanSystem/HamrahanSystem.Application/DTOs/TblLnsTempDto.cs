

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblLnsTempDto
    {
        #region Constructors

        public TblLnsTempDto() {
        }

        public TblLnsTempDto(int? c000, int? c025, int? c050, int? c075, int? c100, int? c125, int? c150, int? c175, int? c200, int? c225, int? c250, int? c275, int? c300, int? c325, int? c350, int? c375, int? c400, int? c425, int? c450, int? c475, int? c500, int? c525, int? c550, int? c575, int? c600, long tempId, string tempRow, int? userId) {

          this.C000 = c000;
          this.C025 = c025;
          this.C050 = c050;
          this.C075 = c075;
          this.C100 = c100;
          this.C125 = c125;
          this.C150 = c150;
          this.C175 = c175;
          this.C200 = c200;
          this.C225 = c225;
          this.C250 = c250;
          this.C275 = c275;
          this.C300 = c300;
          this.C325 = c325;
          this.C350 = c350;
          this.C375 = c375;
          this.C400 = c400;
          this.C425 = c425;
          this.C450 = c450;
          this.C475 = c475;
          this.C500 = c500;
          this.C525 = c525;
          this.C550 = c550;
          this.C575 = c575;
          this.C600 = c600;
          this.TempId = tempId;
          this.TempRow = tempRow;
          this.UserId = userId;
        }

        #endregion

        #region Properties

        public int? C000 { get; set; }

        public int? C025 { get; set; }

        public int? C050 { get; set; }

        public int? C075 { get; set; }

        public int? C100 { get; set; }

        public int? C125 { get; set; }

        public int? C150 { get; set; }

        public int? C175 { get; set; }

        public int? C200 { get; set; }

        public int? C225 { get; set; }

        public int? C250 { get; set; }

        public int? C275 { get; set; }

        public int? C300 { get; set; }

        public int? C325 { get; set; }

        public int? C350 { get; set; }

        public int? C375 { get; set; }

        public int? C400 { get; set; }

        public int? C425 { get; set; }

        public int? C450 { get; set; }

        public int? C475 { get; set; }

        public int? C500 { get; set; }

        public int? C525 { get; set; }

        public int? C550 { get; set; }

        public int? C575 { get; set; }

        public int? C600 { get; set; }

        public long TempId { get; set; }

        public string TempRow { get; set; }

        public int? UserId { get; set; }

        #endregion
    }

}
