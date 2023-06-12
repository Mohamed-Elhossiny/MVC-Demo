using Lab.Models;
namespace Lab.Repository
{
	public class CourseRepository : ICourseRepository
	{
		MvcContext db;
        public CourseRepository(MvcContext _db)
        {
            db = _db;
        }
        public List<Course> GetAll()
        {
            return db.Courses.ToList();
        }
        public Course GetById(int id)
        {
            return db.Courses.SingleOrDefault(c=>c.Id == id);
        }
        public void Add(Course course)
        {
            db.Courses.Add(course);
        }
        public void Update(int c_Id,Course course)
        {
            Course courseDb = db.Courses.FirstOrDefault(course=>course.Id == c_Id);
            courseDb.Name = course.Name;
            courseDb.Degree = course.Degree;
            courseDb.MinDegree = course.MinDegree;
            courseDb.Dept_Id = course.Dept_Id;
        }
        public void Delete(int c_Id)
        {
            Course course = GetById(c_Id);
            db.Courses.Remove(course);
        }
        public void Save()
        {
            db.SaveChanges();
        }

		public List<Course> GetCourseByDeptID(int deptID)
		{
            return db.Courses.Where(c => c.Dept_Id == deptID).ToList();
		}

		public Course GetByInstructor(Instructor instructor)
		{
			return db.Courses.FirstOrDefault(c=>c.Id==instructor.Course_Id);
		}
	}
}
