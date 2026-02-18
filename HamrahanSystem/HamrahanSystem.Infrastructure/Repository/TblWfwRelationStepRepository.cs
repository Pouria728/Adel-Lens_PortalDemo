
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
            SyncForFromStep(fromProcessStepId, toProcessStepIds).Wait();
            return Task.CompletedTask;
        }

        public virtual Task<Dictionary<int, int>> SyncForFromStep(int fromProcessStepId, IEnumerable<int> toProcessStepIds)
        {
            var normalizedTargets = (toProcessStepIds ?? Enumerable.Empty<int>())
                .Where(x => x > 0)
                .Distinct()
                .ToList();
            var normalizedTargetSet = normalizedTargets.ToHashSet();
            var normalizedTargetList = normalizedTargets.ToList();

            var existingRelations = objectSet.Where(x => x.FromProcessStepId == fromProcessStepId).ToList();

            // Keep at most one relation per target step for this source step.
            foreach (var duplicate in existingRelations
                         .Where(x => x.ToProcessStepId.HasValue)
                         .GroupBy(x => x.ToProcessStepId!.Value)
                         .SelectMany(x => x.OrderBy(y => y.RelationStepId).Skip(1))
                         .ToList())
            {
                objectSet.Remove(duplicate);
            }

            var preservedRelations = existingRelations
                .Where(x => x.ToProcessStepId.HasValue)
                .GroupBy(x => x.ToProcessStepId!.Value)
                .Select(x => x.OrderBy(y => y.RelationStepId).First())
                .ToList();

            foreach (var relation in preservedRelations.Where(x => !normalizedTargetSet.Contains(x.ToProcessStepId!.Value)))
            {
                objectSet.Remove(relation);
            }

            var existingTargetIds = preservedRelations
                .Where(x => x.ToProcessStepId.HasValue && normalizedTargetSet.Contains(x.ToProcessStepId.Value))
                .Select(x => x.ToProcessStepId!.Value)
                .ToHashSet();

            foreach (var targetStepId in normalizedTargets.Where(x => !existingTargetIds.Contains(x)))
            {
                objectSet.Add(new TblWfwRelationStep
                {
                    FromProcessStepId = fromProcessStepId,
                    ToProcessStepId = targetStepId
                });
            }

            Save();

            // Materialize first to avoid SQL Server OPENJSON translation for runtime collections.
            var relationMap = objectSet
                .Where(x => x.FromProcessStepId == fromProcessStepId && x.ToProcessStepId.HasValue)
                .ToList()
                .Where(x => x.ToProcessStepId.HasValue && normalizedTargetList.Contains(x.ToProcessStepId.Value))
                .GroupBy(x => x.ToProcessStepId!.Value)
                .ToDictionary(
                    x => x.Key,
                    x => x.OrderBy(y => y.RelationStepId).First().RelationStepId);

            return Task.FromResult(relationMap);
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
