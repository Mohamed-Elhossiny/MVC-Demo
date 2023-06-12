using Lab.Models;
using Lab.ViewModel;

namespace Lab.Repository
{
	public interface IInstructorRepository
	{
		List<Instructor> GetAll();
		Instructor GetById(int I_id);
		void Add(Instructor instructor);
		void Update(int I_id, Instructor instructor);
		void Delete(int I_id);
		void Save();
		Instructor GetByName(string name);
		InstDeptCourseViewModel GetByIdWithLazyLoading(int id);
	}
}
