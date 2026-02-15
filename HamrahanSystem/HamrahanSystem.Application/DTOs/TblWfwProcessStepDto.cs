

using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

	public partial class TblWfwProcessStepDto
	{
		#region Constructors

		public TblWfwProcessStepDto()
		{
		}

		public TblWfwProcessStepDto(bool allowNextStep, string code, int? company, string createDate, int? createdBy, string description, bool isActive, bool isPrint, string leyout, int? modifiedBy, string modifiedDate, string name, int? processId, int orderId, int processStepId,int procedureId,bool isBarcode, List<TblWfwRoleStepDto> tblWfwRoleSteps, TblWfwProcessDto tblWfwProcess, List<TblWfwOrderProcessStepDto> tblWfwOrderProcessSteps)
		{

			this.AllowNextStep = allowNextStep;
			this.Code = code;
			this.Company = company;
			this.CreateDate = createDate;
			this.CreatedBy = createdBy;
			this.Description = description;
			this.IsActive = isActive;
			this.IsPrint = isPrint;
			this.Leyout = leyout;
			this.ModifiedBy = modifiedBy;
			this.ModifiedDate = modifiedDate;
			this.Name = name;
			this.ProcessId = processId;
			this.ProcessStepId = processStepId;
			this.TblWfwRoleStepes = tblWfwRoleSteps;
			this.TblWfwProcess = tblWfwProcess;
			this.OrderId = orderId;
			this.TblWfwOrderProcessSteps = tblWfwOrderProcessSteps;
			this.IsBarcode = isBarcode;
			this.ProcedureId = procedureId;
		}

		#endregion

		#region Properties

		public bool AllowNextStep { get; set; }

		public string Code { get; set; }

		public int? Company { get; set; }

		public string CreateDate { get; set; }

		public int? CreatedBy { get; set; }

		public string Description { get; set; }

		public bool IsActive { get; set; }

		public bool IsPrint { get; set; }

		public string Leyout { get; set; }

		public int? ModifiedBy { get; set; }

		public string ModifiedDate { get; set; }

		public string Name { get; set; }

		public int? ProcessId { get; set; }
		public int ProcedureId { get; set; }
		public bool IsBarcode { get; set; }
		public int OrderId { get; set; }

		public int ProcessStepId { get; set; }
		public List<TblWfwRoleStepDto> TblWfwRoleStepes { get; set; }
		public TblWfwProcessDto TblWfwProcess { get; set; }
		public List<TblWfwOrderProcessStepDto> TblWfwOrderProcessSteps { get; set; }

		#endregion
	}

}
