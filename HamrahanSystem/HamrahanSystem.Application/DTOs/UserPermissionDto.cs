
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

	public partial class UserPermissionDto
	{
		#region Constructors

		public UserPermissionDto()
		{
		}

		public UserPermissionDto(bool granted, string permissionKey, int userId, long userPermissionId, UserDto user, PermissionDto permission)
		{

			this.Granted = granted;
			this.PermissionKey = permissionKey;
			this.UserId = userId;
			this.UserPermissionId = userPermissionId;
			this.User = user;
			this.Permission = permission;
		}

		#endregion

		#region Properties

		public bool Granted { get; set; }

		public string PermissionKey { get; set; }

		public int UserId { get; set; }

		public long UserPermissionId { get; set; }

		#endregion

		#region Navigation Properties

		public UserDto User { get; set; }
		public PermissionDto Permission { get; set; }

		#endregion
	}

}
