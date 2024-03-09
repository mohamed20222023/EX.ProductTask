using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos.roles
{
    public class RoleRegisterDto
    {
        [Required(ErrorMessage = "field-required")]

        public string Name { get; set; }
        [Required(ErrorMessage = "field-required")]

        public string NameAr { get; set; }

        [Required(ErrorMessage = "field-required")]
        public string NameEn { get; set; }




    }
}