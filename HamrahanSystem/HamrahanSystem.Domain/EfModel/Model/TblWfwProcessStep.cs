

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblWfwProcessStep {

        public TblWfwProcessStep()
        {
            this.AllowNextStep = false;
            this.IsPrint = false;
			this.TblWfwOrderProcessSteps = new List<TblWfwOrderProcessStep>();
			OnCreated();
        }

        public bool AllowNextStep { get; set; }

        public string? Code { get; set; }

        public int? Company { get; set; }

        public string? CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string? Description { get; set; }

        public short? IsActive { get; set; }

        public bool IsPrint { get; set; }

        public string? Leyout { get; set; }

        public int? ModifiedBy { get; set; }

        public string? ModifiedDate { get; set; }

        public string? Name { get; set; }

        public int? ProcessId { get; set; }
		public int OrderId { get; set; }

		public bool IsBarcode { get; set; }
		public int ProcedureId { get; set; }

		public int ProcessStepId { get; set; }
        public IList<TblWfwRoleStep> TblWfwRoleStepes { get; set; }
		public IList<TblWfwOrderProcessStep> TblWfwOrderProcessSteps { get; set; }
		public TblWfwProcess TblWfwProcess { get; set; }

		#region Extensibility Method Definitions

		partial void OnCreated();

        #endregion
    }

}
