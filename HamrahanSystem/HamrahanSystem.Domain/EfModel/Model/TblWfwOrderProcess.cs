

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblWfwOrderProcess {

        public TblWfwOrderProcess()
        {
            this.TblWfwOrderProcessSteps = new List<TblWfwOrderProcessStep>();
            OnCreated();
        }

        public DateTime? DateComplete { get; set; }

        public DateTime DateCreate { get; set; }

        public long OrderId { get; set; }

        public long OrderProcessId { get; set; }

        public int ProcessId { get; set; }

        public int StatusId { get; set; }

        public virtual TblWfwProcess TblWfwProcess { get; set; }

        public virtual TblLnsOrder TblLnsOrder { get; set; }

        public virtual IList<TblWfwOrderProcessStep> TblWfwOrderProcessSteps { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
