using Microsoft.AspNetCore.Mvc;

namespace Lab.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult SetSession(string name)
        {
            HttpContext.Session.SetString("name", name);
            HttpContext.Session.SetInt32("Age", 33);
            return Content("Session Saved");
        }
        public IActionResult GetSession()
        {
            string name = HttpContext.Session.GetString("name");
            int? age = HttpContext.Session.GetInt32("Age");

            return Content($"name: {name} || Age: {age}");

        }
        public IActionResult SetCookie()
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTimeOffset.Now.AddHours(3);
            HttpContext.Response.Cookies.Append("Name", "Mohamed",option);
            HttpContext.Response.Cookies.Append("Age", "26");
            return Content("Cookie Added");
        }
        public IActionResult GetCookie()
        {
            string cookieName = HttpContext.Request.Cookies["Name"];
            string cookieAge = HttpContext.Request.Cookies["Age"];

            return Content($"CookieName: {cookieName}\n CookieAge: {cookieAge}");
        }
    }
}
