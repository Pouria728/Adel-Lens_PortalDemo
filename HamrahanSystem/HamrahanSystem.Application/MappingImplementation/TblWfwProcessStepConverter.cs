using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwProcessStepConverter
    {

        public static TblWfwProcessStepDto ToDto(this TblWfwProcessStep source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwProcessStepDto ToDtoWithRelated(this TblWfwProcessStep source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwProcessStepDto();

            // Properties
            target.AllowNextStep = source.AllowNextStep;
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToBoolean(source.IsActive);
            target.IsPrint = source.IsPrint;
            target.Leyout = source.Leyout;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.ProcessId = source.ProcessId;
            target.ProcessStepId = source.ProcessStepId;
			target.OrderId = source.OrderId;
			target.ProcedureId = source.ProcedureId;
			target.IsBarcode = source.IsBarcode;

			if (level > 0)
			{
				target.TblWfwRoleStepes = source.TblWfwRoleStepes.ToDtosWithRelated(level - 1);
				target.TblWfwProcess = source.TblWfwProcess.ToDtoWithRelated(level - 1);
				target.TblWfwOrderProcessSteps = source.TblWfwOrderProcessSteps.ToDtosWithRelated(level - 1);
			}
			// User-defined partial method
			OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwProcessStep ToEntity(this TblWfwProcessStepDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwProcessStep();

            // Properties
            target.AllowNextStep = source.AllowNextStep;
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToInt16(source.IsActive);
            target.IsPrint = source.IsPrint;
            target.Leyout = source.Leyout;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.ProcessId = source.ProcessId;
            target.ProcessStepId = source.ProcessStepId;
			target.OrderId = source.OrderId;
			target.ProcedureId = source.ProcedureId;
			target.IsBarcode = source.IsBarcode;

			// User-defined partial method
			OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwProcessStepDto> ToDtos(this IEnumerable<TblWfwProcessStep> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblWfwProcessStepDto> ToDtos(this IList<TblWfwProcessStep> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblWfwProcessStepDto> ToDtosWithRelated(this IEnumerable<TblWfwProcessStep> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<TblWfwProcessStepDto> ToDtosWithRelated(this IList<TblWfwProcessStep> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblWfwProcessStep> ToEntities(this IEnumerable<TblWfwProcessStepDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }
		public static List<TblWfwProcessStep> ToEntities(this IList<TblWfwProcessStepDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}

		static partial void OnDtoCreating(TblWfwProcessStep source, TblWfwProcessStepDto target);

        static partial void OnEntityCreating(TblWfwProcessStepDto source, TblWfwProcessStep target);

    }

}
