

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblWfwAttachDto
    {
        #region Constructors

        public TblWfwAttachDto() {
        }

        public TblWfwAttachDto(long attachId, long? cartableId, string code, int? company, string createDate, int? createdBy, string description, int? documentId, string fileStock, short? indexDocument, short? isActive, int? modifiedBy, string modifiedDate, string name, string suffix) {

          this.AttachId = attachId;
          this.CartableId = cartableId;
          this.Code = code;
          this.Company = company;
          this.CreateDate = createDate;
          this.CreatedBy = createdBy;
          this.Description = description;
          this.DocumentId = documentId;
          this.FileStock = fileStock;
          this.IndexDocument = indexDocument;
          this.IsActive = isActive;
          this.ModifiedBy = modifiedBy;
          this.ModifiedDate = modifiedDate;
          this.Name = name;
          this.Suffix = suffix;
        }

        #endregion

        #region Properties

        public long AttachId { get; set; }

        public long? CartableId { get; set; }

        public string Code { get; set; }

        public int? Company { get; set; }

        public string CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public int? DocumentId { get; set; }

        public string FileStock { get; set; }

        public short? IndexDocument { get; set; }

        public short? IsActive { get; set; }

        public int? ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string Name { get; set; }

        public string Suffix { get; set; }

        #endregion
    }

}
