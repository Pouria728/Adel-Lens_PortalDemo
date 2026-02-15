

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class User {

        public User()
        {
            this.InsertDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.LastDirectoryUpdate = DateTime.Now;
            this.AllowForceSend = false;
            this.CompanyId = 3;
            this.IsActive = 1;
            this.MobilePhoneVerified = false;
            this.TenantId = 1;
            this.UserPermissions = new List<UserPermission>();
            this.UserRoles = new List<UserRole>();
			this.TblWfwOrderProcessSteps = new List<TblWfwOrderProcessStep>();
			OnCreated();
        }

        public bool AllowForceSend { get; set; }

        public int CompanyId { get; set; }

        public string? DisplayName { get; set; }

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? FiscalYear { get; set; }

        public int? InfoCustomerId { get; set; }

        public DateTime InsertDate { get; set; }

        public int InsertUserId { get; set; }

        public short IsActive { get; set; }

        public DateTime? LastDirectoryUpdate { get; set; }

        public string? LastName { get; set; }

        public string? Mobile { get; set; }

        public string? MobilePhoneNumber { get; set; }

        public bool MobilePhoneVerified { get; set; }

        public string? NationalCode { get; set; }

        public string? PasswordHash { get; set; }

        public string? PasswordSalt { get; set; }

        public string? Source { get; set; }

        public int TenantId { get; set; }

        public int? TwoFactorAuth { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public int UserId { get; set; }

        public string? UserImage { get; set; }

        public string? Username { get; set; }

        public int? WinId { get; set; }
        public bool IsAdmin {  get; set; }

        public virtual IList<UserPermission> UserPermissions { get; set; }

        public virtual IList<UserRole> UserRoles { get; set; }
		public virtual IList<TblWfwOrderProcessStep> TblWfwOrderProcessSteps { get; set; }

		#region Extensibility Method Definitions

		partial void OnCreated();

        #endregion
    }

}
