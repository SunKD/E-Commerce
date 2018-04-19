using System.ComponentModel.DataAnnotations;
using System;

namespace E_Commerce.Models
{
    public class ProductViewModel : BaseEntity
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Product Name should be more than 2 characters")]
        [MinLength(2)]
        public string Name { get; set; }

        [Display(Name = "Image (url):")]
        [Required(ErrorMessage = "Image URL should be more than 10 characters")]
        [MinLength(10)]
        public string Image { get; set; }

        [Display(Name = "Description")]
        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Display(Name = "Initial Quantity")]
        [Required]
        public int Quantity { get; set; }
        
        [Display(Name = "Price")]
        [Required]
        public int Price { get; set; }
    }
}