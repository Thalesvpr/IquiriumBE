using IquiriumBE.Infrastructure.Abstractions;
using IquiriumBE.Infrastructure.Interfaces.Repositories;


namespace IquiriumBE.Persistence.Repositories
{
    public class ProductFeedbackRepository : Repository<ProductFeedbackEntity>, IProductFeedbackRepository
    {
        public ProductFeedbackRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
