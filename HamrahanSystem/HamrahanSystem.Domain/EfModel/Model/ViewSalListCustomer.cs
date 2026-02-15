
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{

	public partial class ViewSalListCustomer
	{

		public ViewSalListCustomer()
		{
			OnCreated();
		}

		public bool? Active { get; set; }

		public string? Caption { get; set; }

		public string? CodeCompany { get; set; }

		public int? CodeValuta { get; set; }

		public byte Company { get; set; }

		public string? DateOpen { get; set; }

		public int? DefineCustomerId { get; set; }

		public string? Expr1 { get; set; }

		public string? NameFormal { get; set; }

		public string? NameFormalEN { get; set; }

		public int RecNo { get; set; }

		public int? RefCustomer { get; set; }

		public byte? RefMasterCompany { get; set; }

		public int? RefMasterKey { get; set; }

		public int RNGroupFormal { get; set; }

		public int? RNValuta { get; set; }

		public string? SActive { get; set; }

		public byte? StateFormal { get; set; }

		public string? TypeFormal { get; set; }

		#region Extensibility Method Definitions

		partial void OnCreated();

		#endregion
	}

}
