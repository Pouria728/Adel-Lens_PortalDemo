

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblInfoCustomerDto
    {
        #region Constructors

        public TblInfoCustomerDto() {
        }

        public TblInfoCustomerDto(string address, string birthDate, string birthPlace, string branchCode, string branchName, string code, byte company, string email, string exportDate, string exportPlace, string fatherName, string fax, string iD, int infoCustomerId, short? isActive, string mailBox, string mobile, bool original, string postalCode, int recNo, int refCity, int refCustomer, int? refPath, int refProvince, int? rNContry, string tableau, string tel, bool? transport) {

          this.Address = address;
          this.BirthDate = birthDate;
          this.BirthPlace = birthPlace;
          this.BranchCode = branchCode;
          this.BranchName = branchName;
          this.Code = code;
          this.Company = company;
          this.Email = email;
          this.ExportDate = exportDate;
          this.ExportPlace = exportPlace;
          this.FatherName = fatherName;
          this.Fax = fax;
          this.ID = iD;
          this.InfoCustomerId = infoCustomerId;
          this.IsActive = isActive;
          this.MailBox = mailBox;
          this.Mobile = mobile;
          this.Original = original;
          this.PostalCode = postalCode;
          this.RecNo = recNo;
          this.RefCity = refCity;
          this.RefCustomer = refCustomer;
          this.RefPath = refPath;
          this.RefProvince = refProvince;
          this.RNContry = rNContry;
          this.Tableau = tableau;
          this.Tel = tel;
          this.Transport = transport;
        }

        #endregion

        #region Properties

        public string Address { get; set; }

        public string BirthDate { get; set; }

        public string BirthPlace { get; set; }

        public string BranchCode { get; set; }

        public string BranchName { get; set; }

        public string Code { get; set; }

        public byte Company { get; set; }

        public string Email { get; set; }

        public string ExportDate { get; set; }

        public string ExportPlace { get; set; }

        public string FatherName { get; set; }

        public string Fax { get; set; }

        public string ID { get; set; }

        public int InfoCustomerId { get; set; }

        public short? IsActive { get; set; }

        public string MailBox { get; set; }

        public string Mobile { get; set; }

        public bool Original { get; set; }

        public string PostalCode { get; set; }

        public int RecNo { get; set; }

        public int RefCity { get; set; }

        public int RefCustomer { get; set; }

        public int? RefPath { get; set; }

        public int RefProvince { get; set; }

        public int? RNContry { get; set; }

        public string Tableau { get; set; }

        public string Tel { get; set; }

        public bool? Transport { get; set; }

        #endregion
    }

}
