using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class OrderItemRequestDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}
