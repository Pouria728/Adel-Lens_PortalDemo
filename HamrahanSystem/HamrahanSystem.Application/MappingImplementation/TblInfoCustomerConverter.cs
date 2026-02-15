using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblInfoCustomerConverter
    {

        public static TblInfoCustomerDto ToDto(this TblInfoCustomer source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblInfoCustomerDto ToDtoWithRelated(this TblInfoCustomer source, int level)
        {
            if (source == null)
              return null;

            var target = new TblInfoCustomerDto();

            // Properties
            target.Address = source.Address;
            target.BirthDate = source.BirthDate;
            target.BirthPlace = source.BirthPlace;
            target.BranchCode = source.BranchCode;
            target.BranchName = source.BranchName;
            target.Code = source.Code;
            target.Company = source.Company;
            target.Email = source.Email;
            target.ExportDate = source.ExportDate;
            target.ExportPlace = source.ExportPlace;
            target.FatherName = source.FatherName;
            target.Fax = source.Fax;
            target.ID = source.ID;
            target.InfoCustomerId = source.InfoCustomerId;
            target.IsActive = source.IsActive;
            target.MailBox = source.MailBox;
            target.Mobile = source.Mobile;
            target.Original = source.Original;
            target.PostalCode = source.PostalCode;
            target.RecNo = source.RecNo;
            target.RefCity = source.RefCity;
            target.RefCustomer = source.RefCustomer;
            target.RefPath = source.RefPath;
            target.RefProvince = source.RefProvince;
            target.RNContry = source.RNContry;
            target.Tableau = source.Tableau;
            target.Tel = source.Tel;
            target.Transport = source.Transport;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblInfoCustomer ToEntity(this TblInfoCustomerDto source)
        {
            if (source == null)
              return null;

            var target = new TblInfoCustomer();

            // Properties
            target.Address = source.Address;
            target.BirthDate = source.BirthDate;
            target.BirthPlace = source.BirthPlace;
            target.BranchCode = source.BranchCode;
            target.BranchName = source.BranchName;
            target.Code = source.Code;
            target.Company = source.Company;
            target.Email = source.Email;
            target.ExportDate = source.ExportDate;
            target.ExportPlace = source.ExportPlace;
            target.FatherName = source.FatherName;
            target.Fax = source.Fax;
            target.ID = source.ID;
            target.InfoCustomerId = source.InfoCustomerId;
            target.IsActive = source.IsActive;
            target.MailBox = source.MailBox;
            target.Mobile = source.Mobile;
            target.Original = source.Original;
            target.PostalCode = source.PostalCode;
            target.RecNo = source.RecNo;
            target.RefCity = source.RefCity;
            target.RefCustomer = source.RefCustomer;
            target.RefPath = source.RefPath;
            target.RefProvince = source.RefProvince;
            target.RNContry = source.RNContry;
            target.Tableau = source.Tableau;
            target.Tel = source.Tel;
            target.Transport = source.Transport;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblInfoCustomerDto> ToDtos(this IEnumerable<TblInfoCustomer> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblInfoCustomerDto> ToDtosWithRelated(this IEnumerable<TblInfoCustomer> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblInfoCustomer> ToEntities(this IEnumerable<TblInfoCustomerDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblInfoCustomer source, TblInfoCustomerDto target);

        static partial void OnEntityCreating(TblInfoCustomerDto source, TblInfoCustomer target);

    }

}
