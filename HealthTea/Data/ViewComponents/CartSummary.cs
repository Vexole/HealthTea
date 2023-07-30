﻿using HealthTea.Data.ShoppingCart;
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
            var items = _cart.GetCartItems();

            return View(items.Count);
        }
    }
}