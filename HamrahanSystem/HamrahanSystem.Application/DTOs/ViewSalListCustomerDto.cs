

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class ViewSalListCustomerDto
	{
		#region Constructors

		public ViewSalListCustomerDto()
		{
		}

		public ViewSalListCustomerDto(bool? active, string caption, string codeCompany, int? codeValuta, byte company, string dateOpen, int? defineCustomerId, string expr1, string nameFormal, string nameFormalEN, int recNo, int? refCustomer, byte? refMasterCompany, int? refMasterKey, int rNGroupFormal, int? rNValuta, string sActive, byte? stateFormal, string typeFormal)
		{

			this.Active = active;
			this.Caption = caption;
			this.CodeCompany = codeCompany;
			this.CodeValuta = codeValuta;
			this.Company = company;
			this.DateOpen = dateOpen;
			this.DefineCustomerId = defineCustomerId;
			this.Expr1 = expr1;
			this.NameFormal = nameFormal;
			this.NameFormalEN = nameFormalEN;
			this.RecNo = recNo;
			this.RefCustomer = refCustomer;
			this.RefMasterCompany = refMasterCompany;
			this.RefMasterKey = refMasterKey;
			this.RNGroupFormal = rNGroupFormal;
			this.RNValuta = rNValuta;
			this.SActive = sActive;
			this.StateFormal = stateFormal;
			this.TypeFormal = typeFormal;
		}

		#endregion

		#region Properties

		public bool? Active { get; set; }

		public string Caption { get; set; }

		public string CodeCompany { get; set; }

		public int? CodeValuta { get; set; }

		public byte Company { get; set; }

		public string DateOpen { get; set; }

		public int? DefineCustomerId { get; set; }

		public string Expr1 { get; set; }

		public string NameFormal { get; set; }

		public string NameFormalEN { get; set; }

		public int RecNo { get; set; }

		public int? RefCustomer { get; set; }

		public byte? RefMasterCompany { get; set; }

		public int? RefMasterKey { get; set; }

		public int RNGroupFormal { get; set; }

		public int? RNValuta { get; set; }

		public string SActive { get; set; }

		public byte? StateFormal { get; set; }

		public string TypeFormal { get; set; }

		#endregion
	}

}
