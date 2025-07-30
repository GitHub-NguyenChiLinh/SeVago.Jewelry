using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Customer : BaseEntity {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }
    [Required, MaxLength(100)]
    public string FullName { get; set; }
    [Required, MaxLength(100)]
    public string Email { get; set; }
    [Required, MaxLength(20)]
    public string PhoneNumber { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}
