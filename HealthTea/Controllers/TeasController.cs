using HealthTea.Data;
using HealthTea.Data.Services;
using HealthTea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HealthTea.Controllers
{
	public class TeasController : Controller
	{
		private readonly ITeaService _service;

		public TeasController(ITeaService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			var teas = await _service.GetAllAsync(t => t.Origin, t => t.Company);
			return View(teas);
		}

		public async Task<IActionResult> Create()
		{
			var dropDownValues = await _service.GetDropDownValuesAsync();
			ViewBag.Origins = new SelectList(dropDownValues.Origins, "Id", "Name");
			ViewBag.Companies = new SelectList(dropDownValues.Companies, "Id", "Name");
			ViewBag.Ingredients = new SelectList(dropDownValues.Ingredients, "Id", "Name");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(TeaViewModel teaViewModel)
		{
			if (!ModelState.IsValid)
			{
				var dropDownValues = await _service.GetDropDownValuesAsync();
				ViewBag.Origins = new SelectList(dropDownValues.Origins, "Id", "Name");
				ViewBag.Companies = new SelectList(dropDownValues.Companies, "Id", "Name");
				ViewBag.Ingredients = new SelectList(dropDownValues.Ingredients, "Id", "Name");
				return View(teaViewModel);
			}
			await _service.AddNewTeaAsync(teaViewModel);
			return RedirectToAction(nameof(Index));
		}

		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var teaDetails = await _service.GetTeaByIdAsync(id);

			if (teaDetails == null) return View("NotFound");
			return View(teaDetails);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var teaDetails = await _service.GetTeaByIdAsync(id);
			if (teaDetails == null) return View("NotFound");

			var teaViewModel = new TeaViewModel
			{
				Id = teaDetails.Id,
				Name = teaDetails.Name,
				Description = teaDetails.Description,
				Price = teaDetails.Price,
				ImageUrl = teaDetails.ImageUrl,
				TeaCategory = teaDetails.TeaCategory,
				OriginId = teaDetails.OriginId,
				CompanyId = teaDetails.CompanyId,
				Status = teaDetails.Status,
				IngredientIds = teaDetails.Ingredients_Teas.Select(x => x.IngredientId).ToList(),
			};

			var dropDownValues = await _service.GetDropDownValuesAsync();
			ViewBag.Origins = new SelectList(dropDownValues.Origins, "Id", "Name");
			ViewBag.Companies = new SelectList(dropDownValues.Companies, "Id", "Name");
			ViewBag.Ingredients = new SelectList(dropDownValues.Ingredients, "Id", "Name");

			return View(teaViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, TeaViewModel teaViewModel)
		{
			if (id != teaViewModel.Id) return View("NotFound");

			if (!ModelState.IsValid)
			{
				var dropDownValues = await _service.GetDropDownValuesAsync();
				ViewBag.Origins = new SelectList(dropDownValues.Origins, "Id", "Name");
				ViewBag.Companies = new SelectList(dropDownValues.Companies, "Id", "Name");
				ViewBag.Ingredients = new SelectList(dropDownValues.Ingredients, "Id", "Name");
				return View(teaViewModel);
			}
			await _service.UpdateTeaAsync(teaViewModel);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Filter(string searchString)
		{
			var teas = await _service.GetAllAsync(t => t.Origin, t => t.Company);
			if (!string.IsNullOrEmpty(searchString))
			{
				var filteredTeas = teas.Where(tea => tea.Name.ToLower().Contains(searchString.ToLower()) 
					|| tea.Description.ToLower().Contains(searchString.ToLower())).ToList();
				return View("Index", filteredTeas);
			}
			return View("Index", teas);
		}
	}
}
