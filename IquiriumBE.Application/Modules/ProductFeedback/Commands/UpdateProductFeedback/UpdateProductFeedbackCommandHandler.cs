using IquiriumBE.Application.Modules.ProductFeedback.Commands.UpdateProductFeedback;
using IquiriumBE.Domain.Interfaces;
using MediatR;

namespace IquiriumBE.Application.Modules.ProductFeedback.Commands.UpdateProduct
{
    internal sealed class UpdateProductFeedbackCommandHandler : IRequestHandler<UpdateProductFeedbackCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductFeedbackCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateProductFeedbackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await _unitOfWork.BeginTransactionAsync();

                var existingProductFeedback = await _unitOfWork.ProductFeedback.GetByIdAsync(request.ProductFeedbackId);
                if (existingProductFeedback == null)
                {
                    throw new KeyNotFoundException($"O feedback do produto com ID {request.ProductFeedbackId} não foi encontrado.");
                }

                existingProductFeedback.Rating = request.Rating;
                existingProductFeedback.Feedback = request.Feedback;

                cancellationToken.ThrowIfCancellationRequested();

                await _unitOfWork.ProductFeedback.UpdateAsync(existingProductFeedback);

                await _unitOfWork.CommitAsync();

                return existingProductFeedback.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao atualizar o feedback do produto.", ex);
            }
        }
    }
}
