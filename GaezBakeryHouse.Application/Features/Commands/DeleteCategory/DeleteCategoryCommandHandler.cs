﻿using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        readonly IMapper _mapper;
        readonly ICategoryRepository _repository;

        public DeleteCategoryCommandHandler(IMapper mapper,
                                            ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToDelete = await _repository.GetByIdAsync(request.Id);

            await _repository.DeleteAsync(categoryToDelete);
        }
    }
}
