using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product : BaseEntity {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }
    [Required, MaxLength(255)]
    public string Name { get; set; }
    [Required, Column(TypeName = "decimal(18,3)")]
    public decimal Price { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }

}
