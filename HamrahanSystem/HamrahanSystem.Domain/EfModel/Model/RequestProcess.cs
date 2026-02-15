

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class RequestProcess {

        public RequestProcess()
        {
            this.RequestProcessSteps = new List<RequestProcessStep>();
            OnCreated();
        }

        public DateTime CreateDate { get; set; }

        public DateTime? DateComplete { get; set; }

        public Guid RequestId { get; set; }

        public Guid RequestProcessId { get; set; }

        public int StatusId { get; set; }


        public virtual Request Request { get; set; }


        public virtual IList<RequestProcessStep> RequestProcessSteps { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
