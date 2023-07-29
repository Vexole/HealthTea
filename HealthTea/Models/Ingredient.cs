using System.ComponentModel.DataAnnotations;

namespace HealthTea.Models
{
	public class Ingredient
	{
        [Key]
        public int Id { get; set; }

        public string ImageUrl { get; set; }

		[Required]
		public string Name { get; set; }

        public IEnumerable<Ingredient_Tea> Ingredients_Teas { get; set; }
    }
}
