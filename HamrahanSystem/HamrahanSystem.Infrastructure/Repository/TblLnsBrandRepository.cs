
using Azure;
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsBrandRepository : EntityFrameworkRepository<TblLnsBrand>, ITblLnsBrandRepository
    {
        public TblLnsBrandRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsBrand> GetAllActive()
        {
            return objectSet.Where(x => x.IsActive == 1).OrderBy(x => x.OrderId).ToList();
        }
        public virtual ICollection<TblLnsBrand> GetAll()
        {
            return objectSet.OrderBy(x => x.OrderId).ToList();
        }
        public virtual Task<(List<TblLnsBrand>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
        {
            var countList = objectSet.Count();
            var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsBrand()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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

        public virtual TblLnsBrand GetByKey(int _BrandId)
        {
            return objectSet.SingleOrDefault(e => e.BrandId == _BrandId);
        }
        public Task Update(TblLnsBrand tblLnsBrand)
        {
            var item = objectSet.Single(x => x.BrandId == tblLnsBrand.BrandId);
            item.Name = tblLnsBrand.Name;
            item.IsActive = tblLnsBrand.IsActive;
            item.IsStock = tblLnsBrand.IsStock;
            item.IsSpecial = tblLnsBrand.IsSpecial;
            item.IsStockGranty = tblLnsBrand.IsStockGranty;
            item.Code = tblLnsBrand.Code;
            item.Description = tblLnsBrand.Description;
            Save();
            return Task.CompletedTask;
        }
        public Task Delete(int _ID)
        {
            objectSet.Where(e => e.BrandId == _ID).ExecuteDelete();
            return Task.CompletedTask;
        }

        public Task Delete(List<TblLnsBrand> TblLnsBrands)
        {
            objectSet.RemoveRange(TblLnsBrands);
            Save();
            return Task.CompletedTask;
        }


        public Task Update(List<TblLnsBrand> TblLnsBrands)
        {
            throw new NotImplementedException();
        }

        public Task Add(TblLnsBrand tblLnsBrand)
        {

            objectSet.Add(tblLnsBrand);
            Save();
            return Task.CompletedTask;

        }

        public Task Add(List<TblLnsBrand> tblLnsBrands)
        {
            objectSet.AddRange(tblLnsBrands);
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
