

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

	public partial class TblClrDefineObjectDto
	{
		#region Constructors

		public TblClrDefineObjectDto()
		{
		}

		public TblClrDefineObjectDto(int? aceNick, int? aceOrdered, bool? active, bool? activeExDate, bool? activePrDate, bool? activeTolerance, int? amountOrdered, string codeObject, string codeSpecialObject, byte company, int defineObjectId, string desObject, double? each, double? equal, double? height, short? isActive, string latinNameObject, double? length, int? maxBacklog, int? minBacklog, string nameObject, string nationalCode, byte[] pic, int recNo, int? refMaster, int? registryKey, int? rnGroup, int? rnKind, int? rNScruple, int? rNSecScruple, double? scale, bool? serial, bool? standard, byte? stylePrice, string technicalSpecs, byte? tolerance, byte? toleranceOut, int? tTMSGoodsType, double? width, List<TblLnsSphCylDto> tblLnsSphCyls, List<TblLnsOrderItemDto> tblLnsOrderItems)
		{

			this.AceNick = aceNick;
			this.AceOrdered = aceOrdered;
			this.Active = active;
			this.ActiveExDate = activeExDate;
			this.ActivePrDate = activePrDate;
			this.ActiveTolerance = activeTolerance;
			this.AmountOrdered = amountOrdered;
			this.CodeObject = codeObject;
			this.CodeSpecialObject = codeSpecialObject;
			this.Company = company;
			this.DefineObjectId = defineObjectId;
			this.DesObject = desObject;
			this.Each = each;
			this.Equal = equal;
			this.Height = height;
			this.IsActive = isActive;
			this.LatinNameObject = latinNameObject;
			this.Length = length;
			this.MaxBacklog = maxBacklog;
			this.MinBacklog = minBacklog;
			this.NameObject = nameObject;
			this.NationalCode = nationalCode;
			this.Pic = pic;
			this.RecNo = recNo;
			this.RefMaster = refMaster;
			this.RegistryKey = registryKey;
			this.RnGroup = rnGroup;
			this.RnKind = rnKind;
			this.RNScruple = rNScruple;
			this.RNSecScruple = rNSecScruple;
			this.Scale = scale;
			this.Serial = serial;
			this.Standard = standard;
			this.StylePrice = stylePrice;
			this.TechnicalSpecs = technicalSpecs;
			this.Tolerance = tolerance;
			this.ToleranceOut = toleranceOut;
			this.TTMSGoodsType = tTMSGoodsType;
			this.Width = width;
			this.TblLnsSphCyls = tblLnsSphCyls;
			this.TblLnsOrderItems=tblLnsOrderItems;
		}

		#endregion

		#region Properties

		public int? AceNick { get; set; }

		public int? AceOrdered { get; set; }

		public bool? Active { get; set; }

		public bool? ActiveExDate { get; set; }

		public bool? ActivePrDate { get; set; }

		public bool? ActiveTolerance { get; set; }

		public int? AmountOrdered { get; set; }

		public string CodeObject { get; set; }

		public string CodeSpecialObject { get; set; }

		public byte Company { get; set; }

		public int DefineObjectId { get; set; }

		public string DesObject { get; set; }

		public double? Each { get; set; }

		public double? Equal { get; set; }

		public double? Height { get; set; }

		public short? IsActive { get; set; }

		public string LatinNameObject { get; set; }

		public double? Length { get; set; }

		public int? MaxBacklog { get; set; }

		public int? MinBacklog { get; set; }

		public string NameObject { get; set; }

		public string NationalCode { get; set; }

		public byte[] Pic { get; set; }

		public int RecNo { get; set; }

		public int? RefMaster { get; set; }

		public int? RegistryKey { get; set; }

		public int? RnGroup { get; set; }

		public int? RnKind { get; set; }

		public int? RNScruple { get; set; }

		public int? RNSecScruple { get; set; }

		public double? Scale { get; set; }

		public bool? Serial { get; set; }

		public bool? Standard { get; set; }

		public byte? StylePrice { get; set; }

		public string TechnicalSpecs { get; set; }

		public byte? Tolerance { get; set; }

		public byte? ToleranceOut { get; set; }

		public int? TTMSGoodsType { get; set; }

		public double? Width { get; set; }
		public List<TblLnsSphCylDto> TblLnsSphCyls { get; set; }
		public List<TblLnsOrderItemDto> TblLnsOrderItems { get; set; }

		#endregion
	}

}
