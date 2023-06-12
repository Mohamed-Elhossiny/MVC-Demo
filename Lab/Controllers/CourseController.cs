using Lab.Models;
using Lab.Repository;
using Lab.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Controllers
{
    public class CourseController : Controller
    {
        //MvcContext db = new MvcContext();
        ICourseRepository courseRepository;
        IDepartmentRepository deptRepository;
        public CourseController(ICourseRepository _courseRepo,IDepartmentRepository _deptRepo)
        {
            courseRepository = _courseRepo;
            deptRepository = _deptRepo;
        }
        public IActionResult Index()
        {
            List<Course> courses = courseRepository.GetAll();
            CourseDepartmentInstrucViewModel viewModel = new CourseDepartmentInstrucViewModel();
            viewModel.C_List = courses;
            return View(viewModel);
        }
        public IActionResult Add()
        {
            ViewData["deptList"] = deptRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Course course)
        {

            if (ModelState.IsValid == true)
            {
                try
                {
                    //db.Courses.Add(course);
                    courseRepository.Add(course);
                    //db.SaveChanges();
                    courseRepository.Save();
                    return RedirectToAction("Index");
                }catch(Exception ex)
                {
                    ModelState.AddModelError("",ex.Message);
                }
            }
            ViewData["deptList"] = deptRepository.GetAll();
            return View("Add", course);
        }
        public IActionResult Edit(int id)
        {
            Course course = courseRepository.GetById(id);
            ViewData["deptList"] = deptRepository.GetAll();
            return View(course);
        }
        [HttpPost]
        public IActionResult Edit(Course course, int id)
        {
            
            if(ModelState.IsValid==true)
            {
                //Course courseDB = courseRepository.GetById(id);
                //courseDB.Name = course.Name;
                //courseDB.MinDegree = course.MinDegree;
                //courseDB.Dept_Id = course.Dept_Id;
                courseRepository.Update(id, course);
                courseRepository.Save();
                return RedirectToAction("index");
            }
            ViewData["deptList"] = deptRepository.GetAll();
            return View(course);
        }
        public IActionResult Delete(int id)
        {
            Course course = courseRepository.GetById(id);
            ViewData["deptList"] = deptRepository.GetAll();
            string confirmMsg = "Confirm Deleting Course";
            ViewData["confrim"] = confirmMsg;
            return View(course);
        }
        [HttpPost]
        public IActionResult Delete(Course coures, int id)
        {
            Course courseModel = courseRepository.GetById(id);
            //if (courseModel != null)
            if (ModelState.IsValid == true)
            {
                courseRepository.Delete(id);
                courseRepository.Save();
                return RedirectToAction("Index");
            }
            ViewData["deptList"] = deptRepository.GetAll();
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
