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
			var companies =  await _service.GetAllAsync();
			return View(companies);
		}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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


        public async Task<IActionResult> Edit(int id)
        {
            var companyDetails = await _service.GetByIdAsync(id);
            if (companyDetails == null) return View("NotFound");
            return View(companyDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, ImageUrl")] Company company)
        {
            if (!ModelState.IsValid)
            {
                return View(company);
            }
            await _service.UpdateAsync(id, company);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var companyDetails = await _service.GetByIdAsync(id);
            if (companyDetails == null) return View("NotFound");
            return View(companyDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyDetails = await _service.GetByIdAsync(id);
            if (companyDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
