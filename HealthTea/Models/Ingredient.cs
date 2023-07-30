using HealthTea.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace HealthTea.Models
{
	public class Ingredient : IBaseEntity
	{
        [Key]
        public int Id { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Ingredient image is required")]
        public string ImageUrl { get; set; }

		[Required(ErrorMessage="Ingredient name is required")]
		public string Name { get; set; }

        public IEnumerable<Ingredient_Tea>? Ingredients_Teas { get; set; }
    }
}
