using MediatR;

namespace IquiriumBE.Application.Modules.Product.Queries.GetProducts
{

        public sealed record GetProductsQuery() : IRequest<IEnumerable<ProductEntity>>;



}
