
namespace HamrahanSystem.Domain.Repository
{
    public partial interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
