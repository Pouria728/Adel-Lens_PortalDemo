

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblWfwUserCartable {

        public TblWfwUserCartable()
        {
            OnCreated();
        }

        public short? Active { get; set; }

        public long? CartableId { get; set; }

        public string? Paraph { get; set; }

        public int? StatusId { get; set; }

        public string? UpdateDate { get; set; }

        public long UserCartableId { get; set; }

        public int? UserId { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
