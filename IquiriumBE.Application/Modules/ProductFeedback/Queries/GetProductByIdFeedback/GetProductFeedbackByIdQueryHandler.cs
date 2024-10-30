using IquiriumBE.Application.modules.ProductFeedback.Queries.GetProductFeedback;
using IquiriumBE.Domain.Interfaces;
using MediatR;

namespace IquiriumBE.Application.modules.ProductFeedback.Queries.GetProductFeedbackById
{
    internal sealed class GetProductFeedbackByIdQueryHandler : IRequestHandler<GetProductFeedbackByIdQuery, ProductFeedbackEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductFeedbackByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductFeedbackEntity> Handle(GetProductFeedbackByIdQuery request, CancellationToken cancellationToken)
        {
            var feedback = await _unitOfWork.ProductFeedback.GetByIdAsync(request.ProductFeedbackId);
            if (feedback == null)
            {
                throw new KeyNotFoundException($"Feedback do produto com ID {request.ProductFeedbackId} não foi encontrado.");
            }
            return feedback;
        }
    }
}
