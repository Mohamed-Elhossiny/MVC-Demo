using Lab.Models;
using Lab.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Controllers
{
    public class CourseController : Controller
    {
        MvcContext db = new MvcContext();
        public IActionResult Index()
        {
            List<Course> courses = db.Courses.ToList();
            CourseDepartmentInstrucViewModel viewModel = new CourseDepartmentInstrucViewModel();
            viewModel.C_List = courses;
            return View(viewModel);
        }
        public IActionResult Add()
        {
            ViewData["deptList"] = db.Departments.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Course course)
        {

            if (ModelState.IsValid == true)
            {
                try
                {
                    db.Courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }catch(Exception ex)
                {
                    ModelState.AddModelError("",ex.Message);
                }
            }
            ViewData["deptList"] = db.Departments.ToList();
            return View("Add", course);
        }
        public IActionResult Edit(int id)
        {
            Course course = db.Courses.FirstOrDefault(c => c.Id == id);
            ViewData["deptList"] = db.Departments.ToList();
            return View(course);
        }
        [HttpPost]
        public IActionResult Edit(Course course, int id)
        {
            
            if(ModelState.IsValid==true)
            {
                Course courseDB = db.Courses.FirstOrDefault(c => c.Id == id);
                courseDB.Name = course.Name;
                courseDB.MinDegree = course.MinDegree;
                courseDB.Dept_Id = course.Dept_Id;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            ViewData["deptList"] = db.Departments.ToList();
            return View(course);
        }
        public IActionResult Delete(int id)
        {
            Course course = db.Courses.FirstOrDefault(c => c.Id == id);
            ViewData["deptList"] = db.Departments.ToList();
            string confirmMsg = "Confirm Deleting Course";
            ViewData["confrim"] = confirmMsg;
            return View(course);
        }
        [HttpPost]
        public IActionResult Delete(Course coures, int id)
        {
            Course courseModel = db.Courses.FirstOrDefault(c => c.Id == id);
            //if (courseModel != null)
            if (ModelState.IsValid == true)
            {
                db.Courses.Remove(courseModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["deptList"] = db.Departments.ToList();
            return View(coures);
        }
        public IActionResult Check(int minDegree,int degree)
        {
            if (minDegree > degree)
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}
