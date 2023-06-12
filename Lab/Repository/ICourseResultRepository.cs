using Lab.Models;

namespace Lab.Repository
{
	public interface ICourseResultRepository
	{
		List<CourseResult> GetAll();
		CourseResult GetById(int id);
		void Add(CourseResult courseReuslt);
		void Update(int c_Id, CourseResult courseResult);
		CourseResult GetByTraineeAndCourse(int t_id, int c_id);
		List<CourseResult> GetByCourseIdWithLazy(int c_id);
		List<CourseResult> GetByTraineeIdWithLazy(int t_id);
		void Delete(int c_Id);
		void Save();
	}
}
