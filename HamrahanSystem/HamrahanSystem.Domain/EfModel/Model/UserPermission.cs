

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class UserPermission {

        public UserPermission()
        {
            this.Granted = true;
            OnCreated();
        }

        public bool Granted { get; set; }

        public string? PermissionKey { get; set; }

        public int UserId { get; set; }

        public long UserPermissionId { get; set; }

		public int? PermissionId { get; set; }

		public virtual User User { get; set; }
		public virtual Permission Permission { get; set; }

		#region Extensibility Method Definitions

		partial void OnCreated();

        #endregion
    }

}
