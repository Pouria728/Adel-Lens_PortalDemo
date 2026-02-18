
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;
using System.Threading.Tasks;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwResultStepRepository : IRepository<TblWfwResultStep>
    {
        ICollection<TblWfwResultStep> GetAll();
        TblWfwResultStep GetByKey(int _ResultStepId);
        ICollection<TblWfwResultStep> GetByRelationStepIds(IEnumerable<int> relationStepIds);
        Task ReplaceForRelations(IEnumerable<int> relationStepIdsToClear, IDictionary<int, IEnumerable<string>> optionsByRelationStepId, int? userId);
    }
}
