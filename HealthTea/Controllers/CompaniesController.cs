using HealthTea.Data.Services;
using HealthTea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthTea.Controllers
{
	public class CompaniesController : Controller
	{
		private readonly ICompanyService _service;

		public CompaniesController(ICompanyService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			var companies = await _service.GetAllAsync();
			return View(companies);
		}

		[Authorize]
		public IActionResult Create()
		{
			if (User.IsInRole("Admin"))
			{
				return View();
			}
			return RedirectToAction("AccessDenied", "Account");
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create([Bind("ImageUrl, Name")] Company company)
		{
			if (!ModelState.IsValid)
			{
				return View(company);
			}
			await _service.AddAsync(company);
			return RedirectToAction(nameof(Index));
		}

		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var companyDetails = await _service.GetByIdAsync(id);

			if (companyDetails == null) return View("NotFound");
			return View(companyDetails);
		}

		[Authorize]
		public async Task<IActionResult> Edit(int id)
		{
			if (User.IsInRole("Admin"))
			{
				var companyDetails = await _service.GetByIdAsync(id);
				if (companyDetails == null) return View("NotFound");
				return View(companyDetails);
			}
			return RedirectToAction("AccessDenied", "Account");
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Edit(int id, [Bind("Id, Name, ImageUrl")] Company company)
		{
			if (!ModelState.IsValid)
			{
				return View(company);
			}
			await _service.UpdateAsync(id, company);
			return RedirectToAction(nameof(Index));
		}

		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			if (User.IsInRole("Admin"))
			{
				var companyDetails = await _service.GetByIdAsync(id);
				if (companyDetails == null) return View("NotFound");
				return View(companyDetails);
			}
			return RedirectToAction("AccessDenied", "Account");
		}

		[HttpPost, ActionName("Delete")]
		[Authorize]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var companyDetails = await _service.GetByIdAsync(id);
			if (companyDetails == null) return View("NotFound");

			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
