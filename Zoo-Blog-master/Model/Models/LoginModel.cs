using System.ComponentModel.DataAnnotations;
namespace Model.Models
{
    public class LoginModel
    {
        [Display(Name ="User name")]
        [Required]
        [RegularExpression("[A-Za-z0-9]{2,25}$", ErrorMessage = "Enter valid name contain 2-25 letters only only")]
        public string? Username { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,20}$",
            ErrorMessage = "Must contain at least one lower case one upper case on digit and speacial charcters between  8 to 20 chars")]
        [MinLength(8)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
