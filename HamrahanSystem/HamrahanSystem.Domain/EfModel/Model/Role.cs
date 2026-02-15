
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class Role {

        public Role()
        {
            this.TenantId = 1;
            this.RolePermissions = new List<RolePermission>();
            this.RoleToRoles_RoleId = new List<RoleToRole>();
            this.RoleToRoles_RolesId = new List<RoleToRole>();
            this.UserRoles = new List<UserRole>();
            OnCreated();
        }

        public int RoleId { get; set; }

        public string? RoleKey { get; set; }

        public string? RoleName { get; set; }

        public int TenantId { get; set; }

        public virtual IList<RolePermission> RolePermissions { get; set; }

        public virtual IList<RoleToRole> RoleToRoles_RoleId { get; set; }

        public virtual IList<RoleToRole> RoleToRoles_RolesId { get; set; }

        public virtual IList<UserRole> UserRoles { get; set; }
		public virtual IList<TblWfwRoleStep> TblWfwRoleStepes { get; set; }

		#region Extensibility Method Definitions

		partial void OnCreated();

        #endregion
    }

}
