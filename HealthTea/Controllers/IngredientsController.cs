using HealthTea.Data.Services;
using HealthTea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthTea.Controllers
{
	public class IngredientsController : Controller
	{
		private readonly IIngredientService _service;

		public IngredientsController(IIngredientService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			var ingredients = await _service.GetAllAsync();
			return View(ingredients);
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
		public async Task<IActionResult> Create([Bind("ImageUrl, Name")] Ingredient ingredient)
		{
			if (!ModelState.IsValid)
			{
				return View(ingredient);
			}
			await _service.AddAsync(ingredient);
			return RedirectToAction(nameof(Index));
		}

		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var ingredientDetails = await _service.GetByIdAsync(id);

			if (ingredientDetails == null) return View("NotFound");
			return View(ingredientDetails);
		}

		[Authorize]
		public async Task<IActionResult> Edit(int id)
		{
			if (User.IsInRole("Admin"))
			{
				var ingredientDetails = await _service.GetByIdAsync(id);
				if (ingredientDetails == null) return View("NotFound");
				return View(ingredientDetails);
			}
			return RedirectToAction("AccessDenied", "Account");
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Edit(int id, [Bind("Id, Name, ImageUrl")] Ingredient ingredient)
		{
			if (!ModelState.IsValid)
			{
				return View(ingredient);
			}
			await _service.UpdateAsync(id, ingredient);
			return RedirectToAction(nameof(Index));
		}

		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			if (User.IsInRole("Admin"))
			{
				var ingredientDetails = await _service.GetByIdAsync(id);
				if (ingredientDetails == null) return View("NotFound");
				return View(ingredientDetails);
			}
			return RedirectToAction("AccessDenied", "Account");
		}

		[HttpPost, ActionName("Delete")]
		[Authorize]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var ingredientDetails = await _service.GetByIdAsync(id);
			if (ingredientDetails == null) return View("NotFound");

			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
