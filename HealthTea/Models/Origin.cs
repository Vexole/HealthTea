using HealthTea.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace HealthTea.Models
{
	public class Origin : IEntityBase
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Image")]
        public string? ImageUrl { get; set; }

		[Required]
		public string? Name { get; set; }

        public IEnumerable<Tea>? Teas { get; set; }
    }
}
