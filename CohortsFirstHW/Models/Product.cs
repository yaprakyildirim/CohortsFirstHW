using System.ComponentModel.DataAnnotations;

namespace CohortsFirstHW.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}