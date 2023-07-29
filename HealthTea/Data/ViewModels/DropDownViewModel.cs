using HealthTea.Models;

namespace HealthTea.Data.ViewModels
{
	public class DropDownViewModel
	{
        public DropDownViewModel()
        {
            Companies = new List<Company>();
            Origins = new List<Origin>();
            Ingredients = new List<Ingredient>();
        }
        public IEnumerable<Company> Companies { get; set; }
        public IEnumerable<Origin> Origins { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
	}
}
