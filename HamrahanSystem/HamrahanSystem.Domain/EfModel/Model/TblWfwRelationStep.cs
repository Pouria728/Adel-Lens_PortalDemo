

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblWfwRelationStep {

        public TblWfwRelationStep()
        {
            OnCreated();
        }

        public int? FromProcessStepId { get; set; }

        public int RelationStepId { get; set; }

        public int? StepActionId { get; set; }

        public int? ToProcessStepId { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
