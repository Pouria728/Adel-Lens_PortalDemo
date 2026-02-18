
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;
using System.Threading.Tasks;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwRelationStepRepository : IRepository<TblWfwRelationStep>
    {
        ICollection<TblWfwRelationStep> GetAll();
        TblWfwRelationStep GetByKey(int _RelationStepId);
        ICollection<TblWfwRelationStep> GetByFromProcessStepId(int fromProcessStepId);
        Task ReplaceForFromStep(int fromProcessStepId, IEnumerable<int> toProcessStepIds);
        Task<Dictionary<int, int>> SyncForFromStep(int fromProcessStepId, IEnumerable<int> toProcessStepIds);
    }
}
