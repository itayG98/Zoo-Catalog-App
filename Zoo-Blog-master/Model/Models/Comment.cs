using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Comment
    {
        [Key]
        public Guid CommentId { get; set; }
        [Required]
        public virtual Guid AnimalID { get; set; }

        [ForeignKey("AnimalID")]
        public virtual Animal Animal { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [RegularExpression(".{3,200}$", ErrorMessage = "Please enter between 3-200 charecters")]
        [Display(Name = "Content")]
        public string? Content { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Comment other && other != default)
                return other.CommentId == CommentId;
            return false;
        }
    }
}

