using HealthTea.Data.ShoppingCart;
using Microsoft.AspNetCore.Mvc;

namespace HealthTea.Data.ViewComponents
{
    public class CartSummary : ViewComponent
    {
        private readonly Cart _cart;
        public CartSummary(Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View(0);
            }
                var items = _cart.GetCartItems();
            var quantity = items.Sum(x => x.Quantity);
            return View(quantity);
        }
    }
}
