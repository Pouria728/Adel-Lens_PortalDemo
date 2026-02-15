

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblWfwCartableDto
    {
        #region Constructors

        public TblWfwCartableDto() {
        }

        public TblWfwCartableDto(int? advertNo, long cartableId, int? company, string createDate, int? daysNo, string documentDate, int? documentId, int? documentNo, int? documentRecNo, string fiscalYear, short? indexDocument, int? processStepId, short? status, string updateDate, int? userId) {

          this.AdvertNo = advertNo;
          this.CartableId = cartableId;
          this.Company = company;
          this.CreateDate = createDate;
          this.DaysNo = daysNo;
          this.DocumentDate = documentDate;
          this.DocumentId = documentId;
          this.DocumentNo = documentNo;
          this.DocumentRecNo = documentRecNo;
          this.FiscalYear = fiscalYear;
          this.IndexDocument = indexDocument;
          this.ProcessStepId = processStepId;
          this.Status = status;
          this.UpdateDate = updateDate;
          this.UserId = userId;
        }

        #endregion

        #region Properties

        public int? AdvertNo { get; set; }

        public long CartableId { get; set; }

        public int? Company { get; set; }

        public string CreateDate { get; set; }

        public int? DaysNo { get; set; }

        public string DocumentDate { get; set; }

        public int? DocumentId { get; set; }

        public int? DocumentNo { get; set; }

        public int? DocumentRecNo { get; set; }

        public string FiscalYear { get; set; }

        public short? IndexDocument { get; set; }

        public int? ProcessStepId { get; set; }

        public short? Status { get; set; }

        public string UpdateDate { get; set; }

        public int? UserId { get; set; }

        #endregion
    }

}
