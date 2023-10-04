using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Entities;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Commands.PostProduct
{
    public class PostProductCommandHandler : IRequestHandler<PostProductCommand>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public PostProductCommandHandler(IProductRepository repository,
                                         IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(PostProductCommand request, CancellationToken cancellationToken)
        {
            byte[] productImage;
            var product = _mapper.Map<PostProductCommand, Product>(request);

            using (var memoryStream = new MemoryStream())
            {
                await request.ProductImage.CopyToAsync(memoryStream);
                productImage = memoryStream.ToArray();
            }

            product.ProductImage = productImage;
            await _repository.PostAsync(product);
        }
    }
}
