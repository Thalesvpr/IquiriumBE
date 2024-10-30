namespace IquiriumBE.Api.Config
{
    using IquiriumBE.Infrastructure.Data.Seeds;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedIdentityData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                await IdentityDataInitializer.SeedRoles(services);
            }
        }
    }

}
