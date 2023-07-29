using HealthTea.Data.Services;
using HealthTea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthTea.Controllers
{
	public class OriginsController : Controller
	{
		private readonly IOriginService _service;

		public OriginsController(IOriginService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			var origins = await _service.GetAllAsync();
			return View(origins);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([Bind("ImageUrl, Name")] Origin origin)
		{
			if (!ModelState.IsValid)
			{
				return View(origin);
			}
			await _service.AddAsync(origin);
			return RedirectToAction(nameof(Index));
		}

		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var originDetails = await _service.GetByIdAsync(id);

			if (originDetails == null) return View("NotFound");
			return View(originDetails);
		}


		public async Task<IActionResult> Edit(int id)
		{
			var originDetails = await _service.GetByIdAsync(id);
			if (originDetails == null) return View("NotFound");
			return View(originDetails);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Id, Name, ImageUrl")] Origin origin)
		{
			if (!ModelState.IsValid)
			{
				return View(origin);
			}
			await _service.UpdateAsync(id, origin);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int id)
		{
			var originDetails = await _service.GetByIdAsync(id);
			if (originDetails == null) return View("NotFound");
			return View(originDetails);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var originDetails = await _service.GetByIdAsync(id);
			if (originDetails == null) return View("NotFound");

			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
