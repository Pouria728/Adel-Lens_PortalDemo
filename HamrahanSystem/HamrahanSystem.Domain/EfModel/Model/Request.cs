

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class Request {

        public Request()
        {
            this.RequestFieldes = new List<RequestFielde>();
            this.RequestProcesses = new List<RequestProcess>();
            OnCreated();
        }

        public int BaseRequestId { get; set; }

        public DateTime DateCreate { get; set; }

        public Guid RequestId { get; set; }

        public int StatusId { get; set; }

        public int UserId { get; set; }


        public virtual IList<RequestFielde> RequestFieldes { get; set; }


        public virtual IList<RequestProcess> RequestProcesses { get; set; }


        public virtual BaseRequeste BaseRequeste { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
