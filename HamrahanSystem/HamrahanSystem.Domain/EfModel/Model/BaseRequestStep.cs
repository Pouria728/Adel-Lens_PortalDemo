

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class BaseRequestStep {

        public BaseRequestStep()
        {
            OnCreated();
        }

        public int BaseRequestId { get; set; }

        public int BaseRequestStepId { get; set; }

        public int OrderId { get; set; }

        public int RoleId { get; set; }

        public string Title { get; set; }


        public virtual BaseRequeste BaseRequeste { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
