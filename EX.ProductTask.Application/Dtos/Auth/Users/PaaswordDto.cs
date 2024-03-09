using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Users
{
    public class PaaswordDto
    {
        [Required(ErrorMessage = "field-required")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "passwordNotCorrect")]
        public string RePassword { get; set; }
    }
}