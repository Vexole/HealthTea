using HealthTea.Data.Base;
using HealthTea.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTea.Models
{
    public class Tea : IBaseEntity
	{
		[Key]
		public int Id { get; set; }
		
		[Display(Name = "Image")]
        public string ImageUrl { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		public bool Status { get; set; }

		[Required]
		public double Price { get; set; }

		[Display(Name = "Tea Category")]
        public TeaCategory TeaCategory { get; set; }

        public IEnumerable<Ingredient_Tea> Ingredients_Teas { get; set; }

        public int OriginId { get; set; }

		[ForeignKey("OriginId")]
        public Origin Origin { get; set; }

		public int CompanyId { get; set; }

		[ForeignKey("CompanyId")]
		public Company Company { get; set; }
	}
}
