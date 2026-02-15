

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class RolePermissionDto
    {
        #region Constructors

        public RolePermissionDto() {
        }

        public RolePermissionDto(string permissionKey, int roleId,int permissionId, long rolePermissionId, RoleDto role ,PermissionDto permission) {

          this.PermissionKey = permissionKey;
          this.RoleId = roleId;
          this.RolePermissionId = rolePermissionId;
          this.Permission = permission;
          this.PermissionId = permissionId;
        }

        #endregion

        #region Properties

        public string PermissionKey { get; set; }

        public int RoleId { get; set; }
		public int PermissionId { get; set; }

		public long RolePermissionId { get; set; }

        #endregion

        #region Navigation Properties

        public RoleDto Role { get; set; }
		public PermissionDto Permission { get; set; }

		#endregion
	}

}
