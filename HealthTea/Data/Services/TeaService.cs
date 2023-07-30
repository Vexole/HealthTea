using HealthTea.Data.Base;
using HealthTea.Data.ViewModels;
using HealthTea.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTea.Data.Services
{
	public class TeaService : BaseEntityRepository<Tea>, ITeaService
	{
		private AppDbContext _context;
		public TeaService(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task AddNewTeaAsync(TeaViewModel teaViewModel)
		{
			var tea = new Tea
			{
				Name = teaViewModel.Name,
				Description = teaViewModel.Description,
				Price = teaViewModel.Price,
				Status = teaViewModel.Status,
				ImageUrl = teaViewModel.ImageUrl,
				OriginId = teaViewModel.OriginId,
				CompanyId = teaViewModel.CompanyId,
				TeaCategory = teaViewModel.TeaCategory
			};
			await _context.Teas.AddAsync(tea);
			await _context.SaveChangesAsync();

			teaViewModel.IngredientIds.ToList().ForEach(async ingredientId =>
			{
				var teaIngredient = new Ingredient_Tea
				{
					TeaId = tea.Id,
					IngredientId = ingredientId
				};
				await _context.Ingredients_Teas.AddAsync(teaIngredient);
			});
			await _context.SaveChangesAsync();
		}

		public async Task UpdateTeaAsync(TeaViewModel teaViewModel)
		{
			var dbTea = await _context.Teas.FirstOrDefaultAsync(t => t.Id == teaViewModel.Id);

			if (dbTea != null)
			{
				dbTea.Id = teaViewModel.Id;
				dbTea.Name = teaViewModel.Name;
				dbTea.Description = teaViewModel.Description;
				dbTea.Price = teaViewModel.Price;
				dbTea.Status = teaViewModel.Status;
				dbTea.ImageUrl = teaViewModel.ImageUrl;
				dbTea.OriginId = teaViewModel.OriginId;
				dbTea.CompanyId = teaViewModel.CompanyId;
				dbTea.TeaCategory = teaViewModel.TeaCategory;
				await _context.SaveChangesAsync();
			};

			var currentIngredients = _context.Ingredients_Teas.Where(t => t.TeaId == teaViewModel.Id).ToList();
			_context.Ingredients_Teas.RemoveRange(currentIngredients);
			await _context.SaveChangesAsync();

			teaViewModel.IngredientIds.ToList().ForEach(async ingredientId =>
				{
					var teaIngredient = new Ingredient_Tea
					{
						TeaId = teaViewModel.Id,
						IngredientId = ingredientId
					};
					await _context.Ingredients_Teas.AddAsync(teaIngredient);
				});
			await _context.SaveChangesAsync();
		}

		public async Task<DropDownViewModel> GetDropDownValuesAsync()
		{
			var dropDownViewModel = new DropDownViewModel
			{
				Origins = await _context.Origins.OrderBy(t => t.Name).ToListAsync(),
				Companies = await _context.Companies.OrderBy(t => t.Name).ToListAsync(),
				Ingredients = await _context.Ingredients.OrderBy(t => t.Name).ToListAsync(),
			};

			return dropDownViewModel;
		}

		public async Task<Tea> GetTeaByIdAsync(int id)
		{
			var teaDetails = await _context.Teas
				.Include(t => t.Origin)
				.Include(t => t.Company)
				.Include(t => t.Ingredients_Teas)
				.ThenInclude(t => t.Ingredient)
				.FirstOrDefaultAsync(t => t.Id == id);

			return teaDetails;
		}
	}
}