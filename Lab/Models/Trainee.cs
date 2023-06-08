using System.ComponentModel.DataAnnotations.Schema;

namespace Lab.Models
{
	public class Trainee
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public int Grade { get; set; }
        [ForeignKey("Dept")]
        public int Dept_Id { get; set; }
        public virtual Department? Dept { get; set; }
        public virtual List<CourseResult> CrsResult { get; set; }
    }
}
