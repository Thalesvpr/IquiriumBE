using IquiriumBe.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("products")]
public class ProductEntity : BaseEntity
{
    [Column("title")]
    [Required]
    [MaxLength(255)]
    public required string Title { get; set; }

    [Column("description", TypeName = "text")]
    public required string Description { get; set; }

    [Column("price", TypeName = "decimal(18, 2)")]
    [Required]
    public decimal Price { get; set; }

    [Column("status")]
    [MaxLength(50)]
    public string Status { get; set; } = "Available";
}
