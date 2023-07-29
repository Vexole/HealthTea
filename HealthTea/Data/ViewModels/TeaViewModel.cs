using HealthTea.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HealthTea.Models
{
    public class TeaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Tea Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Tea Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Tea Image")]
        [Required(ErrorMessage = "Tea image url is required")]
        public string ImageUrl { get; set; }

        [Display(Name = "In Stock?")]
        public bool Status { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Tea category is required")]
        public TeaCategory TeaCategory { get; set; }

        [Display(Name = "Select ingredient(s)")]
        [Required(ErrorMessage = "Tea ingredient(s) is required")]
        public IEnumerable<int> IngredientIds { get; set; }

        [Display(Name = "Select a origin")]
        [Required(ErrorMessage = "Tea origin is required")]
        public int OriginId { get; set; }

        [Display(Name = "Select a company")]
        [Required(ErrorMessage = "Tea company is required")]
        public int CompanyId { get; set; }
    }
}
