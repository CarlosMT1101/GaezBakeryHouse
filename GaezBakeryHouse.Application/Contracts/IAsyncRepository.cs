using GaezBakeryHouse.Domain.Common;

namespace GaezBakeryHouse.Application.Contracts
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
