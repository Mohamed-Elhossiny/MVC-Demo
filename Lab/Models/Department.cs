namespace Lab.Models
{
	public class Department
	{
        public int Id { get; set; }
		public string Name { get; set; }
        public string? Manager { get; set; }
        public virtual List<Instructor>? Instructors { get; set; }
    }
}
