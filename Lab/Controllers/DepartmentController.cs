using Lab.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Controllers
{
	public class DepartmentController : Controller
	{
		MvcContext db = new MvcContext();
		public IActionResult Index()
		{
			List<Department> depts = db.Departments.ToList();
			return View(depts);
		}
	}
}
