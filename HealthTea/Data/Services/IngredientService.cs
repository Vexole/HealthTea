using HealthTea.Data.Base;
using HealthTea.Models;

namespace HealthTea.Data.Services
{
    public class IngredientService : BaseEntityRepository<Ingredient>, IIngredientService
    {
        public IngredientService(AppDbContext context) : base(context) { }

    }
}
