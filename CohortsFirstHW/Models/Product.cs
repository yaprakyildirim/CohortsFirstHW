using System.ComponentModel.DataAnnotations;

namespace CohortsFirstHW.Models
{
    public class Product
    {
        //Product propertylerine bazı kısıtlar getirilmiştir
        [Required]//girilmesi zorunlu alan
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]//girilmesi zorunlu alan ve hata mesajı
        public string Name { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 10)] //description için en az 10 en fazla 50 karakter uzunluğu kısıtı
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}