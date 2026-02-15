

using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

	public partial class TblWfwProcessDto
	{
		#region Constructors

		public TblWfwProcessDto()
		{
		}

		public TblWfwProcessDto(string code, short? codeSystem, int? company, string createDate, int? createdBy, string description, short? indexDocument, bool isActive, int? modifiedBy, string modifiedDate, string name, int processId, List<TblWfwProcessStepDto> tblWfwProcessSteps, List<TblWfwOrderProcessDto> tblWfwOrderProcesses)
		{

			this.Code = code;
			this.CodeSystem = codeSystem;
			this.Company = company;
			this.CreateDate = createDate;
			this.CreatedBy = createdBy;
			this.Description = description;
			this.IndexDocument = indexDocument;
			this.IsActive = isActive;
			this.ModifiedBy = modifiedBy;
			this.ModifiedDate = modifiedDate;
			this.Name = name;
			this.ProcessId = processId;
			this.TblWfwProcessSteps = tblWfwProcessSteps;
			this.TblWfwOrderProcesses = tblWfwOrderProcesses;

		}

		#endregion

		#region Properties

		public string Code { get; set; }

		public short? CodeSystem { get; set; }

		public int? Company { get; set; }

		public string CreateDate { get; set; }

		public int? CreatedBy { get; set; }

		public string Description { get; set; }

		public short? IndexDocument { get; set; }

		public bool IsActive { get; set; }

		public int? ModifiedBy { get; set; }

		public string ModifiedDate { get; set; }

		public string Name { get; set; }

		public int ProcessId { get; set; }
		public List<TblWfwProcessStepDto> TblWfwProcessSteps { get; set; }
		public List<TblWfwOrderProcessDto> TblWfwOrderProcesses { get; set; }
		#endregion
	}

}
