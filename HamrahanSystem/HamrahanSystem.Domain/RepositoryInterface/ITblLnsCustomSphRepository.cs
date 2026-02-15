using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsCustomSphRepository : IRepository<TblLnsCustomSph>
    {
        ICollection<TblLnsCustomSph> GetAll();
        TblLnsCustomSph GetByKey(int customSphId);
        Task<(List<TblLnsCustomSph>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx);
        Task Delete(int id);
        Task Delete(List<TblLnsCustomSph> items);
        Task Update(TblLnsCustomSph item);
        Task Update(List<TblLnsCustomSph> items);
        Task Add(TblLnsCustomSph item);
        Task Add(List<TblLnsCustomSph> items);
    }
}
