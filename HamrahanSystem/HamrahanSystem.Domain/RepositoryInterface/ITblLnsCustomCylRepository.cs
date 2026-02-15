using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsCustomCylRepository : IRepository<TblLnsCustomCyl>
    {
        ICollection<TblLnsCustomCyl> GetAll();
        TblLnsCustomCyl GetByKey(int customCylId);
        Task<(List<TblLnsCustomCyl>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx);
        Task Delete(int id);
        Task Delete(List<TblLnsCustomCyl> items);
        Task Update(TblLnsCustomCyl item);
        Task Update(List<TblLnsCustomCyl> items);
        Task Add(TblLnsCustomCyl item);
        Task Add(List<TblLnsCustomCyl> items);
    }
}
