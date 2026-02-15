
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsOrderItemRepository : IRepository<TblLnsOrderItem>
    {
        ICollection<TblLnsOrderItem> GetAll();
        TblLnsOrderItem GetByKey(long _OrderItemId);
        Task UpdateProvidedQuantity(List<TblLnsOrderItem> tblLnsOrderItems);

	}
}
