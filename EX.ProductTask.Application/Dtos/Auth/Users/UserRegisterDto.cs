using System.ComponentModel.DataAnnotations;
using Application.Dtos.Auth.Users;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Users
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "field-required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "field-required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "field-required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "field-required")]
        public string Password { get; set; }

        [Compare("Password" , ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPasword { get; set; }
		public List<UserRoleRegisterDto> UserRole { get; set; }

	}
}
