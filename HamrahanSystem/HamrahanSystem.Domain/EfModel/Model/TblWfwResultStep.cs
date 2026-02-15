
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblWfwResultStep {

        public TblWfwResultStep()
        {
            OnCreated();
        }

        public string? Code { get; set; }

        public string? Command { get; set; }

        public short CommandType { get; set; }

        public int? Company { get; set; }

        public string? CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string? Description { get; set; }

        public short? IsActive { get; set; }

        public int? ModifiedBy { get; set; }

        public string? ModifiedDate { get; set; }

        public string? Name { get; set; }

        public int? RelationStepId { get; set; }

        public int ResultStepId { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
