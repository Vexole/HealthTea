using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthTea.Data
{
	public class ViewBagSelectList
	{
        public SelectList Origins { get; set; }
        public SelectList Companies { get; set; }
        public SelectList Ingredients { get; set; }
	}
}
