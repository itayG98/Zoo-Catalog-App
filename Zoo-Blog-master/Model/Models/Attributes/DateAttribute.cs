using System.ComponentModel.DataAnnotations;

namespace Model.Models.Attributes
{
    public class DateValidationAttribute : ValidationAttribute
    {
        public const int MAX_LIFE_TIME = 150;
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime != default)
            {
                int Years = (int)DateTime.Now.Subtract(dateTime).TotalDays / 365;
                if (Years > MAX_LIFE_TIME)
                {
                    return new ValidationResult("This birth date is unvalid .Please specify a date afterwards");
                }
                else if (Years < 0)
                {
                    return new ValidationResult("This birth date is unvalid .Please specify an earlier date");
                }
                else if (dateTime.Subtract(DateTime.Now).TotalDays > 0)
                {
                    return new ValidationResult("This birth date is unvalid .Please specify an earlier date");
                }
                else
                    return ValidationResult.Success;
            }
            return new ValidationResult("This is not a valid date ");
        }
    }
}
