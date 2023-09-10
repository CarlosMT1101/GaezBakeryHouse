using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Domain.Entities;
using MediatR;


namespace GaezBakeryHouse.Application.Features.Queries.GetAllCategories
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDTO>>
    {
        readonly ICategoryRepository _repository;
        readonly IMapper _mapper;

        public GetAllCategoriesHandler(ICategoryRepository repository, 
                                       IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAll();

            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);
        }
    }
}
