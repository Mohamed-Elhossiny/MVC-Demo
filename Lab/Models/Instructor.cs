using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab.Models
{
	public class Instructor
	{
        public int Id { get; set; }
        
        //[RegularExpression(@"[a-zA-z]{3,}+[\s]{1}[\w*]")]
        [MaxLength(25)]
        [MinLength(3)]
        public string Name { get; set; }

        [RegularExpression(@"^\w*\.(jpg|png|jpeg|gif)$",ErrorMessage ="Write Correct Extension for Image")]
        public string? Image { get; set; }

        [Remote("CheckSalary","Instructor",ErrorMessage ="Min Salary Must be 5000")]
        [Column(TypeName ="money")]
        public decimal? Salary { get; set; }

        [RegularExpression(@"^(Cairo|Alex|Giza|Sirs)$",ErrorMessage ="Address must be within (Cairo | Alex | Giza | Sirs)")]
        public string? Address { get; set; }
        [ForeignKey("Dept")]
        public int Dept_Id { get; set; }
        public virtual Department? Dept { get; set; }
        [ForeignKey("Course")]
        public int Course_Id { get; set; }
        public virtual Course? Course { get; set; }

    }
}
