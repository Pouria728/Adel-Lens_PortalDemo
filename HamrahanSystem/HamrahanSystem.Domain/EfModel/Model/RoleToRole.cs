

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class RoleToRole {

        public RoleToRole()
        {
            OnCreated();
        }

        public int RoleId { get; set; }

        public int RolesId { get; set; }

        public long RoleToRolesId { get; set; }

        public virtual Role Role_RoleId { get; set; }

        public virtual Role Role_RolesId { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
