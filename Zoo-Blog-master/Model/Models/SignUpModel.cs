using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class SignUpModel
    {

        [Required]
        [Display(Name = "User name")]
        [RegularExpression("[A-Za-z0-9]{2,25}$", ErrorMessage = "Enter valid name contain 2-25 letters only only")]
        public string? Username { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,20}$",
            ErrorMessage = "Must contain at least one lower case one upper case on digit and speacial charcters between  8 to 20 chars")]
        [MinLength(8)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,20}$",
            ErrorMessage ="Must contain at least one lower case one upper case on digit and speacial charcters between  8 to 20 chars")]
        [MinLength(8)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Compare("Password",
            ErrorMessage = "The two passwords must mach")]
        public string? ConiformPassword { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
