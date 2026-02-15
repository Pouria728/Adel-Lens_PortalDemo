using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblClrDefineObjectConverter
    {

        public static TblClrDefineObjectDto ToDto(this TblClrDefineObject source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblClrDefineObjectDto ToDtoWithRelated(this TblClrDefineObject source, int level)
        {
            if (source == null)
              return null;

            var target = new TblClrDefineObjectDto();

            // Properties
            target.AceNick = source.AceNick;
            target.AceOrdered = source.AceOrdered;
            target.Active = source.Active;
            target.ActiveExDate = source.ActiveExDate;
            target.ActivePrDate = source.ActivePrDate;
            target.ActiveTolerance = source.ActiveTolerance;
            target.AmountOrdered = source.AmountOrdered;
            target.CodeObject = source.CodeObject;
            target.CodeSpecialObject = source.CodeSpecialObject;
            target.Company = source.Company;
            target.DefineObjectId = source.DefineObjectId;
            target.DesObject = source.DesObject;
            target.Each = source.Each;
            target.Equal = source.Equal;
            target.Height = source.Height;
            target.IsActive = source.IsActive;
            target.LatinNameObject = source.LatinNameObject;
            target.Length = source.Length;
            target.MaxBacklog = source.MaxBacklog;
            target.MinBacklog = source.MinBacklog;
            target.NameObject = source.NameObject;
            target.NationalCode = source.NationalCode;
            target.Pic = source.Pic;
            target.RecNo = source.RecNo;
            target.RefMaster = source.RefMaster;
            target.RegistryKey = source.RegistryKey;
            target.RnGroup = source.RnGroup;
            target.RnKind = source.RnKind;
            target.RNScruple = source.RNScruple;
            target.RNSecScruple = source.RNSecScruple;
            target.Scale = source.Scale;
            target.Serial = source.Serial;
            target.Standard = source.Standard;
            target.StylePrice = source.StylePrice;
            target.TechnicalSpecs = source.TechnicalSpecs;
            target.Tolerance = source.Tolerance;
            target.ToleranceOut = source.ToleranceOut;
            target.TTMSGoodsType = source.TTMSGoodsType;
            target.Width = source.Width;

			// Navigation Properties
			if (level > 0)
			{
				target.TblLnsOrderItems = source.TblLnsOrderItems.ToDtosWithRelated(level - 1);
				target.TblLnsSphCyls = source.TblLnsSphCyls.ToDtosWithRelated(level - 1);

			}


			// User-defined partial method
			OnDtoCreating(source, target);

            return target;
        }

        public static TblClrDefineObject ToEntity(this TblClrDefineObjectDto source)
        {
            if (source == null)
              return null;

            var target = new TblClrDefineObject();

            // Properties
            target.AceNick = source.AceNick;
            target.AceOrdered = source.AceOrdered;
            target.Active = source.Active;
            target.ActiveExDate = source.ActiveExDate;
            target.ActivePrDate = source.ActivePrDate;
            target.ActiveTolerance = source.ActiveTolerance;
            target.AmountOrdered = source.AmountOrdered;
            target.CodeObject = source.CodeObject;
            target.CodeSpecialObject = source.CodeSpecialObject;
            target.Company = source.Company;
            target.DefineObjectId = source.DefineObjectId;
            target.DesObject = source.DesObject;
            target.Each = source.Each;
            target.Equal = source.Equal;
            target.Height = source.Height;
            target.IsActive = source.IsActive;
            target.LatinNameObject = source.LatinNameObject;
            target.Length = source.Length;
            target.MaxBacklog = source.MaxBacklog;
            target.MinBacklog = source.MinBacklog;
            target.NameObject = source.NameObject;
            target.NationalCode = source.NationalCode;
            target.Pic = source.Pic;
            target.RecNo = source.RecNo;
            target.RefMaster = source.RefMaster;
            target.RegistryKey = source.RegistryKey;
            target.RnGroup = source.RnGroup;
            target.RnKind = source.RnKind;
            target.RNScruple = source.RNScruple;
            target.RNSecScruple = source.RNSecScruple;
            target.Scale = source.Scale;
            target.Serial = source.Serial;
            target.Standard = source.Standard;
            target.StylePrice = source.StylePrice;
            target.TechnicalSpecs = source.TechnicalSpecs;
            target.Tolerance = source.Tolerance;
            target.ToleranceOut = source.ToleranceOut;
            target.TTMSGoodsType = source.TTMSGoodsType;
            target.Width = source.Width;


            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblClrDefineObjectDto> ToDtos(this IEnumerable<TblClrDefineObject> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblClrDefineObjectDto> ToDtosWithRelated(this IEnumerable<TblClrDefineObject> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblClrDefineObject> ToEntities(this IEnumerable<TblClrDefineObjectDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblClrDefineObject source, TblClrDefineObjectDto target);

        static partial void OnEntityCreating(TblClrDefineObjectDto source, TblClrDefineObject target);

    }

}
