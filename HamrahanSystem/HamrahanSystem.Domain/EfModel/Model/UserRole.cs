

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class UserRole {

        public UserRole()
        {
            OnCreated();
        }

        public int RoleId { get; set; }

        public int UserId { get; set; }

        public long UserRoleId { get; set; }

        public virtual User User { get; set; }

        public virtual Role Role { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
