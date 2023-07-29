using System.ComponentModel.DataAnnotations;

namespace HealthTea.Models
{
	public class Origin
	{
		[Key]
		public int Id { get; set; }

		public string? ImageUrl { get; set; }

		[Required]
		public string? Name { get; set; }

        public IEnumerable<Tea> Teas { get; set; }
    }
}
