using MediatR;

namespace IquiriumBE.Application.Modules.ProductFeedback.Queries.GetProductFeedbacks
{

        public sealed record GetProductFeedbacksQuery() : IRequest<IEnumerable<ProductFeedbackEntity>>;



}
