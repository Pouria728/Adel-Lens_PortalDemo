
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblWfwRelationStepCondition {

        public TblWfwRelationStepCondition()
        {
            OnCreated();
        }

        public int? ConditionId { get; set; }

        public string? OkResult { get; set; }

        public int RelationStepConditionId { get; set; }

        public int? RelationStepId { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
