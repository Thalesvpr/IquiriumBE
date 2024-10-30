using IquiriumBE.Domain.Interfaces;
using IquiriumBE.Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IquiriumBe.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private IDbContextTransaction _currentTransaction;
        private bool _disposed = false;
        public IProductRepository Product { get; }
        public IProductFeedbackRepository ProductFeedback { get; }



        public UnitOfWork(
            ApplicationDbContext context,
            IProductRepository productRepository,
            IProductFeedbackRepository productFeedbackRepository
            
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this.Product = productRepository ?? throw new ArgumentNullException();
            this.ProductFeedback = productFeedbackRepository ?? throw new ArgumentNullException();

        }

        // Iniciar uma transação
        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction == null)
            {
                _currentTransaction = await _context.Database.BeginTransactionAsync();
            }
        }

        // Confirmar as mudanças
        public async Task<int> CommitAsync()
        {
            try
            {
                int result = await _context.SaveChangesAsync();
                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync();
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
                return result;
            }
            catch (Exception ex)
            {
                await RollbackAsync();
                throw new Exception("Ocorreu um erro ao salvar as mudanças.", ex);
            }
        }

        // Reverter as mudanças
        public async Task RollbackAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }

            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
