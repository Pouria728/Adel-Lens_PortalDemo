

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class UserRoleDto
    {
        #region Constructors

        public UserRoleDto() {
        }

        public UserRoleDto(int roleId, int userId, long userRoleId, UserDto user, RoleDto role) {

          this.RoleId = roleId;
          this.UserId = userId;
          this.UserRoleId = userRoleId;
          this.User = user;
          this.Role = role;
        }

        #endregion

        #region Properties

        public int RoleId { get; set; }

        public int UserId { get; set; }

        public long UserRoleId { get; set; }

        #endregion

        #region Navigation Properties

        public UserDto User { get; set; }

        public RoleDto Role { get; set; }

        #endregion
    }

}
