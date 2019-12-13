using System;
using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public Decimal UnitPrice { get; set; }
        [Required]
        public int ProductId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSize { get; set; }
        public Decimal ProductPrice { get; set; }
        public string ProductTitle { get; set; }
        public string ProductArtDescription { get; set; }
        public string ProductArtDating { get; set; }
        public string ProductArtId { get; set; }
        public string ProductArtist { get; set; }
        public DateTime ProductArtistBirthDate { get; set; }
        public DateTime ProductArtistDeathDate { get; set; }
        public string ProductArtistNationality { get; set; }
    }
}