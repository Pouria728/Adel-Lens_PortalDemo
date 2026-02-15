
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class RoleDto
    {
        #region Constructors

        public RoleDto() {
        }

        public RoleDto(int roleId, string roleKey, string roleName, int tenantId, List<RolePermissionDto> rolePermissions, List<RoleToRoleDto> roleToRoles_RoleId, List<RoleToRoleDto> roleToRoles_RolesId, List<UserRoleDto> userRoles,List<TblWfwRoleStepDto> tblWfwRoleSteps ) {

          this.RoleId = roleId;
          this.RoleKey = roleKey;
          this.RoleName = roleName;
          this.TenantId = tenantId;
          this.RolePermissions = rolePermissions;
          this.RoleToRoles_RoleId = roleToRoles_RoleId;
          this.RoleToRoles_RolesId = roleToRoles_RolesId;
          this.UserRoles = userRoles;
          this.TblWfwRoleStepes = tblWfwRoleSteps;
        }

        #endregion

        #region Properties

        public int RoleId { get; set; }

        public string RoleKey { get; set; }

        public string RoleName { get; set; }

        public int TenantId { get; set; }

        #endregion

        #region Navigation Properties

        public List<RolePermissionDto> RolePermissions { get; set; }

        public List<RoleToRoleDto> RoleToRoles_RoleId { get; set; }

        public List<RoleToRoleDto> RoleToRoles_RolesId { get; set; }

        public List<UserRoleDto> UserRoles { get; set; }
		public List<TblWfwRoleStepDto> TblWfwRoleStepes { get; set; }

		#endregion
	}

}
