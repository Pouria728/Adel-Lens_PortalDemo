
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class PermissionDto
	{
        #region Constructors

        public PermissionDto() {
        }

        public PermissionDto(int permissionId, string permissionKey, string title,  List<UserPermissionDto> userPermissions, List<RolePermissionDto> rolePermissions) {

          this.PermissionId = permissionId;
          this.PermissionKey = permissionKey;
          this.Title = title;
          this.UserPermissions = userPermissions;
          this.RolePermissions = rolePermissions;

        }

		#endregion

		#region Properties

		public int PermissionId { get; set; }

		public string PermissionKey { get; set; }

		public string Title { get; set; }

		#endregion

		#region Navigation Properties

		public  List<RolePermissionDto> RolePermissions { get; set; }

		public List<UserPermissionDto> UserPermissions { get; set; }

        #endregion
    }

}
