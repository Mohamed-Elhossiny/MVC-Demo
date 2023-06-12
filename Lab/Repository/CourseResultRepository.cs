using Lab.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Lab.Repository
{
	public class CourseResultRepository:ICourseResultRepository
	{
		MvcContext db;
		public CourseResultRepository(MvcContext _db)
		{
			db = _db;
		}
		public List<CourseResult> GetAll()
		{
			return db.CourseResults.ToList();
		}
		public CourseResult GetById(int id)
		{
			return db.CourseResults.SingleOrDefault(c => c.Id == id);
		}
		public void Add(CourseResult courseResult)
		{
			db.CourseResults.Add(courseResult);
		}
		public void Update(int c_Id, CourseResult courseResult)
		{
			CourseResult courseDb = db.CourseResults.FirstOrDefault(course => course.Id == c_Id);
			courseDb.Degree = courseResult.Degree;
			courseDb.Trainee_ID = courseResult.Trainee_ID;
			courseDb.Course_Id = courseResult.Course_Id;
			
		}
		public void Delete(int c_Id)
		{
			CourseResult courseResult = GetById(c_Id);
			db.CourseResults.Remove(courseResult);
		}
		public void Save()
		{
			db.SaveChanges();
		}

		public CourseResult GetByTraineeAndCourse(int t_id, int c_id)
		{
			return db.CourseResults.SingleOrDefault(cr => cr.Trainee_ID == t_id && cr.Course_Id == c_id);
		}

		public List<CourseResult> GetByCourseIdWithLazy(int c_id)
		{
			return db.CourseResults.Include(c => c.Course).Include(c => c.Trainee).Where(c => c.Course_Id == c_id).ToList();
		}

		public List<CourseResult> GetByTraineeIdWithLazy(int t_id)
		{
			return db.CourseResults.Include(c => c.Course).Include(c => c.Trainee).Where(c => c.Trainee_ID == t_id).ToList();
		}
	}
}
