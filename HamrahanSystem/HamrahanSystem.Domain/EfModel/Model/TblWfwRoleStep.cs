

using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblWfwRoleStep {

        public TblWfwRoleStep()
        {
            OnCreated();
        }

        public int ProcessStepId { get; set; }

        public int RoleId { get; set; }

        public int RoleStepId { get; set; }
        
        public TblWfwProcessStep TblWfwProcessStep { get; set; }

        public Role Role { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
