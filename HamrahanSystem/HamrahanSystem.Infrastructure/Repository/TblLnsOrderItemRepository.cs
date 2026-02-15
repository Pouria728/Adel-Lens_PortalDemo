
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
namespace HamrahanSystem.Infrastructure.Repository
{
	public partial class TblLnsOrderItemRepository : EntityFrameworkRepository<TblLnsOrderItem>, ITblLnsOrderItemRepository
	{
		public TblLnsOrderItemRepository(AdelModel context)
			: base(context)
		{
		}

		public virtual ICollection<TblLnsOrderItem> GetAll()
		{
			return objectSet.ToList();
		}

		public virtual TblLnsOrderItem GetByKey(long _OrderItemId)
		{
			return objectSet.SingleOrDefault(e => e.OrderItemId == _OrderItemId);
		}

		public Task UpdateProvidedQuantity(List<TblLnsOrderItem> tblLnsOrderItems)
		{
			foreach (var itemdata in tblLnsOrderItems)
			{
				var item = objectSet.Single(x => x.OrderItemId == itemdata.OrderItemId);
				item.ProvidedQuantity = itemdata.ProvidedQuantity;
				Save();
			}
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
