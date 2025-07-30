using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order : BaseEntity {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }
    [Required, ForeignKey("Customer")]
    public int CustomerId { get; set; }
    [Required, Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }
    [Required, Column(TypeName = "decimal(18,3)")]
    public decimal TotalAmount { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }

}
