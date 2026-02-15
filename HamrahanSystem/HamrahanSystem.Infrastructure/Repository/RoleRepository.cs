
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class RoleRepository : EntityFrameworkRepository<Role>, IRoleRepository
    {
        public RoleRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<Role> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<Role>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new Role()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
                if (colsort != null)
                    item = sort == "asc" ? item.OrderBy(x => EF.Property<object>(x, colsort)) : item.OrderByDescending(x => EF.Property<object>(x, colsort));

            }
            if (page.HasValue && rowInPage.HasValue && countList > 0)
            {
                int totalPages = (int)Math.Ceiling((float)countList / (float)rowInPage);
                page = page.Value > totalPages ? totalPages : page.Value;
                int CountIndexs = ((page.Value - 1) * rowInPage.Value) + rowInPage.Value > countList ? countList - ((page.Value - 1) * rowInPage.Value) : rowInPage.Value;
                item = item.Skip((page.Value - 1) * rowInPage.Value).Take(CountIndexs);
            }
            if (maxResult.HasValue && maxResult > 0)
			{
				item = item.Take(maxResult.Value);
			}
		
        
            return Task.FromResult((item.ToList(), countList));

		}

		public virtual Role GetByKey(int _RoleId)
        {
            return objectSet.Include(x=>x.RolePermissions).SingleOrDefault(e => e.RoleId == _RoleId);
        }
		public Task Update(Role role)
		{
			var item = objectSet.Include(x=>x.RolePermissions).Single(x => x.RoleId == role.RoleId);
			item.RolePermissions.Clear();
			item.RoleName=role.RoleName;
			item.RolePermissions=role.RolePermissions;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.RoleId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<Role> roles)
		{
			objectSet.RemoveRange(roles);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<Role> roles)
		{
			throw new NotImplementedException();
		}

		public Task Add(Role role)
		{

			objectSet.Add(role);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<Role> roles)
		{
			objectSet.AddRange(roles);
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
