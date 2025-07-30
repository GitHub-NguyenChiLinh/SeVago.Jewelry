using System.ComponentModel.DataAnnotations;

namespace DTOs.Requests
{
    public class UpdateCustomerDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }
    }
} 