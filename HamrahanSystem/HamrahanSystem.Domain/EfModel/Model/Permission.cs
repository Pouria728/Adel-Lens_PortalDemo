
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class Permission
	{

        public Permission()
        {
            this.RolePermissions = new List<RolePermission>();
            
            this.UserPermissions = new List<UserPermission>();
            OnCreated();
        }

        public int PermissionId { get; set; }

        public string? PermissionKey { get; set; }

        public string? Title { get; set; }


        public virtual IList<RolePermission> RolePermissions { get; set; }

        public virtual IList<UserPermission> UserPermissions { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
