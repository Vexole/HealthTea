using System.ComponentModel.DataAnnotations;

namespace HealthTea.Models
{
	public class Ingredient_Tea
	{
        [Key]
        public int Id { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public int TeaId { get; set; }
        public Tea Tea { get; set; }
    }
}
