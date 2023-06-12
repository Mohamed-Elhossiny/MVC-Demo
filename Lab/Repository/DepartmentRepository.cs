using Lab.Models;

namespace Lab.Repository
{
	public class DepartmentRepository : IDepartmentRepository
	{
		MvcContext db;
		public DepartmentRepository(MvcContext _db)
		{
			db = _db;
		}

		public void Add(Department dept)
		{
			db.Departments.Add(dept);
		}

		public void Delete(int D_Id)
		{
			Department dept = db.Departments.FirstOrDefault(d=>d.Id==D_Id);
			db.Departments.Remove(dept);
		}

		public List<Department> GetAll()
		{
			return db.Departments.ToList();
		}

		public Department GetById(int D_Id)
		{
			Department dept = db.Departments.FirstOrDefault(d => d.Id == D_Id);
			return dept;
		}


		public void Update(int D_Id, Department dept)
		{
			Department deptDB = db.Departments.FirstOrDefault(d => d.Id == D_Id);
			deptDB.Name = dept.Name;
			deptDB.Manager = dept.Manager;
		}
		public void Save()
		{
			db.SaveChanges();
		}

		public Department GetByInst_Id(Instructor instructor)
		{
			return db.Departments.FirstOrDefault(d=>d.Id==instructor.Dept_Id);
		}
	}
}
