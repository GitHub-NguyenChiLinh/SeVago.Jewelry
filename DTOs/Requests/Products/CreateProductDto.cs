using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs.Requests.Products
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required, Column(TypeName = "decimal(18,3)")]
        public decimal Price { get; set; }
    }
}