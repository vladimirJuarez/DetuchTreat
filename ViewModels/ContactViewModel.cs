using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage="The Min Length is 5")]
        [MinLength(5)]
        public string Name { get; set; } 
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Subject { get; set; }

        [MaxLength(10, ErrorMessage = "Too long")]
        public string Message { get; set; }    
    }
}
