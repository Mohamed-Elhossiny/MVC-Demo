using Lab.Models;
using Microsoft.AspNetCore.Mvc;
using Lab.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Lab.Repository;

namespace Lab.Controllers
{
	public class InstructorController : Controller
	{
		MvcContext db = new MvcContext();
		IInstructorRepository InsRepo;
		ICourseRepository CourseRepo;
		IDepartmentRepository DeptRepo;
		public InstructorController
			(IInstructorRepository _InsRepo, ICourseRepository _CousrRepo, IDepartmentRepository _DeptRepo)
		{
			this.InsRepo = _InsRepo;
			this.CourseRepo = _CousrRepo;
			this.DeptRepo = _DeptRepo;
		}
		public IActionResult GetCourseByDept(int deptId)
		{
			var courses = CourseRepo.GetCourseByDeptID(deptId).Select(c => new { c.Id, c.Name }).ToList();
			return Json(courses);
		}
		[Route("allinstructor")]
		public IActionResult Index()
		{
			List<Instructor> InstList = InsRepo.GetAll();
			InstDeptCourseViewModel InsModel = new InstDeptCourseViewModel();
			InsModel.InstList = InstList;
			return View(InsModel);
		}
		[Route("insdetails/{id:int}")]
		public IActionResult Details(int id)
		{
			Instructor instructor = InsRepo.GetById(id);
			Department department = DeptRepo.GetByInst_Id(instructor);
			Course course = CourseRepo.GetByInstructor(instructor);

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
				Instructor instructor = InsRepo.GetByName(name);
				if (instructor != null)
				{
					Department dept = DeptRepo.GetByInst_Id(instructor);
					Course course = CourseRepo.GetByInstructor(instructor);
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
			ViewData["DeptList"] = DeptRepo.GetAll();
			ViewData["CourseList"] = CourseRepo.GetAll();
			return View();
		}
		[HttpPost]
		public IActionResult Save(Instructor instructor)
		{

			if (ModelState.IsValid == true)
			{
				InsRepo.Add(instructor);
				InsRepo.Save();
				return RedirectToAction("Index");
			}
			ViewData["DeptList"] = DeptRepo.GetAll();
			ViewData["CourseList"] = CourseRepo.GetAll();
			return View("Add", instructor);
		}
		public IActionResult Edit(int id)
		{
			Instructor instructor = InsRepo.GetById(id);
			ViewData["deptList"] = DeptRepo.GetAll();
			ViewData["courseList"] = CourseRepo.GetAll();
			return View(instructor);
		}
		[HttpPost]
		public IActionResult Edit(Instructor ins, int id)
		{

			if (ModelState.IsValid == true)
			{
				Instructor instVM = InsRepo.GetById(id);
				instVM.Name = ins.Name;
				instVM.Salary = ins.Salary;
				instVM.Address = ins.Address;
				instVM.Image = ins.Image;
				instVM.Dept_Id = ins.Dept_Id;
				instVM.Course_Id = ins.Course_Id;
				InsRepo.Save();
				return RedirectToAction("Index");
			}
			ViewData["deptList"] = DeptRepo.GetAll();
			ViewData["courseList"] = CourseRepo.GetAll();
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

		public IActionResult getpartialdetails(int id)
		{
			//var ins = db.Instructors.Include(i => i.Course).Include(i => i.Dept).Where(i => i.Id == id)
			//.Select(i => new InstDeptCourseViewModel
			//{
			//	InsName = i.Name,
			//	InsID = i.Id,
			//	ImageSrc = i.Image,
			//	InsSalary = i.Salary,
			//	DeptName = i.Dept.Name,
			//	CrsName = i.Course.Name
			//});
			var ins = InsRepo.GetByIdWithLazyLoading(id);

			return PartialView("_DetailsPartialView", ins);

		}

	}
}
