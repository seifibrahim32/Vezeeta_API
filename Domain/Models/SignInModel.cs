using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Please enter your email")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50), MinLength(8)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
            ErrorMessage = "Please enter email correctly.")]

        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [MaxLength(50), MinLength(6)]
        public string? Password { get; set; }
         
    }
}
