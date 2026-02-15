

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblWfwStepAction {

        public TblWfwStepAction()
        {
            OnCreated();
        }

        public int? ProcessActionId { get; set; }

        public int? ProcessStepId { get; set; }

        public int StepActionId { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
