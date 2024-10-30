using IquiriumBE.Domain.Interfaces;
using MediatR;

namespace IquiriumBE.Application.Modules.Product.Commands.DeleteProduct
{
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await _unitOfWork.BeginTransactionAsync();

                // Busca o produto existente
                var existingProduct = await _unitOfWork.Product.GetByIdAsync(command.ProductId);
                if (existingProduct == null)
                {
                    throw new KeyNotFoundException($"Produto com ID {command.ProductId} não foi encontrado.");
                }

                cancellationToken.ThrowIfCancellationRequested();

                // Remove o produto do repositório
                await _unitOfWork.Product.DeleteAsync(existingProduct);


                await _unitOfWork.CommitAsync();

                return existingProduct.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao deletar o produto.", ex);
            }
        }
    }
}
