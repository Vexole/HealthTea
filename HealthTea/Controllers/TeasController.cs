using HealthTea.Data;
using HealthTea.Data.Services;
using HealthTea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthTea.Controllers
{
	public class TeasController : Controller
	{
		private readonly ITeaService _service;
		private readonly string ID = "Id";
		private readonly string NAME = "Name";

		public TeasController(ITeaService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			var teas = await _service.GetAllAsync(t => t.Origin, t => t.Company);
			return View(teas);
		}

		[Authorize]
		public async Task<IActionResult> Create()
		{
			if (User.IsInRole("Admin"))
			{
				var dropDownData = await GetDropDownData();
				ViewBag.Origins = dropDownData.Origins;
				ViewBag.Companies = dropDownData.Companies;
				ViewBag.Ingredients = dropDownData.Ingredients;

				return View();
			}
			return RedirectToAction("AccessDenied", "Account");
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create(TeaViewModel teaViewModel)
		{
			if (!ModelState.IsValid)
			{
				var dropDownData = await GetDropDownData();
				ViewBag.Origins = dropDownData.Origins;
				ViewBag.Companies = dropDownData.Companies;
				ViewBag.Ingredients = dropDownData.Ingredients;

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

		[Authorize]
		public async Task<IActionResult> Edit(int id)
		{
			if (User.IsInRole("Admin"))
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

				var dropDownData = await GetDropDownData();
				ViewBag.Origins = dropDownData.Origins;
				ViewBag.Companies = dropDownData.Companies;
				ViewBag.Ingredients = dropDownData.Ingredients;

				return View(teaViewModel);
			}
			return RedirectToAction("AccessDenied", "Account");
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Edit(int id, TeaViewModel teaViewModel)
		{
			if (id != teaViewModel.Id) return View("NotFound");

			if (!ModelState.IsValid)
			{
				var dropDownData = await GetDropDownData();
				ViewBag.Origins = dropDownData.Origins;
				ViewBag.Companies = dropDownData.Companies;
				ViewBag.Ingredients = dropDownData.Ingredients;

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

		private async Task<ViewBagSelectList> GetDropDownData()
		{
			var dropDownValues = await _service.GetDropDownValuesAsync();
			var origins = new SelectList(dropDownValues.Origins, ID, NAME);
			var companies = new SelectList(dropDownValues.Companies, ID, NAME);
			var ingredients = new SelectList(dropDownValues.Ingredients, ID, NAME);

			return new ViewBagSelectList { Origins = origins, Companies = companies, Ingredients = ingredients };
		}
	}
}
