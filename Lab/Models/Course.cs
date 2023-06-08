using Lab.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab.Models
{
	public class Course
	{
		public int Id { get; set; }

		[MaxLength(25,ErrorMessage ="Max Lenght must be < 25")]
		[MinLength(2,ErrorMessage ="Min Lehth must be > 2")]
		[UniqueName]
		public string Name { get; set; }
		
		[Range(50,100)]
		public int? Degree { get; set; }
		[Remote("Check","Course",ErrorMessage ="Min Degree must be Less than Degree",AdditionalFields ="Degree")]
		public int MinDegree { get; set; }
		[ForeignKey("Dept")]
		public int Dept_Id { get; set; }
		public virtual Department? Dept { get; set; }
		public virtual List<Instructor>? Instructors { get; set; }
        public virtual List<CourseResult>? CrsResult { get; set; }
    }
}
