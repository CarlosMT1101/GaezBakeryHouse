using GaezBakeryHouse.Application.DTOs;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDTO>>
    {
    }
}
