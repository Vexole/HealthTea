using System.ComponentModel.DataAnnotations;

namespace HealthTea.Models
{
	public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public Tea Tea { get; set; }
        public int Quantity { get; set; }

        public string CartId { get; set; }
    }
}
