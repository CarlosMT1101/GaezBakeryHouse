using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Domain.Entities;
using MediatR;


namespace GaezBakeryHouse.Application.Features.Queries.GetAllCategories
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IQueryable<CategoryDTO>>
    {
        readonly ICategoryRepository _repository;
        readonly IMapper _mapper;

        public GetAllCategoriesHandler(ICategoryRepository repository, 
                                       IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        Task<IQueryable<CategoryDTO>> IRequestHandler<GetAllCategoriesQuery, IQueryable<CategoryDTO>>.Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _repository.GetAll();
            var categoriesDTO = _mapper.ProjectTo<CategoryDTO>(categories);

            return Task.FromResult(categoriesDTO);
        }
    }
}
