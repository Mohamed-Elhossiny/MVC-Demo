using Lab.Models;

namespace Lab.Repository
{
	public interface ICourseRepository
	{
		List<Course> GetAll();
		Course GetById(int id);
		void Add(Course course);
		void Update(int c_Id, Course course);
		void Delete(int c_Id);
		void Save();
		List<Course> GetCourseByDeptID(int  deptID);
		Course GetByInstructor(Instructor instructor);

	}
}