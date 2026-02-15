

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

	public partial class TblWfwRoleStepDto
	{
		#region Constructors

		public TblWfwRoleStepDto()
		{
		}

		public TblWfwRoleStepDto(int processStepId, int roleId, int roleStepId,RoleDto role,TblWfwProcessStepDto tblWfwProcessStep)
		{

			this.ProcessStepId = processStepId;
			this.RoleId = roleId;
			this.RoleStepId = roleStepId;
			this.TblWfwProcessStep = tblWfwProcessStep;
			this.Role = role;
		}

		#endregion

		#region Properties

		public int ProcessStepId { get; set; }

		public int RoleId { get; set; }

		public int RoleStepId { get; set; }
		public TblWfwProcessStepDto TblWfwProcessStep { get; set; }
		public RoleDto Role { get; set; }

		#endregion
	}

}
