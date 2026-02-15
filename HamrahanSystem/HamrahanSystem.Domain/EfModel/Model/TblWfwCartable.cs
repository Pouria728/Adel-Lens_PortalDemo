

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblWfwCartable {

        public TblWfwCartable()
        {
            OnCreated();
        }

        public int? AdvertNo { get; set; }

        public long CartableId { get; set; }

        public int? Company { get; set; }

        public string? CreateDate { get; set; }

        public int? DaysNo { get; set; }

        public string? DocumentDate { get; set; }

        public int? DocumentId { get; set; }

        public int? DocumentNo { get; set; }

        public int? DocumentRecNo { get; set; }

        public string? FiscalYear { get; set; }

        public short? IndexDocument { get; set; }

        public int? ProcessStepId { get; set; }

        public short? Status { get; set; }

        public string? UpdateDate { get; set; }

        public int? UserId { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
