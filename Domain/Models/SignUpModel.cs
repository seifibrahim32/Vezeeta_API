using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Please enter your email")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50), MinLength(8)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
            ErrorMessage = "Please enter email correctly.")]

        public string? Email { get; set; }

        [DataType(DataType.ImageUrl)]  
        [MinLength(8)]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Please enter your password")] 
        [MaxLength(50), MinLength(6)]
        public string? Password { get; set; }
         
        [Required(ErrorMessage = "Please enter your date of birth")] 
        public string? DateOfBirth { get; set; }
         
        [Required(ErrorMessage = "Please enter your gender")]  
        public int Gender { get; set; }

    }
}
