using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Attributes
{
    public class ImageFileValidationAttribute : ValidationAttribute
    {
        const int Max_File_Size = 10 * 1024 * 1024; // 10MB
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file != default)
            {
                if (file.Length > Max_File_Size)
                    return new ValidationResult("This file's size is bigger than the 10MB limitation");
                if (file.ContentType.Contains("image"))
                    return ValidationResult.Success;
                return new ValidationResult("This is not a valid file ");
            }
            return new ValidationResult("Please enter a valid image file");
        }
    }
}
