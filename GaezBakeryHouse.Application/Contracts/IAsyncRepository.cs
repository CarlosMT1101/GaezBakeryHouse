using GaezBakeryHouse.Domain.Common;

namespace GaezBakeryHouse.Application.Contracts
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task PostAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
