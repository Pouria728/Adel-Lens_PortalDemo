

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

	public partial class TblLnsSphCylDto
	{
		#region Constructors

		public TblLnsSphCylDto()
		{
		}

		public TblLnsSphCylDto(int? cylId, int? defineObjectId, int? lensIndexRSphId, int orderId, int sphCylId, int? stock, List<TblLnsOrderItemDto> tblLnsOrderItems, TblLnsCylDto tblLnsCyl, TblLnsLensIndexRSphDto tblLnsLensIndexRSph, TblClrDefineObjectDto tblClrDefineObject)
		{

			this.CylId = cylId;
			this.DefineObjectId = defineObjectId;
			this.LensIndexRSphId = lensIndexRSphId;
			this.OrderId = orderId;
			this.SphCylId = sphCylId;
			this.Stock = stock;
			this.TblLnsOrderItems = tblLnsOrderItems;
			this.TblLnsCyl = tblLnsCyl;
			this.TblLnsLensIndexRSph = tblLnsLensIndexRSph;
			this.TblClrDefineObject = tblClrDefineObject;
		}

		#endregion

		#region Properties

		public int? CylId { get; set; }

		public int? DefineObjectId { get; set; }

		public int? LensIndexRSphId { get; set; }

		public int OrderId { get; set; }

		public int SphCylId { get; set; }

		public int? Stock { get; set; }

		#endregion

		#region Navigation Properties

		public List<TblLnsOrderItemDto> TblLnsOrderItems { get; set; }

		public TblLnsCylDto TblLnsCyl { get; set; }
		public TblClrDefineObjectDto TblClrDefineObject { get; set; }

		public TblLnsLensIndexRSphDto TblLnsLensIndexRSph { get; set; }

		#endregion
	}

}
