

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class RoleToRoleDto
    {
        #region Constructors

        public RoleToRoleDto() {
        }

        public RoleToRoleDto(int roleId, int rolesId, long roleToRolesId, RoleDto role_RoleId, RoleDto role_RolesId) {

          this.RoleId = roleId;
          this.RolesId = rolesId;
          this.RoleToRolesId = roleToRolesId;
          this.Role_RoleId = role_RoleId;
          this.Role_RolesId = role_RolesId;
        }

        #endregion

        #region Properties

        public int RoleId { get; set; }

        public int RolesId { get; set; }

        public long RoleToRolesId { get; set; }

        #endregion

        #region Navigation Properties

        public RoleDto Role_RoleId { get; set; }

        public RoleDto Role_RolesId { get; set; }

        #endregion
    }

}
