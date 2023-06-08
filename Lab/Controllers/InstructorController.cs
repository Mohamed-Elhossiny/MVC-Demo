using Lab.Models;
using Microsoft.AspNetCore.Mvc;
using Lab.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lab.Controllers
{
	public class InstructorController : Controller
	{
		MvcContext db = new MvcContext();

		public IActionResult GetCourseByDept(int deptId)
		{
			var courses = db.Courses.Where(c => c.Dept_Id == deptId).Select(c=> new {c.Id,c.Name}).ToList();
			return Json(courses);
		}
		public IActionResult Index()
		{
			List<Instructor> InstList = db.Instructors.ToList();
			InstDeptCourseViewModel InsModel = new InstDeptCourseViewModel();
			InsModel.InstList = InstList;
			return View(InsModel);
		}
		public IActionResult Details(int id)
		{
			Instructor instructor = db.Instructors.SingleOrDefault(i => i.Id == id);
			Department department = db.Departments.SingleOrDefault(d => d.Id == instructor.Dept_Id);
			Course course = db.Courses.SingleOrDefault(c => c.Id == instructor.Course_Id);

			InstDeptCourseViewModel insModel = new InstDeptCourseViewModel();
			insModel.InsName = instructor.Name;
			insModel.InsID = instructor.Id;
			insModel.InsSalary = (decimal)instructor.Salary;
			insModel.ImageSrc = instructor.Image;
			insModel.DeptName = department.Name;
			insModel.CrsName = course.Name;

			return View("details", insModel);
		}
		[HttpPost]
		public IActionResult Search(string name)
		{
			if (name != null)
			{
				Instructor instructor = db.Instructors.Where(i => i.Name.Contains(name)).FirstOrDefault();
				if (instructor != null)
				{
					Department dept = db.Departments.FirstOrDefault(d => d.Id == instructor.Dept_Id);
					Course course = db.Courses.FirstOrDefault(c => c.Id == instructor.Course_Id);
					InstDeptCourseViewModel intVM = new InstDeptCourseViewModel();
					intVM.InsName = instructor.Name;
					intVM.InsID = instructor.Id;
					intVM.InsSalary = (decimal)instructor.Salary;
					intVM.ImageSrc = instructor.Image;
					intVM.DeptName = dept.Name;
					intVM.CrsName = course.Name;
					return View("Search", intVM);
				}
				else
				{
					return Content("There is N Instructor have this Name");
				}
			}
			return RedirectToAction("index");
		}

		public IActionResult Add()
		{
			ViewData["DeptList"] = db.Departments.ToList();
			ViewData["CourseList"] = db.Courses.ToList();
			return View();
		}
		[HttpPost]
		public IActionResult Save(Instructor instructor)
		{

			if (ModelState.IsValid == true)
			{
				db.Instructors.Add(instructor);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewData["DeptList"] = db.Departments.ToList();
			ViewData["CourseList"] = db.Courses.ToList();
			return View("Add", instructor);
		}
		public IActionResult Edit(int id)
		{
			Instructor instructor = db.Instructors.FirstOrDefault(i => i.Id == id);
			ViewData["deptList"] = db.Departments.ToList();
			ViewData["courseList"] = db.Courses.ToList();
			return View(instructor);
		}
		[HttpPost]
		public IActionResult Edit(Instructor ins, int id)
		{

			if (ModelState.IsValid == true)
			{
				Instructor instVM = db.Instructors.Where(i => i.Id == id).FirstOrDefault();
				instVM.Name = ins.Name;
				instVM.Salary = ins.Salary;
				instVM.Address = ins.Address;
				instVM.Image = ins.Image;
				instVM.Dept_Id = ins.Dept_Id;
				instVM.Course_Id = ins.Course_Id;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewData["deptList"] = db.Departments.ToList();
			ViewData["courseList"] = db.Courses.ToList();
			return View(ins);

		}
		public IActionResult CheckSalary(decimal Salary)
		{
			if (Salary < 5000)
			{
				return Json(false);
			}
			return Json(true);
		}


	}
}
