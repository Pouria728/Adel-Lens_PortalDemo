
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

	public partial class GridDto
	{
		#region Constructors

		public GridDto()
		{
		}

		public GridDto(int id, string sidx, string sord, int page, int rows,int? orderstatusid ,int? customerid ,int indexdocumetid)
		{

			this.Id = id;
			this.Sidx = sidx;
			this.Sord = sord;
			this.Page = page;
			this.Rows = rows;

		}

		#endregion

		#region Properties

		public int Id { get; set; }

		public string Sidx { get; set; }

		public string Sord { get; set; }
		public int? orderstatusid { get; set; }
		public int? statusrequestid { get; set; }
		public int? customerid { get; set; }
		public int? indexdocumetid { get; set; }
		public int? createdById { get; set; }
		public string factorNo { get; set; }
		public string? fromDate { get; set; }
		public string? toDate { get; set; }
		public int Page {
			get => _page;
			set => _page = (value == 0 ? 1 : value);
		}
		public int Rows
		{
			get => _row;
			set => _row = (value == 0 ? 20 : value);

		}


		#endregion

		#region Navigation Properties


		#endregion

		private int _page;
		private int _row;

	}
}
