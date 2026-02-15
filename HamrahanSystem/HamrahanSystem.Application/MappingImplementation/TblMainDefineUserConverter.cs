

using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblMainDefineUserConverter
    {

        public static TblMainDefineUserDto ToDto(this TblMainDefineUser source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblMainDefineUserDto ToDtoWithRelated(this TblMainDefineUser source, int level)
        {
            if (source == null)
              return null;

            var target = new TblMainDefineUserDto();

            // Properties
            target.Active = source.Active;
            target.CodePersona = source.CodePersona;
            target.Company = source.Company;
            target.Id = source.Id;
            target.IsDisableTriggers = source.IsDisableTriggers;
            target.PassWord = source.PassWord;
            target.PassWordCheck = source.PassWordCheck;
            target.PictureNo = source.PictureNo;
            target.RNPersona = source.RNPersona;
            target.SQLPassword = source.SQLPassword;
            target.SQLUserName = source.SQLUserName;
            target.UserID = source.UserID;
            target.UserName = source.UserName;
            target.WebPassword = source.WebPassword;
            target.WebServer = source.WebServer;
            target.WebUserName = source.WebUserName;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblMainDefineUser ToEntity(this TblMainDefineUserDto source)
        {
            if (source == null)
              return null;

            var target = new TblMainDefineUser();

            // Properties
            target.Active = source.Active;
            target.CodePersona = source.CodePersona;
            target.Company = source.Company;
            target.Id = source.Id;
            target.IsDisableTriggers = source.IsDisableTriggers;
            target.PassWord = source.PassWord;
            target.PassWordCheck = source.PassWordCheck;
            target.PictureNo = source.PictureNo;
            target.RNPersona = source.RNPersona;
            target.SQLPassword = source.SQLPassword;
            target.SQLUserName = source.SQLUserName;
            target.UserID = source.UserID;
            target.UserName = source.UserName;
            target.WebPassword = source.WebPassword;
            target.WebServer = source.WebServer;
            target.WebUserName = source.WebUserName;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblMainDefineUserDto> ToDtos(this IEnumerable<TblMainDefineUser> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblMainDefineUserDto> ToDtosWithRelated(this IEnumerable<TblMainDefineUser> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblMainDefineUser> ToEntities(this IEnumerable<TblMainDefineUserDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblMainDefineUser source, TblMainDefineUserDto target);

        static partial void OnEntityCreating(TblMainDefineUserDto source, TblMainDefineUser target);

    }

}
