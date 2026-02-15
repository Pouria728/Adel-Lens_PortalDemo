

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class RequestProcessStep {

        public RequestProcessStep()
        {
            OnCreated();
        }

        public DateTime Datecreate { get; set; }

        public Guid RequestProcessId { get; set; }

        public Guid RequestProcessStepId { get; set; }

        public int RoleId { get; set; }

        public int StatusId { get; set; }

        public int? UserId { get; set; }


        public virtual RequestProcess RequestProcess { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
