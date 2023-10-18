using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Entities;

namespace GaezBakeryHouse.Infrastructure.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
