
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using System.Threading.Tasks;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwResultStepRepository : EntityFrameworkRepository<TblWfwResultStep>, ITblWfwResultStepRepository
    {
        public TblWfwResultStepRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwResultStep> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblWfwResultStep GetByKey(int _ResultStepId)
        {
            return objectSet.SingleOrDefault(e => e.ResultStepId == _ResultStepId);
        }

        public virtual ICollection<TblWfwResultStep> GetByRelationStepIds(IEnumerable<int> relationStepIds)
        {
            var relationIdSet = (relationStepIds ?? Enumerable.Empty<int>())
                .Where(x => x > 0)
                .Distinct()
                .ToHashSet();

            if (!relationIdSet.Any())
            {
                return new List<TblWfwResultStep>();
            }

            return objectSet
                .Where(x => x.RelationStepId.HasValue)
                .ToList()
                .Where(x => x.RelationStepId.HasValue && relationIdSet.Contains(x.RelationStepId.Value))
                .ToList();
        }

        public virtual Task ReplaceForRelations(IEnumerable<int> relationStepIdsToClear, IDictionary<int, IEnumerable<string>> optionsByRelationStepId, int? userId)
        {
            var cleanupRelationIds = (relationStepIdsToClear ?? Enumerable.Empty<int>())
                .Where(x => x > 0)
                .Distinct()
                .ToHashSet();

            if (cleanupRelationIds.Any())
            {
                var existing = objectSet
                    .Where(x => x.RelationStepId.HasValue)
                    .ToList()
                    .Where(x => x.RelationStepId.HasValue && cleanupRelationIds.Contains(x.RelationStepId.Value))
                    .ToList();

                if (existing.Any())
                {
                    objectSet.RemoveRange(existing);
                }
            }

            var optionMap = optionsByRelationStepId ?? new Dictionary<int, IEnumerable<string>>();
            foreach (var item in optionMap.Where(x => x.Key > 0))
            {
                var relationStepId = item.Key;
                var normalizedNames = (item.Value ?? Enumerable.Empty<string>())
                    .Select(x => x?.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                for (var i = 0; i < normalizedNames.Count; i++)
                {
                    var optionName = normalizedNames[i]!;
                    objectSet.Add(new TblWfwResultStep
                    {
                        RelationStepId = relationStepId,
                        Name = optionName.Length > 100 ? optionName[..100] : optionName,
                        Code = $"R{relationStepId:D4}-{i + 1:D2}",
                        IsActive = 1,
                        CommandType = 0,
                        CreatedBy = userId,
                        CreateDate = DateTime.Now.ToString("yyyy/MM/dd")
                    });
                }
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
