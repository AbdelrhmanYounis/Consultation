using System.ComponentModel.DataAnnotations;

namespace Consultation.Models
{
    public class ProductConfirmation
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string order { get; set; } //come from paymob
        public bool Expired { get; set; }

    }
}
