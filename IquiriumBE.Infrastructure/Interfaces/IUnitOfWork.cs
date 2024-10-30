using IquiriumBE.Infrastructure.Interfaces.Repositories;
using System.Threading.Tasks;

namespace IquiriumBE.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepository Product { get; }
        public IProductFeedbackRepository ProductFeedback { get; }

        Task BeginTransactionAsync();
        Task<int> CommitAsync();
        Task RollbackAsync();
    }
}
