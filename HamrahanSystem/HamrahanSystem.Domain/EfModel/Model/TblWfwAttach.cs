

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblWfwAttach {

        public TblWfwAttach()
        {
            OnCreated();
        }

        public long AttachId { get; set; }

        public long? CartableId { get; set; }

        public string? Code { get; set; }

        public int? Company { get; set; }

        public string? CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public string? Description { get; set; }

        public int? DocumentId { get; set; }

        public string? FileStock { get; set; }

        public short? IndexDocument { get; set; }

        public short? IsActive { get; set; }

        public int? ModifiedBy { get; set; }

        public string? ModifiedDate { get; set; }

        public string? Name { get; set; }

        public string? Suffix { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
