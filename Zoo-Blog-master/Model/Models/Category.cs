using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryID { get; set; }
        [Required]
        [Display(Name = "Name")]
        [RegularExpression("[A-Za-z\\s]{2,25}$")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Category other && other != default)
                return other.CategoryID == CategoryID;
            return false;
        }
    }
}
