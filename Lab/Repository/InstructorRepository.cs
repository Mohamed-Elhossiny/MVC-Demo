using Lab.Models;
using Lab.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Lab.Repository
{
	public class InstructorRepository : IInstructorRepository
	{
		MvcContext db;
        public InstructorRepository(MvcContext _db)
        {
			this.db = _db;
        }
        public void Add(Instructor instructor)
		{
			db.Instructors.Add(instructor);
		}

		public void Delete(int I_id)
		{
			Instructor instructor = db.Instructors.SingleOrDefault(x => x.Id == I_id);
			db.Instructors.Remove(instructor);
		}

		public List<Instructor> GetAll()
		{
			return db.Instructors.ToList();
		}

		public Instructor GetById(int I_id)
		{
			return db.Instructors.FirstOrDefault(i=>i.Id==I_id);
		}

		public InstDeptCourseViewModel GetByIdWithLazyLoading(int id)
		{
			return db.Instructors.Include(i => i.Course).Include(i => i.Dept).Where(i => i.Id == id)
				.Select(i=> new InstDeptCourseViewModel
				{
					InsName = i.Name,
					InsID = i.Id,
					ImageSrc = i.Image,
					InsSalary = i.Salary,
					DeptName = i.Dept.Name,
					CrsName = i.Course.Name
				}).FirstOrDefault();
		}

		public Instructor GetByName(string name)
		{
			return db.Instructors.Where(i=>i.Name.Contains(name)).FirstOrDefault();
		}

		public void Save()
		{
			db.SaveChanges();
		}

		public void Update(int I_id, Instructor instructor)
		{
			Instructor insDb = db.Instructors.FirstOrDefault(i => i.Id == I_id);
			insDb.Name = instructor.Name;
			insDb.Address = instructor.Address;
			insDb.Image = instructor.Image;
			insDb.Salary = instructor.Salary;
			insDb.Dept_Id = instructor.Dept_Id;
			insDb.Course_Id = instructor.Course_Id;

		}
	}
}
