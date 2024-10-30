using IquiriumBE.Domain.Interfaces;
using MediatR;

namespace IquiriumBE.Application.Modules.ProductFeedback.Commands.DeleteProductFeedback
{
    internal sealed class DeleteProductFeedbackCommandHandler : IRequestHandler<DeleteProductFeedbackCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductFeedbackCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteProductFeedbackCommand request, CancellationToken cancellationToken)
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

                cancellationToken.ThrowIfCancellationRequested();

                await _unitOfWork.ProductFeedback.DeleteAsync(existingProductFeedback);

                await _unitOfWork.CommitAsync();

                return existingProductFeedback.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao deletar o feedback do produto.", ex);
            }
        }
    }
}
