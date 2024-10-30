using IquiriumBE.Domain.Interfaces;
using MediatR;

namespace IquiriumBE.Application.Modules.ProductFeedback.Commands.CreateProductFeedback
{
    public sealed class CreateProductFeedbackCommandHandler : IRequestHandler<CreateProductFeedbackCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductFeedbackCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProductFeedbackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await _unitOfWork.BeginTransactionAsync();

                var existingProduct = await _unitOfWork.Product.GetByIdAsync(request.ProductId);
                if (existingProduct == null)
                {
                    throw new KeyNotFoundException($"Produto com ID {request.ProductId} não foi encontrado.");
                }

                var newProductFeedback = new ProductFeedbackEntity(
                    productId: request.ProductId,
                    userId: request.UserId,
                    rating: request.Rating,
                    feedback: request.Feedback
                );

                cancellationToken.ThrowIfCancellationRequested();

                await _unitOfWork.ProductFeedback.AddAsync(newProductFeedback);

                await _unitOfWork.CommitAsync();

                return newProductFeedback.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao criar o feedback do produto.", ex);
            }
        }
    }
}
