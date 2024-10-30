using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IquiriumBE.Domain.Entities;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;



public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<AccountEntity, IdentityRole, string>(options)
{
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductFeedbackEntity> ProductFeedbacks { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ProductEntity>()
            .HasMany(p => p.Feedbacks)
            .WithOne(f => f.Product)
            .HasForeignKey(f => f.ProductId);

        builder.Entity<ProductFeedbackEntity>()
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId);
    }
}
