

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblClrDefineObject {

        public TblClrDefineObject()
        {
            this.AceNick = 0;
            this.AceOrdered = 0;
            this.AmountOrdered = 0;
            this.IsActive = 1;
            this.MaxBacklog = 0;
            this.MinBacklog = 0;
			this.TblLnsSphCyls = new List<TblLnsSphCyl>();
			this.TblLnsOrderItems = new List<TblLnsOrderItem>();
			OnCreated();
        }

        /// <summary>
        /// نقطه بحران?
        /// </summary>
        public int? AceNick { get; set; }

        /// <summary>
        /// نقطه سفارش
        /// </summary>
        public int? AceOrdered { get; set; }

        /// <summary>
        /// فعال بودن ?الا
        /// </summary>
        public bool? Active { get; set; }

        public bool? ActiveExDate { get; set; }

        public bool? ActivePrDate { get; set; }

        public bool? ActiveTolerance { get; set; }

        /// <summary>
        /// م?زان سفارش
        /// </summary>
        public int? AmountOrdered { get; set; }

        /// <summary>
        /// ?د
        /// </summary>
        public string? CodeObject { get; set; }

        public string? CodeSpecialObject { get; set; }

        public byte Company { get; set; }

        public int DefineObjectId { get; set; }

        public string? DesObject { get; set; }

        public double? Each { get; set; }

        public double? Equal { get; set; }

        /// <summary>
        /// ارتفاع
        /// </summary>
        public double? Height { get; set; }

        public short? IsActive { get; set; }

        /// <summary>
        /// نام لات?ن
        /// </summary>
        public string? LatinNameObject { get; set; }

        /// <summary>
        /// طول
        /// </summary>
        public double? Length { get; set; }

        /// <summary>
        /// حدا?ثر موجود?
        /// </summary>
        public int? MaxBacklog { get; set; }

        /// <summary>
        /// حداقل موجود?
        /// </summary>
        public int? MinBacklog { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public string? NameObject { get; set; }

        public string? NationalCode { get; set; }

        public byte[]? Pic { get; set; }

        public int RecNo { get; set; }

        public int? RefMaster { get; set; }

        public int? RegistryKey { get; set; }

        public int? RnGroup { get; set; }

        public int? RnKind { get; set; }

        /// <summary>
        /// واحد سنجش
        /// </summary>
        public int? RNScruple { get; set; }

        public int? RNSecScruple { get; set; }

        /// <summary>
        /// وزن
        /// </summary>
        public double? Scale { get; set; }

        public bool? Serial { get; set; }

        public bool? Standard { get; set; }

        public byte? StylePrice { get; set; }

        /// <summary>
        /// مشخصات فن?
        /// </summary>
        public string? TechnicalSpecs { get; set; }

        public byte? Tolerance { get; set; }

        public byte? ToleranceOut { get; set; }

        public int? TTMSGoodsType { get; set; }

        /// <summary>
        /// عرض
        /// </summary>
        public double? Width { get; set; }
		public virtual IList<TblLnsSphCyl> TblLnsSphCyls { get; set; }
		public virtual IList<TblLnsOrderItem> TblLnsOrderItems { get; set; }

		#region Extensibility Method Definitions

		partial void OnCreated();

        public override bool Equals(object obj)
        {
          TblClrDefineObject toCompare = obj as TblClrDefineObject;
          if (toCompare == null)
          {
            return false;
          }

          if (!Object.Equals(this.Company, toCompare.Company))
            return false;
          if (!Object.Equals(this.RecNo, toCompare.RecNo))
            return false;

          return true;
        }

        public override int GetHashCode()
        {
          int hashCode = 13;
          hashCode = (hashCode * 7) + Company.GetHashCode();
          hashCode = (hashCode * 7) + RecNo.GetHashCode();
          return hashCode;
        }

        #endregion
    }

}
