

using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class UserDto
    {
        #region Constructors

        public UserDto() {
        }

        public UserDto(bool allowForceSend, int companyId, string displayName, string email, string firstName, string fiscalYear, int? infoCustomerId, System.DateTime insertDate, int insertUserId, short isActive, System.DateTime? lastDirectoryUpdate, string lastName, string mobile, string mobilePhoneNumber, bool mobilePhoneVerified, string nationalCode, string passwordHash, string passwordSalt, string source, int tenantId, int? twoFactorAuth, System.DateTime? updateDate, int? updateUserId, int userId, string userImage, string username, int? winId,bool isAdmin, List<UserPermissionDto> userPermissions, List<UserRoleDto> userRoles, List<TblWfwOrderProcessStepDto> tblWfwOrderProcessSteps) {

          this.AllowForceSend = allowForceSend;
          this.CompanyId = companyId;
          this.DisplayName = displayName;
          this.Email = email;
          this.FirstName = firstName;
          this.FiscalYear = fiscalYear;
          this.InfoCustomerId = infoCustomerId;
          this.InsertDate = insertDate;
          this.InsertUserId = insertUserId;
          this.IsActive = Convert.ToBoolean(isActive);
          this.LastDirectoryUpdate = lastDirectoryUpdate;
          this.LastName = lastName;
          this.Mobile = mobile;
          this.MobilePhoneNumber = mobilePhoneNumber;
          this.MobilePhoneVerified = mobilePhoneVerified;
          this.NationalCode = nationalCode;
          this.PasswordHash = passwordHash;
          this.PasswordSalt = passwordSalt;
          this.Source = source;
          this.TenantId = tenantId;
          this.TwoFactorAuth = twoFactorAuth;
          this.UpdateDate = updateDate;
          this.UpdateUserId = updateUserId;
          this.UserId = userId;
          this.UserImage = userImage;
          this.Username = username;
          this.WinId = winId;
          this.UserPermissions = userPermissions;
          this.UserRoles = userRoles;
            this.IsAdmin=isAdmin;
			this.TblWfwOrderProcessSteps = tblWfwOrderProcessSteps;
		}

        #endregion

        #region Properties

        public bool AllowForceSend { get; set; }

        public int CompanyId { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string FiscalYear { get; set; }

        public int? InfoCustomerId { get; set; }

        public System.DateTime InsertDate { get; set; }

        public int InsertUserId { get; set; }

        public bool IsActive { get; set; }

        public System.DateTime? LastDirectoryUpdate { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; set; }

        public string MobilePhoneNumber { get; set; }

        public bool MobilePhoneVerified { get; set; }

        public string NationalCode { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public string Source { get; set; }

        public int TenantId { get; set; }

        public int? TwoFactorAuth { get; set; }

        public System.DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public int UserId { get; set; }

        public string UserImage { get; set; }

        public string Username { get; set; }

        public int? WinId { get; set; }
		public bool IsAdmin { get; set; }

		#endregion

		#region Navigation Properties

		public List<UserPermissionDto> UserPermissions { get; set; }

        public List<UserRoleDto> UserRoles { get; set; }
		public List<TblWfwOrderProcessStepDto> TblWfwOrderProcessSteps { get; set; }

		#endregion
	}

}
