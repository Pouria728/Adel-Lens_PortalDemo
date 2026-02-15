using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwProcessConverter
    {

        public static TblWfwProcessDto ToDto(this TblWfwProcess source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwProcessDto ToDtoWithRelated(this TblWfwProcess source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwProcessDto();

            // Properties
            target.Code = source.Code;
            target.CodeSystem = source.CodeSystem;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IndexDocument = source.IndexDocument;
            target.IsActive = Convert.ToBoolean( source.IsActive);
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.ProcessId = source.ProcessId;

			if (level > 0)
			{
				target.TblWfwProcessSteps = source.TblWfwProcessSteps.ToDtosWithRelated(level - 1);
				target.TblWfwOrderProcesses = source.TblWfwOrderProcesses.ToDtosWithRelated(level - 1);
			}
			// User-defined partial method
			OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwProcess ToEntity(this TblWfwProcessDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwProcess();

            // Properties
            target.Code = source.Code;
            target.CodeSystem = source.CodeSystem;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IndexDocument = source.IndexDocument;
            target.IsActive = Convert.ToInt16(source.IsActive);
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.ProcessId = source.ProcessId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwProcessDto> ToDtos(this IEnumerable<TblWfwProcess> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblWfwProcessDto> ToDtos(this IList<TblWfwProcess> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblWfwProcessDto> ToDtosWithRelated(this IEnumerable<TblWfwProcess> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<TblWfwProcessDto> ToDtosWithRelated(this IList<TblWfwProcess> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblWfwProcess> ToEntities(this IEnumerable<TblWfwProcessDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }
		public static List<TblWfwProcess> ToEntities(this IList<TblWfwProcessDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}

		static partial void OnDtoCreating(TblWfwProcess source, TblWfwProcessDto target);

        static partial void OnEntityCreating(TblWfwProcessDto source, TblWfwProcess target);

    }

}
