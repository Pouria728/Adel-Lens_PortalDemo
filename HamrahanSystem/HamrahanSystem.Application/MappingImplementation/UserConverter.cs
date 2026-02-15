using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class UserConverter
    {

        public static UserDto ToDto(this User source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static UserDto ToDtoWithRelated(this User source, int level)
        {
            if (source == null)
              return null;

            var target = new UserDto();

            // Properties
            target.AllowForceSend = source.AllowForceSend;
            target.CompanyId = source.CompanyId;
            target.DisplayName = source.DisplayName;
            target.Email = source.Email;
            target.FirstName = source.FirstName;
            target.FiscalYear = source.FiscalYear;
            target.InfoCustomerId = source.InfoCustomerId;
            target.InsertDate = source.InsertDate;
            target.InsertUserId = source.InsertUserId;
            target.IsActive = Convert.ToBoolean(source.IsActive);
            target.LastDirectoryUpdate = source.LastDirectoryUpdate;
            target.LastName = source.LastName;
            target.Mobile = source.Mobile;
            target.MobilePhoneNumber = source.MobilePhoneNumber;
            target.MobilePhoneVerified = source.MobilePhoneVerified;
            target.NationalCode = source.NationalCode;
            target.PasswordHash = source.PasswordHash;
            target.PasswordSalt = source.PasswordSalt;
            target.Source = source.Source;
            target.TenantId = source.TenantId;
            target.TwoFactorAuth = source.TwoFactorAuth;
            target.UpdateDate = source.UpdateDate;
            target.UpdateUserId = source.UpdateUserId;
            target.UserId = source.UserId;
            target.UserImage = source.UserImage;
            target.Username = source.Username;
            target.WinId = source.WinId;
			target.IsAdmin = source.IsAdmin;

			// Navigation Properties
			if (level > 0) {
              target.UserPermissions = source.UserPermissions.ToDtosWithRelated(level - 1);
              target.UserRoles = source.UserRoles.ToDtosWithRelated(level - 1);
				target.TblWfwOrderProcessSteps = source.TblWfwOrderProcessSteps.ToDtosWithRelated(level - 1);
			}

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static User ToEntity(this UserDto source)
        {
            if (source == null)
              return null;

            var target = new User();

            // Properties
            target.AllowForceSend = source.AllowForceSend;
            target.CompanyId = source.CompanyId;
            target.DisplayName = source.DisplayName;
            target.Email = source.Email;
            target.FirstName = source.FirstName;
            target.FiscalYear = source.FiscalYear;
            target.InfoCustomerId = source.InfoCustomerId;
            target.InsertDate = source.InsertDate;
            target.InsertUserId = source.InsertUserId;
            target.IsActive =Convert.ToInt16(source.IsActive);
            target.LastDirectoryUpdate = source.LastDirectoryUpdate;
            target.LastName = source.LastName;
            target.Mobile = source.Mobile;
            target.MobilePhoneNumber = source.MobilePhoneNumber;
            target.MobilePhoneVerified = source.MobilePhoneVerified;
            target.NationalCode = source.NationalCode;
            target.PasswordHash = source.PasswordHash;
            target.PasswordSalt = source.PasswordSalt;
            target.Source = source.Source;
            target.TenantId = source.TenantId;
            target.TwoFactorAuth = source.TwoFactorAuth;
            target.UpdateDate = source.UpdateDate;
            target.UpdateUserId = source.UpdateUserId;
            target.UserId = source.UserId;
            target.UserImage = source.UserImage;
            target.Username = source.Username;
            target.WinId = source.WinId;
			target.IsAdmin = source.IsAdmin;

			// User-defined partial method
			OnEntityCreating(source, target);

            return target;
        }

        public static List<UserDto> ToDtos(this IEnumerable<User> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<UserDto> ToDtosWithRelated(this IEnumerable<User> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<User> ToEntities(this IEnumerable<UserDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(User source, UserDto target);

        static partial void OnEntityCreating(UserDto source, User target);

    }

}
