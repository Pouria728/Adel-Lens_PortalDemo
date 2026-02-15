

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblWfwOrderProcessStepDto
    {
        #region Constructors

        public TblWfwOrderProcessStepDto() {
        }

        public TblWfwOrderProcessStepDto(System.DateTime? dateComplete, System.DateTime dateCreate, long orderProcessId, long orderProcessStepId, int processStepId, int statusId, int? userId, TblWfwOrderProcessDto tblWfwOrderProcess, UserDto user, TblWfwProcessStepDto tblWfwProcessStep , string factorNo="") {

            this.FactorNo= factorNo;
          this.DateComplete = dateComplete;
          this.DateCreate = dateCreate;
          this.OrderProcessId = orderProcessId;
          this.OrderProcessStepId = orderProcessStepId;
          this.ProcessStepId = processStepId;
          this.StatusId = statusId;
          this.UserId = userId;
          this.TblWfwOrderProcess = tblWfwOrderProcess;
          this.User = user;
          this.TblWfwProcessStep = tblWfwProcessStep;
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// شماره فاکتور
        /// </summary>
        public string FactorNo{get;set;}
        public System.DateTime? DateComplete { get; set; }

        public System.DateTime DateCreate { get; set; }

        public long OrderProcessId { get; set; }

        public long OrderProcessStepId { get; set; }

        public int ProcessStepId { get; set; }

        public int StatusId { get; set; }

        public int? UserId { get; set; }

        #endregion

        #region Navigation Properties

        public TblWfwOrderProcessDto TblWfwOrderProcess { get; set; }

        public UserDto User { get; set; }

        public TblWfwProcessStepDto TblWfwProcessStep { get; set; }

        #endregion
    }

}
