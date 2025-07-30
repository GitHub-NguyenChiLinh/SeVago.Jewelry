using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BaseEntity {
    [Required, Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Required, Column(TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [DefaultValue(false)]
    public bool IsDeleted { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set;}
}
