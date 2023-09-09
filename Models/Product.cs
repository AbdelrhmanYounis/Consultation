using System.ComponentModel.DataAnnotations;

namespace Consultation.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Counsel Title")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Please enter Counsel Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter Counsel Price")]
        [Display(Name = "Price")]
        public double ProductPrice { get; set; } //price for you

        [Required]
        public int PriceId { get; set; } // price from you
        public ProductPrice Price { get; set; }
        [Required]
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public string order { get; set; } //come from paymob
        public bool IsAvailable { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
