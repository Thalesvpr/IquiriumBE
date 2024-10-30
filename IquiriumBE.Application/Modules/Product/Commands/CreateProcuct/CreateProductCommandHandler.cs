
using IquiriumBE.Domain.Interfaces;
using MediatR;

namespace IquiriumBE.Application.Modules.Product.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(IUnitOfWork _unitOfWork) {
            this._unitOfWork = _unitOfWork;
        }


        public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await _unitOfWork.BeginTransactionAsync();

                var newProduct = new ProductEntity(
                    title: command.Title,
                    description: command.Description,
                    price: command.Price,
                    status: command.Status
                );

                cancellationToken.ThrowIfCancellationRequested();

                await _unitOfWork.Product.AddAsync(newProduct);

                await _unitOfWork.CommitAsync();

                return newProduct.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao criar o produto.", ex);
            }
        }
    }
}
