using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{
    public static partial class TblLnsCustomCylConverter
    {
        public static TblLnsCustomCylDto ToDto(this TblLnsCustomCyl source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new TblLnsCustomCylDto
            {
                CustomCylId = source.CustomCylId,
                Name = source.Name,
                Code = source.Code,
                Description = source.Description,
                OrderId = source.OrderId,
                IsActive = Convert.ToBoolean(source.IsActive)
            };

            OnDtoCreating(source, target);
            return target;
        }

        public static TblLnsCustomCyl ToEntity(this TblLnsCustomCylDto source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new TblLnsCustomCyl
            {
                CustomCylId = source.CustomCylId,
                Name = source.Name,
                Code = source.Code,
                Description = source.Description,
                OrderId = source.OrderId,
                IsActive = Convert.ToInt16(source.IsActive)
            };

            OnEntityCreating(source, target);
            return target;
        }

        public static List<TblLnsCustomCylDto> ToDtos(this IEnumerable<TblLnsCustomCyl> source)
        {
            return source?.Select(src => src.ToDto()).ToList();
        }

        public static List<TblLnsCustomCyl> ToEntities(this IEnumerable<TblLnsCustomCylDto> source)
        {
            return source?.Select(src => src.ToEntity()).ToList();
        }

        static partial void OnDtoCreating(TblLnsCustomCyl source, TblLnsCustomCylDto target);
        static partial void OnEntityCreating(TblLnsCustomCylDto source, TblLnsCustomCyl target);
    }
}
