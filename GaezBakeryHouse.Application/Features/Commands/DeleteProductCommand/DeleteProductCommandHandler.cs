using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaezBakeryHouse.Application.Features.Commands.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        readonly IMapper _mapper;
        readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IMapper mapper, 
                                           IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await _repository.GetByIdAsync(request.Id);

            await _repository.DeleteAsync(productToDelete);
        }
    }
}
