using System.ComponentModel.DataAnnotations;

namespace HealthTea.Data.Enums
{
    public enum TeaCategory
    {
        Herbal = 1,
        Mint,
        Green,
        Black,
        Yellow,
        White,
		[Display(Name = "Earl Grey")]
		Earl_Grey
    }
}
