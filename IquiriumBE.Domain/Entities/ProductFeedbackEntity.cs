using IquiriumBE.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("product_feedbacks")]
public class ProductFeedbackEntity : BaseEntity
{

    [ForeignKey("Product")]
    [Column("product_id")]
    [Required]
    public Guid ProductId { get; set; }
    public ProductEntity? Product { get; set; }

    [ForeignKey("User")]
    [Column("user_id")]
    [Required]
    public string UserId { get; set; }
    public AccountEntity? User { get; set; }

    [Column("rating")]
    [Range(1, 5)]
    [Required]
    public int Rating { get; set; }

    [Column("feedback", TypeName = "text")]
    [MaxLength(1000)]
    public string? Feedback { get; set; }

    public ProductFeedbackEntity(Guid productId, string userId, int rating, string? feedback)
    {
        ProductId = productId;
        UserId = userId;
        Rating = rating;
        Feedback = feedback;
    }

    public ProductFeedbackEntity() { }

}
