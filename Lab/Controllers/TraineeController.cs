using Lab.Models;
using Lab.Repository;
using Lab.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab.Controllers
{
    public class TraineeController : Controller
    {
        //MvcContext db = new MvcContext();
        ITraineeRepository traineeRepo;
        ICourseRepository courseRepo;
        ICourseResultRepository courseResultRepo;
        public TraineeController
            (ITraineeRepository _TraineeRepo,ICourseRepository _courseRepo,ICourseResultRepository _courResRepo)
        {
            this.traineeRepo = _TraineeRepo;
            this.courseRepo = _courseRepo;
            courseResultRepo = _courResRepo;
        }
        public IActionResult ShowResult(int tid, int cid)
        {
            Trainee trainee = traineeRepo.GetById(tid);
            Course course = courseRepo.GetById(cid);
            CourseResult courseResult = courseResultRepo.GetByTraineeAndCourse(tid,cid);
            TraineeCourseViewModel traineeModel = new TraineeCourseViewModel();
            traineeModel.T_Name = trainee.Name;
            traineeModel.C_Name = course.Name;
            traineeModel.MinDegree = course.MinDegree;
            traineeModel.Degree = courseResult.Degree;
            if (traineeModel.Degree < traineeModel.MinDegree)
            {
                traineeModel.Color = "red";
            }
            else
            {
                traineeModel.Color = "green";
            }

            return View(traineeModel);
        }

        // Using Quary Syntax to join Three tables

        //public IActionResult showCourseR(int id)
        //{
        //    List<CourseResult> courseResults = db.CourseResults.ToList();
        //    List<Course> courses = db.Courses.ToList();
        //    List<Trainee> trainees = db.Trainees.ToList();

        //    var TraineeCourses = (from courseRes in courseResults
        //                          join course in courses on courseRes.Course_Id equals course.Id
        //                          join trainee in trainees on courseRes.Trainee_ID equals trainee.Id
        //                          where courseRes.Course_Id == id
        //                          select new TraineeCourseViewModel
        //                          {
        //                              C_Name = course.Name,
        //                              T_Name = trainee.Name,
        //                              Degree = courseRes.Degree,
        //                              MinDegree = course.MinDegree,
        //                              Color = courseRes.Degree < course.MinDegree ? "red" : "green"

        //                          }).ToList();

        //    return View(TraineeCourses);
        //}

        // Using Quary Syntax to join Three tables


        //     public IActionResult showtraineeR(int id)
        //     {
        //List<CourseResult> courseResults = db.CourseResults.ToList();
        //List<Course> courses = db.Courses.ToList();
        //List<Trainee> trainees = db.Trainees.ToList();

        //         var TraineeResult = (from courseRes in courseResults
        //                             join course in courses on courseRes.Course_Id equals course.Id
        //                             join trainee in trainees on courseRes.Trainee_ID equals trainee.Id
        //                             where trainee.Id == id
        //                             select new TraineeCourseViewModel
        //                             {
        //                                 C_Name = course.Name,
        //                                 T_Name = trainee.Name,
        //                                 Degree = courseRes.Degree,
        //                                 MinDegree = course.MinDegree,
        //                                 Color = courseRes.Degree < course.MinDegree ? "red" : "green",
        //                             }).ToList();
        //return View(TraineeResult);
        //     }

        public IActionResult ShowCourseDetails(int id)
        {
            List<CourseResult> courseResults = courseResultRepo.GetByCourseIdWithLazy(id);
            List<TraineeCourseViewModel> list = new List<TraineeCourseViewModel>();
            foreach (var item in courseResults)
            {
                list.Add(new TraineeCourseViewModel
                {
                    C_Name = item.Course.Name,
                    T_Name = item.Trainee.Name,
                    Degree = item.Degree,
                    MinDegree = item.Course.MinDegree,
                    Color = item.Degree > item.Course.MinDegree ? "green" : "red"
                });
            }
            return View(list);
        }

        public IActionResult showTraineeDetails(int id)
        {
            List<CourseResult> courseResults = courseResultRepo.GetByTraineeIdWithLazy(id);
            List<TraineeCourseViewModel> list = new List<TraineeCourseViewModel>();
            foreach (var item in courseResults)
            {
                list.Add(new TraineeCourseViewModel
                {
                    C_Name = item.Course.Name,
                    T_Name = item.Trainee.Name,
                    Degree = item.Degree,
                    Color = item.Course.MinDegree < item.Degree ? "red" : "green"
                });
                
            }
            return View("showtraineeR", list);

        }

		public IActionResult GetCookie()
		{
			string cookieName = HttpContext.Request.Cookies["Name"];
			string cookieAge = HttpContext.Request.Cookies["Age"];

			return Content($"CookieName: {cookieName}\n CookieAge: {cookieAge}");
		}
	}
}
