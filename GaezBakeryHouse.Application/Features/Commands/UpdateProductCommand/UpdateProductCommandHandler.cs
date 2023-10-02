using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Entities;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Commands.UpdateProductCommand
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository repository, 
                                           IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            byte[] productImage;

            var productToUpdate = await _repository.GetByIdAsync(request.Id);
            _mapper.Map(request, productToUpdate, typeof(UpdateProductCommand), typeof(Product));

            using (var memoryStream = new MemoryStream())
            {
                await request.ProductImage.CopyToAsync(memoryStream);
                productImage = memoryStream.ToArray();
            }

            productToUpdate.ProductImage = productImage;
            await _repository.UpdateAsync(productToUpdate);
        }
    }
}
