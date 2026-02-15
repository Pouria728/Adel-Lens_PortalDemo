

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblInfoCustomer {

        public TblInfoCustomer()
        {
            this.BranchCode = @"";
            this.BranchName = @"";
            this.IsActive = 1;
            OnCreated();
        }

        public string? Address { get; set; }

        public string? BirthDate { get; set; }

        public string? BirthPlace { get; set; }

        public string? BranchCode { get; set; }

        public string? BranchName { get; set; }

        public string? Code { get; set; }

        public byte Company { get; set; }

        public string? Email { get; set; }

        public string? ExportDate { get; set; }

        public string? ExportPlace { get; set; }

        public string? FatherName { get; set; }

        public string? Fax { get; set; }

        public string? ID { get; set; }

        public int InfoCustomerId { get; set; }

        public short? IsActive { get; set; }

        public string? MailBox { get; set; }

        public string? Mobile { get; set; }

        public bool Original { get; set; }

        public string? PostalCode { get; set; }

        public int RecNo { get; set; }

        public int RefCity { get; set; }

        public int RefCustomer { get; set; }

        public int? RefPath { get; set; }

        public int RefProvince { get; set; }

        public int? RNContry { get; set; }

        public string? Tableau { get; set; }

        public string? Tel { get; set; }

        public bool? Transport { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        public override bool Equals(object obj)
        {
          TblInfoCustomer toCompare = obj as TblInfoCustomer;
          if (toCompare == null)
          {
            return false;
          }

          if (!Object.Equals(this.Company, toCompare.Company))
            return false;
          if (!Object.Equals(this.RecNo, toCompare.RecNo))
            return false;

          return true;
        }

        public override int GetHashCode()
        {
          int hashCode = 13;
          hashCode = (hashCode * 7) + Company.GetHashCode();
          hashCode = (hashCode * 7) + RecNo.GetHashCode();
          return hashCode;
        }

        #endregion
    }

}
