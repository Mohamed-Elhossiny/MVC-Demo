using Lab.Models;

namespace Lab.Repository
{
	public interface IDepartmentRepository
	{
		List<Department> GetAll();
		Department GetById(int id);
		void Add(Department dept);
		void Update(int D_Id, Department dept);
		void Delete(int D_Id);
		void Save();
	}
}
