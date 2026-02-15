
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using System.Threading.Tasks;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwRelationStepRepository : EntityFrameworkRepository<TblWfwRelationStep>, ITblWfwRelationStepRepository
    {
        public TblWfwRelationStepRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwRelationStep> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblWfwRelationStep GetByKey(int _RelationStepId)
        {
            return objectSet.SingleOrDefault(e => e.RelationStepId == _RelationStepId);
        }

        public virtual ICollection<TblWfwRelationStep> GetByFromProcessStepId(int fromProcessStepId)
        {
            return objectSet.Where(x => x.FromProcessStepId == fromProcessStepId).ToList();
        }

        public virtual Task ReplaceForFromStep(int fromProcessStepId, IEnumerable<int> toProcessStepIds)
        {
            var normalizedTargets = (toProcessStepIds ?? Enumerable.Empty<int>())
                .Where(x => x > 0)
                .Distinct()
                .ToList();

            var existingRelations = objectSet.Where(x => x.FromProcessStepId == fromProcessStepId).ToList();
            if (existingRelations.Count > 0)
            {
                objectSet.RemoveRange(existingRelations);
            }

            foreach (var targetStepId in normalizedTargets)
            {
                objectSet.Add(new TblWfwRelationStep
                {
                    FromProcessStepId = fromProcessStepId,
                    ToProcessStepId = targetStepId
                });
            }

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
