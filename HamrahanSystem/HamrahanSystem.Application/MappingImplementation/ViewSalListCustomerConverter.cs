using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class ViewSalListCustomerConverter
    {

        public static ViewSalListCustomerDto ToDto(this ViewSalListCustomer source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static ViewSalListCustomerDto ToDtoWithRelated(this ViewSalListCustomer source, int level)
        {
            if (source == null)
              return null;

            var target = new ViewSalListCustomerDto();

            // Properties
            target.Active = source.Active;
            target.Caption = source.Caption;
            target.CodeCompany = source.CodeCompany;
            target.CodeValuta = source.CodeValuta;
            target.Company = source.Company;
            target.DateOpen = source.DateOpen;
            target.DefineCustomerId = source.DefineCustomerId;
            target.Expr1 = source.Expr1;
            target.NameFormal = source.NameFormal;
            target.NameFormalEN = source.NameFormalEN;
            target.RecNo = source.RecNo;
            target.RefCustomer = source.RefCustomer;
            target.RefMasterCompany = source.RefMasterCompany;
            target.RefMasterKey = source.RefMasterKey;
            target.RNGroupFormal = source.RNGroupFormal;
            target.RNValuta = source.RNValuta;
            target.SActive = source.SActive;
            target.StateFormal = source.StateFormal;
            target.TypeFormal = source.TypeFormal;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static ViewSalListCustomer ToEntity(this ViewSalListCustomerDto source)
        {
            if (source == null)
              return null;

            var target = new ViewSalListCustomer();

            // Properties
            target.Active = source.Active;
            target.Caption = source.Caption;
            target.CodeCompany = source.CodeCompany;
            target.CodeValuta = source.CodeValuta;
            target.Company = source.Company;
            target.DateOpen = source.DateOpen;
            target.DefineCustomerId = source.DefineCustomerId;
            target.Expr1 = source.Expr1;
            target.NameFormal = source.NameFormal;
            target.NameFormalEN = source.NameFormalEN;
            target.RecNo = source.RecNo;
            target.RefCustomer = source.RefCustomer;
            target.RefMasterCompany = source.RefMasterCompany;
            target.RefMasterKey = source.RefMasterKey;
            target.RNGroupFormal = source.RNGroupFormal;
            target.RNValuta = source.RNValuta;
            target.SActive = source.SActive;
            target.StateFormal = source.StateFormal;
            target.TypeFormal = source.TypeFormal;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<ViewSalListCustomerDto> ToDtos(this IEnumerable<ViewSalListCustomer> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<ViewSalListCustomerDto> ToDtos(this List<ViewSalListCustomer> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<ViewSalListCustomerDto> ToDtosWithRelated(this IEnumerable<ViewSalListCustomer> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<ViewSalListCustomerDto> ToDtosWithRelated(this List<ViewSalListCustomer> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<ViewSalListCustomer> ToEntities(this IEnumerable<ViewSalListCustomerDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }
		public static List<ViewSalListCustomer> ToEntities(this List<ViewSalListCustomerDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}

		static partial void OnDtoCreating(ViewSalListCustomer source, ViewSalListCustomerDto target);

        static partial void OnEntityCreating(ViewSalListCustomerDto source, ViewSalListCustomer target);

    }

}
