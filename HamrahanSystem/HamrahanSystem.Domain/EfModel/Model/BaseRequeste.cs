

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class BaseRequeste
    {

        public BaseRequeste()
        {
            this.BaseRequestSteps = new List<BaseRequestStep>();
            this.Requests = new List<Request>();
            this.BaseRequestFieldes = new List<BaseRequestField>();
            OnCreated();
        }

        public int BaseRequestId { get; set; }

        public DateTime? DateCreate { get; set; }

        public bool IsActive { get; set; }

        public string Title { get; set; }

        public int TypeRequest { get; set; }


        public virtual IList<BaseRequestStep> BaseRequestSteps { get; set; }


        public virtual IList<Request> Requests { get; set; }


        public virtual IList<BaseRequestField> BaseRequestFieldes { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
