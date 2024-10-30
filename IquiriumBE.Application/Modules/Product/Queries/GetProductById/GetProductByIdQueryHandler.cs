using IquiriumBE.Application.modules.Product.Queries.GetProduct;
using IquiriumBE.Domain.Interfaces;
using MediatR;

namespace IquiriumBE.Application.modules.Product.Queries.GetProductById
{
    internal sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductEntity> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {

            var product = await _unitOfWork.Product.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {request.ProductId} was not found.");
            }
            return product;

        }
    }
}
