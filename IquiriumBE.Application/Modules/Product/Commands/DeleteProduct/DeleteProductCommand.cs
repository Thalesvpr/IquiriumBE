using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace IquiriumBE.Application.Modules.Product.Commands.DeleteProduct
{
    public sealed class DeleteProductCommand : IRequest<Guid>
    {
        [Required(ErrorMessage = "O ID do produto é obrigatório.")]
        public Guid ProductId { get; set; }
    }
}
