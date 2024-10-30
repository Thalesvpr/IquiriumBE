using IquiriumBE.Application.Modules.Product.Commands.CreateProduct;
using IquiriumBE.Domain.Entities;
using IquiriumBE.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IquiriumBE.Api.Config;
using IquiriumBE.Infrastructure.Data.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Configuração de controladores e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configuração do DbContext com PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração de Injeção de Dependência
builder.Services.AddDependencyInjections();

// Configuração do MediatR para comandos e queries
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(CreateProductCommandHandler).Assembly
));

// Configuração do ASP.NET Identity com Entity Framework
builder.Services.AddIdentityCore<AccountEntity>()
.AddRoles<IdentityRole>().AddApiEndpoints()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Adiciona os endpoints do Identity de forma automática
builder.Services.AddIdentityApiEndpoints<AccountEntity>();

var app = builder.Build();

// Configura os endpoints da API
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<AccountEntity>();


// Executar Seed de Roles e Usuários
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentityDataInitializer.SeedRoles(services);
}


// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapSwagger();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
