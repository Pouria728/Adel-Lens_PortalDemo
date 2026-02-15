

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class RequestFielde {

        public RequestFielde()
        {
            OnCreated();
        }

        public int BaseRequestFieldId { get; set; }

        public string DataValue { get; set; }

        public Guid RequestFieldId { get; set; }

        public Guid RequestId { get; set; }


        public virtual Request Request { get; set; }


        public virtual BaseRequestField BaseRequestFielded { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
