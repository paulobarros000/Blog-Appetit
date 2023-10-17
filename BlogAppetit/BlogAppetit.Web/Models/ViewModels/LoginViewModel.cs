using System.ComponentModel.DataAnnotations;

namespace BlogAppetit.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password tem de ter pelo menos 6 caracteres")]
        public string Password { get; set; } //um bocado tosco guardar a password em string, mas para este projeto é o suf
    }
}
