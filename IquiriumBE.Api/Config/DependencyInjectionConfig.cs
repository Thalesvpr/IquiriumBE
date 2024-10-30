using IquiriumBE.Domain.Interfaces;
using IquiriumBe.Infrastructure;
using IquiriumBE.Persistence.Repositories;
using IquiriumBE.Infrastructure.Interfaces.Repositories;

namespace IquiriumBE.Infrastructure.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {

            //Repositorys
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductFeedbackRepository, ProductFeedbackRepository>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();


            //Cqrs



            return services;
        }
    }
}
