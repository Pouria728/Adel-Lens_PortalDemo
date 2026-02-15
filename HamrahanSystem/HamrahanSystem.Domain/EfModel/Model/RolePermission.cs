
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class RolePermission {

        public RolePermission()
        {
            OnCreated();
        }

        public string? PermissionKey { get; set; }

        public int RoleId { get; set; }

        public long RolePermissionId { get; set; }
		public int PermissionId { get; set; }
		public virtual Role Role { get; set; }
		
		public virtual Permission Permission { get; set; }

		#region Extensibility Method Definitions

		partial void OnCreated();

        #endregion
    }

}
