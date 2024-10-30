using IquiriumBE.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("products")]
public class ProductEntity : BaseEntity
{
    [Column("title")]
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [Column("description", TypeName = "text")]
    public string Description { get; set; }

    [Column("price", TypeName = "decimal(18, 2)")]
    [Required]
    public decimal Price { get; set; }

    [Column("status")]
    [MaxLength(50)]
    public string Status { get; set; } = "Available";

    public ProductEntity(string title, string description, decimal price, string status = "Available")
    {
        Title = title;
        Description = description;
        Price = price;
        Status = status;
    }
    public ICollection<ProductFeedbackEntity> Feedbacks { get; set; } = new List<ProductFeedbackEntity>();

    public ProductEntity() {}
}
