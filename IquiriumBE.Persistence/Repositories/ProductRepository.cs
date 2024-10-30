using IquiriumBE.Infrastructure.Abstractions;
using IquiriumBE.Infrastructure.Interfaces.Repositories;


namespace IquiriumBE.Persistence.Repositories
{
    public class ProductRepository : Repository<ProductEntity>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
