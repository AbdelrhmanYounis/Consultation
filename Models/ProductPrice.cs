using System.ComponentModel.DataAnnotations;

namespace Consultation.Models
{
    public class ProductPrice
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public double Price { get; set; }

    }
}
