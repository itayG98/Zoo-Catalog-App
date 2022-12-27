using Microsoft.AspNetCore.Http;
using Model.Models.Attributes;
using Model.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    public class Animal
    {
        private const string DefaultPath = "InitImages/Defult.jpg";
        private static readonly byte[] _deafualtRawData;

        static Animal()
        {
            _deafualtRawData = ImagesFormater.ImageToBytesArrayFromLocalPath(DefaultPath);
        }

        [Key]
        public Guid ID { get; set; }


        [Required(ErrorMessage = "Enter the name")]
        [Display(Name = "Name")]
        [RegularExpression("[A-Za-z\\s]{2,25}$",ErrorMessage = "Enter valid name contain 2-25 letters only only")]
        [MinLength(2)]
        [MaxLength(25)]
        [DataType(DataType.Text)]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Enter description please")]
        [Display(Name = "Description")]
        [RegularExpression(".{3,200}$",ErrorMessage ="Please enter between 3-200 charecters")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }


        public virtual Guid CategoryID { get; set; }


        [Display(Name = "Category")]
        [ForeignKey("CategoryID")]
        [EnumDataType(typeof(CategoriesEnum))]
        public virtual Category? Category { get; set; }

        [Required]
        [NotMapped]
        [Display(Name = "Category")]
        public CategoriesEnum CategoryEnum { get; set; }


        [Required(ErrorMessage = "Please enter birth date")]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        [DateValidation]
        public DateTime BirthDate { get; set; }


        [Display(Name = "Raw Image Data")]
        public byte[] ImageRawData { get; set; } = DeafualtRawData;

        [NotMapped]
        [ImageFileValidation]
        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }
        
        public virtual ICollection<Comment>? Comments { get; set; }  

        [NotMapped]
        public int Age => (int)DateTime.Now.Subtract(BirthDate).TotalDays / 365;
        [NotMapped]
        public static byte[] DeafualtRawData => _deafualtRawData;
        public override bool Equals(object? obj)
        {
            if (obj is Animal other && other != default)
                return other.ID == ID;
            return false;
        }
    }


}

