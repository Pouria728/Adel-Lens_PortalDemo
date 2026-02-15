using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

	public static partial class TblLnsSphCylConverter
	{

		public static TblLnsSphCylDto ToDto(this TblLnsSphCyl source)
		{
			return source.ToDtoWithRelated(0);
		}

		public static TblLnsSphCylDto ToDtoWithRelated(this TblLnsSphCyl source, int level)
		{
			if (source == null)
				return null;

			var target = new TblLnsSphCylDto();

			// Properties
			target.CylId = source.CylId;
			target.DefineObjectId = source.DefineObjectId;
			target.LensIndexRSphId = source.LensIndexRSphId;
			target.OrderId = source.OrderId;
			target.SphCylId = source.SphCylId;
			target.Stock = source.Stock;

			// Navigation Properties
			if (level > 0)
			{
				target.TblLnsOrderItems = source.TblLnsOrderItems.ToDtosWithRelated(level - 1);
				target.TblLnsCyl = source.TblLnsCyl.ToDtoWithRelated(level - 1);
				target.TblLnsLensIndexRSph = source.TblLnsLensIndexRSph.ToDtoWithRelated(level - 1);
				target.TblClrDefineObject = source.TblClrDefineObject.ToDtoWithRelated(level - 1);
			}

			// User-defined partial method
			OnDtoCreating(source, target);

			return target;
		}

		public static TblLnsSphCyl ToEntity(this TblLnsSphCylDto source)
		{
			if (source == null)
				return null;

			var target = new TblLnsSphCyl();

			// Properties
			target.CylId = source.CylId;
			target.DefineObjectId = source.DefineObjectId;
			target.LensIndexRSphId = source.LensIndexRSphId;
			target.OrderId = source.OrderId;
			target.SphCylId = source.SphCylId;
			target.Stock = source.Stock;

			// User-defined partial method
			OnEntityCreating(source, target);

			return target;
		}

		public static List<TblLnsSphCylDto> ToDtos(this IEnumerable<TblLnsSphCyl> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblLnsSphCylDto> ToDtosWithRelated(this IEnumerable<TblLnsSphCyl> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblLnsSphCyl> ToEntities(this IEnumerable<TblLnsSphCylDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}

		static partial void OnDtoCreating(TblLnsSphCyl source, TblLnsSphCylDto target);

		static partial void OnEntityCreating(TblLnsSphCylDto source, TblLnsSphCyl target);

	}

}
