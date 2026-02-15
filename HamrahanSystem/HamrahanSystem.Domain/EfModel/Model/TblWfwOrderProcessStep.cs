using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblWfwOrderProcessStep {

        public TblWfwOrderProcessStep()
        {
            OnCreated();
        }
      
        public DateTime? DateComplete { get; set; }

        public DateTime DateCreate { get; set; }

        public long OrderProcessId { get; set; }

        public long OrderProcessStepId { get; set; }

        public int ProcessStepId { get; set; }

        public int StatusId { get; set; }

        public int? UserId { get; set; }

        public virtual TblWfwOrderProcess TblWfwOrderProcess { get; set; }

        public virtual User User { get; set; }

        public virtual TblWfwProcessStep TblWfwProcessStep { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
