

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class BaseRequestField
    {

        public BaseRequestField()
        {
            this.BaseRequestFieldes_BaseRequestParentFieldId = new List<BaseRequestField>();
            this.RequestFieldes = new List<RequestFielde>();
            OnCreated();
        }

        public bool AllowNull { get; set; }

        public int BaseRequestFieldId { get; set; }

        public int BaseRequestId { get; set; }

        public int? BaseRequestParentFieldId { get; set; }

        public int DataTypeId { get; set; }

        public int FieldType { get; set; }

        public int MaxRecord { get; set; }

        public string Title { get; set; }


        public virtual IList<BaseRequestField> BaseRequestFieldes_BaseRequestParentFieldId { get; set; }


        public virtual BaseRequestField BaseRequestFielde_BaseRequestParentFieldId { get; set; }


        public virtual BaseRequeste BaseRequeste { get; set; }


        public virtual IList<RequestFielde> RequestFieldes { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
