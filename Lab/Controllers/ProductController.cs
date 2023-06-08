using Lab.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult GetAll()
		{
			List<Product> products = ProductBL.GetAllProducts();
			return View("AllProducts",products);
		}
		public IActionResult Details(int id)
		{
			Product product = ProductBL.GetProductByID(id);
			return View("Details",product);
		}
	}
}
