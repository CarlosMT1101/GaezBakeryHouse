using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Entities;
using MediatR;


namespace GaezBakeryHouse.Application.Features.Commands.PostCategory
{
    public class PostCategoryCommandHandler : IRequestHandler<PostCategoryCommand>
    {
        readonly ICategoryRepository _repository;
        readonly IMapper _mapper;

        public PostCategoryCommandHandler(ICategoryRepository repository, 
                                          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(PostCategoryCommand request, CancellationToken cancellationToken)
        {
            byte[] categoryImage;
            var category = _mapper.Map<PostCategoryCommand, Category>(request);

            using (var memoryStream = new MemoryStream())
            {
                await request.CategoryImage.CopyToAsync(memoryStream);
                categoryImage = memoryStream.ToArray();
            }

            category.CategoryImage = categoryImage;
            await _repository.PostAsync(category);
        }
    }
}
