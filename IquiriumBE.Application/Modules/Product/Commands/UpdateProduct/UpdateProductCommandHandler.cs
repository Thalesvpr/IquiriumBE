using IquiriumBE.Domain.Interfaces;
using MediatR;

namespace IquiriumBE.Application.Modules.Product.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
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

                // Atualiza as propriedades do produto
                existingProduct.Title = command.Title;
                existingProduct.Description = command.Description;
                existingProduct.Price = command.Price;
                existingProduct.Status = command.Status;

                cancellationToken.ThrowIfCancellationRequested();

                // Salva as alterações no repositório
                await _unitOfWork.Product.UpdateAsync(existingProduct);

                await _unitOfWork.CommitAsync();

                return existingProduct.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao atualizar o produto.", ex);
            }
        }
    }
}
