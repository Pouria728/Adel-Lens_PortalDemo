
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwOrderProcessStepRepository : EntityFrameworkRepository<TblWfwOrderProcessStep>, ITblWfwOrderProcessStepRepository
    {
        public TblWfwOrderProcessStepRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwOrderProcessStep> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<TblWfwOrderProcessStep>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
			if (!string.IsNullOrEmpty(sidx))
			{
				var colsort = (new TblWfwOrderProcessStep()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
				if (colsort != null)
					item = sort == "asc" ? item.OrderBy(x => EF.Property<object>(x, colsort)) : item.OrderByDescending(x => EF.Property<object>(x, colsort));

			}
			if (maxResult.HasValue && maxResult > 0)
			{
				item = item.Take(maxResult.Value);
			}
			if (page.HasValue && rowInPage.HasValue && countList > 0)
			{
				int totalPages = (int)Math.Ceiling((float)countList / (float)rowInPage);
				page = page.Value > totalPages ? totalPages : page.Value;
				int CountIndexs = ((page.Value - 1) * rowInPage.Value) + rowInPage.Value > countList ? countList - ((page.Value - 1) * rowInPage.Value) : rowInPage.Value;
				item = item.Skip((page.Value - 1) * rowInPage.Value).Take(CountIndexs);
			}
		
			return Task.FromResult((item.ToList(), countList));

		}
		public virtual Task<(List<TblWfwOrderProcessStep>, int)> GetAllByFilter(int? RequestStatusId, int? OrderStatusId, int? IndexDocument, int? CustomerId, int? RoleId, int? CreatedById, int? maxResult, int? page, int? rowInPage, string sort, string sidx, string factorNo = "", string? fromDate = null, string? toDate = null)
		{
			var baseQuery = objectSet.Include(x => x.TblWfwOrderProcess.TblWfwProcess)
				.Include(x => x.TblWfwProcessStep).ThenInclude(x => x.TblWfwRoleStepes)
				.Include(x => x.TblWfwOrderProcess).ThenInclude(x => x.TblLnsOrder)
				.Where(x => (RequestStatusId.HasValue == false || x.StatusId == RequestStatusId)
				&& (OrderStatusId.HasValue == false || x.TblWfwOrderProcess.TblLnsOrder.StatusId == OrderStatusId)
				&& (IndexDocument.HasValue == false || x.TblWfwOrderProcess.TblLnsOrder.IndexDocument == IndexDocument)
				&& (CustomerId.HasValue == false || x.TblWfwOrderProcess.TblLnsOrder.DefineCustomerId == CustomerId)
				&& (CreatedById.HasValue == false || x.TblWfwOrderProcess.TblLnsOrder.CreatedBy == CreatedById)
				&& (RoleId.HasValue == false || x.TblWfwProcessStep.TblWfwRoleStepes.Any(y => y.RoleId == RoleId))
				&& (string.IsNullOrWhiteSpace(factorNo) == true || x.TblWfwOrderProcess.TblLnsOrder.FactorNo.ToLower().Contains(factorNo.ToLower())));

			var fromDateValue = ParseFilterDate(fromDate);
			var toDateValue = ParseFilterDate(toDate);
			if (fromDateValue.HasValue)
			{
				baseQuery = baseQuery.Where(x => x.DateCreate.Date >= fromDateValue.Value.Date);
			}
			if (toDateValue.HasValue)
			{
				baseQuery = baseQuery.Where(x => x.DateCreate.Date <= toDateValue.Value.Date);
			}

			var countList = baseQuery.Count();
			var item = ApplySort(baseQuery.AsQueryable(), sort, sidx);
			if (maxResult.HasValue && maxResult > 0)
			{
				item = item.Take(maxResult.Value);
			}
			if (page.HasValue && rowInPage.HasValue && countList > 0)
			{
				int totalPages = (int)Math.Ceiling((float)countList / (float)rowInPage);
				page = page.Value > totalPages ? totalPages : page.Value;
				int CountIndexs = ((page.Value - 1) * rowInPage.Value) + rowInPage.Value > countList ? countList - ((page.Value - 1) * rowInPage.Value) : rowInPage.Value;
				item = item.Skip((page.Value - 1) * rowInPage.Value).Take(CountIndexs);
			}
			return Task.FromResult((item.Include(x=>x.TblWfwProcessStep).ThenInclude(x=>x.TblWfwRoleStepes).Include(x=>x.TblWfwOrderProcess).ThenInclude(x=>x.TblLnsOrder).Include(x=>x.TblWfwOrderProcess.TblWfwProcess).ToList(), countList));


		}

		private static IQueryable<TblWfwOrderProcessStep> ApplySort(IQueryable<TblWfwOrderProcessStep> query, string sort, string sidx)
		{
			if (string.IsNullOrWhiteSpace(sidx))
			{
				return query.OrderByDescending(x => x.DateCreate);
			}

			var sortAsc = string.Equals(sort, "asc", StringComparison.OrdinalIgnoreCase);
			switch (sidx.Trim().ToLowerInvariant())
			{
				case "id":
				case "orderprocessstepid":
					return sortAsc ? query.OrderBy(x => x.OrderProcessStepId) : query.OrderByDescending(x => x.OrderProcessStepId);
				case "customer":
					return sortAsc
						? query.OrderBy(x => x.TblWfwOrderProcess.TblLnsOrder.DefineCustomerId)
						: query.OrderByDescending(x => x.TblWfwOrderProcess.TblLnsOrder.DefineCustomerId);
				case "factorno":
				case "factorno1":
					return sortAsc
						? query.OrderBy(x => x.TblWfwOrderProcess.TblLnsOrder.FactorNo)
						: query.OrderByDescending(x => x.TblWfwOrderProcess.TblLnsOrder.FactorNo);
				case "indexdocuments":
				case "indexdocument":
					return sortAsc
						? query.OrderBy(x => x.TblWfwOrderProcess.TblLnsOrder.IndexDocument)
						: query.OrderByDescending(x => x.TblWfwOrderProcess.TblLnsOrder.IndexDocument);
				case "stepname":
					return sortAsc ? query.OrderBy(x => x.TblWfwProcessStep.Name) : query.OrderByDescending(x => x.TblWfwProcessStep.Name);
				case "stepcode":
					return sortAsc ? query.OrderBy(x => x.TblWfwProcessStep.Code) : query.OrderByDescending(x => x.TblWfwProcessStep.Code);
				case "statusorder":
					return sortAsc
						? query.OrderBy(x => x.TblWfwOrderProcess.TblLnsOrder.StatusId)
						: query.OrderByDescending(x => x.TblWfwOrderProcess.TblLnsOrder.StatusId);
				case "statusrequest":
					return sortAsc ? query.OrderBy(x => x.StatusId) : query.OrderByDescending(x => x.StatusId);
				case "createdby":
				case "createdbyuser":
					return sortAsc
						? query.OrderBy(x => x.TblWfwOrderProcess.TblLnsOrder.CreatedBy)
						: query.OrderByDescending(x => x.TblWfwOrderProcess.TblLnsOrder.CreatedBy);
				case "datecreate":
					return sortAsc ? query.OrderBy(x => x.DateCreate) : query.OrderByDescending(x => x.DateCreate);
				default:
					return query.OrderByDescending(x => x.DateCreate);
			}
		}

		private static DateTime? ParseFilterDate(string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return null;
			}

			return TryParseOrderDate(value, out var result) ? result.Date : null;
		}

		private static bool TryParseOrderDate(string? value, out DateTime result)
		{
			result = default;
			if (string.IsNullOrWhiteSpace(value))
			{
				return false;
			}

			var normalized = ConvertToLatinDigits(value.Trim());
			var parts = normalized.Split(new[] { '/', '-', '.' }, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length >= 3 &&
				int.TryParse(parts[0], out var year) &&
				int.TryParse(parts[1], out var month) &&
				int.TryParse(parts[2], out var day))
			{
				if (year >= 1700)
				{
					try
					{
						result = new DateTime(year, month, day, new GregorianCalendar());
						return true;
					}
					catch
					{
					}
				}
				else if (year >= 1300 && year <= 1500)
				{
					try
					{
						result = new PersianCalendar().ToDateTime(year, month, day, 0, 0, 0, 0);
						return true;
					}
					catch
					{
					}
				}
			}

			return DateTime.TryParse(normalized, CultureInfo.InvariantCulture, DateTimeStyles.None, out result)
				|| DateTime.TryParse(normalized, CultureInfo.CurrentCulture, DateTimeStyles.None, out result);
		}

		private static string ConvertToLatinDigits(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}

			var chars = value.ToCharArray();
			for (var i = 0; i < chars.Length; i++)
			{
				var ch = chars[i];
				if (ch >= '۰' && ch <= '۹')
				{
					chars[i] = (char)('0' + (ch - '۰'));
				}
				else if (ch >= '٠' && ch <= '٩')
				{
					chars[i] = (char)('0' + (ch - '٠'));
				}
			}

			return new string(chars);
		}

		public virtual TblWfwOrderProcessStep GetByKey(long _OrderId)
        {
            return objectSet.Include(x => x.TblWfwProcessStep).ThenInclude(x => x.TblWfwRoleStepes).Include(x => x.TblWfwOrderProcess).ThenInclude(x => x.TblLnsOrder).ThenInclude(x=>x.TblLnsOrderItems).ThenInclude(x=>x.TblClrDefineObject).Include(x => x.TblWfwOrderProcess.TblWfwProcess).SingleOrDefault(e => e.OrderProcessStepId == _OrderId);
        }
		public Task Update(TblWfwOrderProcessStep TblWfwOrderProcessStep)
		{
			var item = objectSet.Single(x => x.OrderProcessStepId == TblWfwOrderProcessStep.OrderProcessStepId);
			item.UserId = TblWfwOrderProcessStep.UserId;
			item.StatusId = TblWfwOrderProcessStep.StatusId;
			item.DateComplete = TblWfwOrderProcessStep.DateComplete;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.OrderProcessStepId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblWfwOrderProcessStep> TblWfwOrderProcessSteps)
		{
			objectSet.RemoveRange(TblWfwOrderProcessSteps);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblWfwOrderProcessStep> TblWfwOrderProcessSteps)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblWfwOrderProcessStep TblWfwOrderProcessStep)
		{

			objectSet.Add(TblWfwOrderProcessStep);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblWfwOrderProcessStep> TblWfwOrderProcessStep)
		{
			objectSet.AddRange(TblWfwOrderProcessStep);
			Save();
			return Task.CompletedTask;
		}

		public new AdelModel Context 
        {
            get
            {
                return (AdelModel)base.Context;
            }
        }
    }
}
