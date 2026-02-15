

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsTemp {

        public TblLnsTemp()
        {
            OnCreated();
        }

        public int? C000 { get; set; }

        public int? C025 { get; set; }

        public int? C050 { get; set; }

        public int? C075 { get; set; }

        public int? C100 { get; set; }

        public int? C125 { get; set; }

        public int? C150 { get; set; }

        public int? C175 { get; set; }

        public int? C200 { get; set; }

        public int? C225 { get; set; }

        public int? C250 { get; set; }

        public int? C275 { get; set; }

        public int? C300 { get; set; }

        public int? C325 { get; set; }

        public int? C350 { get; set; }

        public int? C375 { get; set; }

        public int? C400 { get; set; }

        public int? C425 { get; set; }

        public int? C450 { get; set; }

        public int? C475 { get; set; }

        public int? C500 { get; set; }

        public int? C525 { get; set; }

        public int? C550 { get; set; }

        public int? C575 { get; set; }

        public int? C600 { get; set; }

        public long TempId { get; set; }

        public string? TempRow { get; set; }

        public int? UserId { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
