
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsOrderRepository : EntityFrameworkRepository<TblLnsOrder>, ITblLnsOrderRepository
    {
        public TblLnsOrderRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsOrder> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<TblLnsOrder>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
			if (!string.IsNullOrEmpty(sidx))
			{
				var colsort = (new TblLnsOrder()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
		public virtual Task<(List<TblLnsOrder>, int)> GetAllByFilter(int? OrderStatusId, int? IndexDocument, int? CustomerId, int? UserId, int? maxResult, int? page, int? rowInPage, string sort, string sidx, string search = "", string? fromDate = null, string? toDate = null)
		{
			var baseQuery = objectSet.Where(x => (OrderStatusId.HasValue == false || x.StatusId == OrderStatusId) && (IndexDocument.HasValue == false || x.IndexDocument == IndexDocument)
			&& (CustomerId.HasValue == false || x.DefineCustomerId == CustomerId) && (UserId.HasValue == false || x.CreatedBy == UserId) && (string.IsNullOrWhiteSpace(search) == true || x.FactorNo.ToLower().Contains(search.ToLower())));

			var fromDateValue = ParseFilterDate(fromDate);
			var toDateValue = ParseFilterDate(toDate);

			IEnumerable<TblLnsOrder> filtered = baseQuery;
			if (fromDateValue.HasValue || toDateValue.HasValue)
			{
				filtered = baseQuery.AsEnumerable()
					.Where(x => TryParseOrderDate(x.CreateDate, out var createDate)
						&& (!fromDateValue.HasValue || createDate.Date >= fromDateValue.Value.Date)
						&& (!toDateValue.HasValue || createDate.Date <= toDateValue.Value.Date))
					.ToList();
			}

			var countList = filtered.Count();
			var item = ApplySort(filtered.AsQueryable(), sort, sidx);
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

		private static IQueryable<TblLnsOrder> ApplySort(IQueryable<TblLnsOrder> query, string sort, string sidx)
		{
			if (string.IsNullOrWhiteSpace(sidx))
			{
				return query.OrderByDescending(x => x.CreateDate);
			}

			var sortAsc = string.Equals(sort, "asc", StringComparison.OrdinalIgnoreCase);
			switch (sidx.Trim().ToLowerInvariant())
			{
				case "id":
				case "orderid":
					return sortAsc ? query.OrderBy(x => x.OrderId) : query.OrderByDescending(x => x.OrderId);
				case "customer":
					return sortAsc ? query.OrderBy(x => x.DefineCustomerId) : query.OrderByDescending(x => x.DefineCustomerId);
				case "factorno":
				case "factorno1":
					return sortAsc ? query.OrderBy(x => x.FactorNo) : query.OrderByDescending(x => x.FactorNo);
				case "statusorder":
				case "statusid":
					return sortAsc ? query.OrderBy(x => x.StatusId) : query.OrderByDescending(x => x.StatusId);
				case "createdby":
				case "createdbyuser":
					return sortAsc ? query.OrderBy(x => x.CreatedBy) : query.OrderByDescending(x => x.CreatedBy);
				case "indexdocuments":
				case "indexdocument":
					return sortAsc ? query.OrderBy(x => x.IndexDocument) : query.OrderByDescending(x => x.IndexDocument);
				case "createdate":
					return sortAsc ? query.OrderBy(x => x.CreateDate) : query.OrderByDescending(x => x.CreateDate);
				default:
					return query.OrderByDescending(x => x.CreateDate);
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

		public virtual TblLnsOrder GetByKey(long _OrderId)
        {
            return objectSet.Include(x=>x.TblLnsOrderItems).SingleOrDefault(e => e.OrderId == _OrderId);
        }
		public Task Update(TblLnsOrder tblLnsOrder)
		{
			var item = objectSet.Single(x => x.OrderId == tblLnsOrder.OrderId);
			item.StatusId = tblLnsOrder.StatusId;
			if (!string.IsNullOrWhiteSpace(tblLnsOrder.FactorNo))
			{
				item.FactorNo = tblLnsOrder.FactorNo;
			}
			
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.BrandId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsOrder> tblLnsOrders)
		{
			objectSet.RemoveRange(tblLnsOrders);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsOrder> tblLnsOrders)
		{
			throw new NotImplementedException();
		}

		public Task<TblLnsOrder> Add(TblLnsOrder tblLnsOrder)
		{

			objectSet.Add(tblLnsOrder);
			Save();
			return Task.FromResult(tblLnsOrder);

		}

		public Task Add(List<TblLnsOrder> tblLnsOrder)
		{
			objectSet.AddRange(tblLnsOrder);
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
