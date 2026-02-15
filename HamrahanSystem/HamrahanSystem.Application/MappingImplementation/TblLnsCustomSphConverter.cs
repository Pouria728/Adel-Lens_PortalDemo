using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{
    public static partial class TblLnsCustomSphConverter
    {
        public static TblLnsCustomSphDto ToDto(this TblLnsCustomSph source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new TblLnsCustomSphDto
            {
                CustomSphId = source.CustomSphId,
                Name = source.Name,
                Code = source.Code,
                Description = source.Description,
                OrderId = source.OrderId,
                IsActive = Convert.ToBoolean(source.IsActive)
            };

            OnDtoCreating(source, target);
            return target;
        }

        public static TblLnsCustomSph ToEntity(this TblLnsCustomSphDto source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new TblLnsCustomSph
            {
                CustomSphId = source.CustomSphId,
                Name = source.Name,
                Code = source.Code,
                Description = source.Description,
                OrderId = source.OrderId,
                IsActive = Convert.ToInt16(source.IsActive)
            };

            OnEntityCreating(source, target);
            return target;
        }

        public static List<TblLnsCustomSphDto> ToDtos(this IEnumerable<TblLnsCustomSph> source)
        {
            return source?.Select(src => src.ToDto()).ToList();
        }

        public static List<TblLnsCustomSph> ToEntities(this IEnumerable<TblLnsCustomSphDto> source)
        {
            return source?.Select(src => src.ToEntity()).ToList();
        }

        static partial void OnDtoCreating(TblLnsCustomSph source, TblLnsCustomSphDto target);
        static partial void OnEntityCreating(TblLnsCustomSphDto source, TblLnsCustomSph target);
    }
}
