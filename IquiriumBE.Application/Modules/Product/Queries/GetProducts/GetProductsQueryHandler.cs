using IquiriumBE.Domain.Interfaces;
using MediatR;

namespace IquiriumBE.Application.Modules.Product.Queries.GetProducts
{
    internal sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductEntity>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Product.GetAllAsync();
            return products;
        }
    }
}
