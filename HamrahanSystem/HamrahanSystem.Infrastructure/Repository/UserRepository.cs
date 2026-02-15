
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class UserRepository : EntityFrameworkRepository<User>, IUserRepository
    {
        public UserRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<User> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<User>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new User()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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

		public virtual User GetByKey(int _UserId)
        {
            return objectSet.Include(x=>x.UserRoles).SingleOrDefault(e => e.UserId == _UserId);
        }
		public virtual List<User> GetByUserName(string userName)
		{
			return objectSet.Include(x => x.UserRoles).ThenInclude(x => x.Role).ThenInclude(x => x.RolePermissions).ThenInclude(x => x.Permission).Where(x=>x.Username==userName).ToList();
		}
		public Task Update(User user)
		{
			var item = objectSet.Include(x=>x.UserRoles).Single(x => x.UserId == user.UserId);
			item.UserRoles.Clear();
			item.FirstName = user.FirstName;
			item.LastName = user.LastName;
			item.Email = user.Email;
			item.MobilePhoneNumber = user.MobilePhoneNumber;
			item.UserRoles = user.UserRoles;
			item.Username = user.Username;
			item.PasswordHash = user.PasswordHash;
			item.PasswordSalt = user.PasswordSalt;
			item.IsActive = user.IsActive;
			item.IsAdmin = user.IsAdmin;
			item.InfoCustomerId = user.InfoCustomerId;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.UserId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<User> users)
		{
			objectSet.RemoveRange(users);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<User> users)
		{
			throw new NotImplementedException();
		}

		public Task Add(User user)
		{

			objectSet.Add(user);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<User> users)
		{
			objectSet.AddRange(users);
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
