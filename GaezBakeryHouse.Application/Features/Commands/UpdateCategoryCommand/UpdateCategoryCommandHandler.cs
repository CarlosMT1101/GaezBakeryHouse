using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Entities;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Commands.UpdateCategoryCommand
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {

        readonly ICategoryRepository _repository;
        readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository repository,
                                            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            byte[] categoryImage;

            var categoryToUpdate = await _repository.GetByIdAsync(request.Id);
            _mapper.Map(request, categoryToUpdate, typeof(UpdateCategoryCommand), typeof(Category));

            using(var memoryStream = new MemoryStream())
            {
                await request.CategoryImage.CopyToAsync(memoryStream);
                categoryImage = memoryStream.ToArray();
            }

            categoryToUpdate.CategoryImage = categoryImage;
            await _repository.UpdateAsync(categoryToUpdate);
        }
    }
}
