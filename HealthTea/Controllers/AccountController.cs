using HealthTea.Data;
using HealthTea.Data.Constants;
using HealthTea.Data.ViewModels;
using HealthTea.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthTea.Controllers
{
    public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly AppDbContext _context;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
		}

		public async Task<IActionResult> Users()
		{
			var users = await _context.Users.ToListAsync();
			return View(users);
		}

		public IActionResult Login()
		{
			return View(new LoginViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid) return View(loginViewModel);

			var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
			if (user != null)
			{
				var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
				if (passwordCheck)
				{
					var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
					if (result.Succeeded)
					{
						return RedirectToAction(nameof(Index), "Teas");
					}
				}
				TempData["Error"] = ErrorMessages.INVALID_CREDENTIALS;
				return View(loginViewModel);
			}

			TempData["Error"] = ErrorMessages.INVALID_CREDENTIALS;
			return View(loginViewModel);
		}

		public IActionResult Register()
		{
			return View(new RegisterViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (!ModelState.IsValid) return View(registerViewModel);

			var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
			if (user != null)
			{
				TempData["Error"] = ErrorMessages.EMAIL_ADDRESS_IN_USE;
				return View(registerViewModel);
			}

			var newUser = new ApplicationUser()
			{
				FullName = registerViewModel.FullName,
				Email = registerViewModel.EmailAddress,
				UserName = registerViewModel.EmailAddress
			};
			var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

			if (newUserResponse.Succeeded)
			{
				await _userManager.AddToRoleAsync(newUser, UserRoles.User);
			}
			else
			{
				var errorMessage = ErrorMessages.REGISTRATION_FAILED;
				newUserResponse.Errors.ToList().ForEach(error =>
				{
					errorMessage += error.Description.ToString() + "\n";
				});
				TempData["Error"] = errorMessage;
				return View(registerViewModel);
			}

			return View("RegistrationCompleted");
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Index), "Teas");
		}

		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
