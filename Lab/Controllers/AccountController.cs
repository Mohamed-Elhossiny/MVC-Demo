using Lab.Models;
using Lab.Repository;
using Lab.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lab.Controllers
{
	public class AccountController : Controller
	{
		IAccountRepository accountRepo;
        public AccountController(IAccountRepository _accountRepo)
        {
			this.accountRepo = _accountRepo;
        }

		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
        public IActionResult Register(AccountUserViewModel accountVM)
        {
			if(ModelState.IsValid)
			{
				Account account = new Account();
				account.Username = accountVM.Username;
				account.Password = accountVM.Password;

				accountRepo.Create(account);
				accountRepo.Save();
				ClaimsIdentity claims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
				claims.AddClaim(new Claim(ClaimTypes.Name, account.Username));
				claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()));
				claims.AddClaim(new Claim(ClaimTypes.Role, accountRepo.GetRoles(account.Id)));

				ClaimsPrincipal principal = new ClaimsPrincipal(claims);
				HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
				 return RedirectToAction("Index","Course");
			}
            return View(accountVM);
        }
		[Authorize(Roles = "Admin")]
		public IActionResult Show()
		{
			return Content("Welcome "+ User.Identity.Name);
		}
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
        public IActionResult Login(AccountUserViewModel account)
        {

			if (accountRepo.Find(account.Username, account.Password))
			{
				Account accountDb = accountRepo.Get(account.Username, account.Password);

				ClaimsIdentity claims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
				claims.AddClaim(new Claim(ClaimTypes.Name,accountDb.Username));
				claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, accountDb.Id.ToString()));
				claims.AddClaim(new Claim(ClaimTypes.Role,accountRepo.GetRoles(accountDb.Id)));

				ClaimsPrincipal principle = new ClaimsPrincipal(claims);
				HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
				return RedirectToAction("Index","Course");
			}
            return View(account);
        }

		public IActionResult LogOut()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login");
		}

        public IActionResult Index()
		{
			return View();
		}
	}
}
