using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderDetail : BaseEntity {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderDetailId { get; set; }
    [Required, ForeignKey("Order")]
    public int OrderId { get; set; }
    [Required, ForeignKey("Product")]
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    [Required, Column(TypeName = "decimal(18,3)")]
    public decimal UnitPrice { get; set; }
    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}
