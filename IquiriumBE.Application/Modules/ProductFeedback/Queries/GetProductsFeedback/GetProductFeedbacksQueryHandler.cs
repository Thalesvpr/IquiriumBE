using IquiriumBE.Domain.Interfaces;
using MediatR;

namespace IquiriumBE.Application.Modules.ProductFeedback.Queries.GetProductFeedbacks
{
    internal sealed class GetProductFeedbacksQueryHandler : IRequestHandler<GetProductFeedbacksQuery, IEnumerable<ProductFeedbackEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductFeedbacksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductFeedbackEntity>> Handle(GetProductFeedbacksQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductFeedback.GetAllAsync();
            return products;
        }
    }
}
