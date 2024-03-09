using System;
using System.ComponentModel.DataAnnotations;
using Application.Dtos.Auth.Users;
using Core.Enums;
using Microsoft.AspNetCore.Http;


namespace Application.Dtos.Users
{
    public class UserEditDto
    {
        [Required(ErrorMessage = "field-required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "field-required")]
        [EmailAddress(ErrorMessage = "email-error")]
        public string Email { get; set; }
        public string Password { get; set; }

    }
}