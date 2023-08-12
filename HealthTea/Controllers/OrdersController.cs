using HealthTea.Data.Services;
using HealthTea.Data.ShoppingCart;
using HealthTea.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthTea.Controllers
{
	[Authorize]
	public class OrdersController : Controller
	{
		private readonly Cart _cart;
		private readonly ITeaService _teasService;
		private readonly IOrdersService _ordersService;

		public OrdersController(Cart cart, ITeaService teasService, IOrdersService ordersService)
		{
			_cart = cart;
			_teasService = teasService;
			_ordersService = ordersService;
		}

		public async Task<IActionResult> Index()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
			string userRole = User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

			var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
			return View(orders);
		}

		public IActionResult Cart()
		{
			var items = _cart.GetCartItems();
			_cart.CartItems = items;

			var response = new CartViewModel()
			{
				Cart = _cart,
				CartTotal = _cart.GetCartTotal()
			};

			return View(response);
		}

		public async Task<IActionResult> AddItemToCart(int id)
		{
			var tea = await _teasService.GetTeaByIdAsync(id);

			if (tea != null)
			{
				_cart.AddItemToCart(tea);
			}
			return RedirectToAction(nameof(Cart));
		}

		public async Task<IActionResult> RemoveItemFromCart(int id)
		{
			var tea = await _teasService.GetTeaByIdAsync(id);

			if (tea != null)
			{
				_cart.RemoveItemFromCart(tea);
			}
			return RedirectToAction(nameof(Cart));
		}

		public async Task<IActionResult> CompleteOrder()
		{
			var items = _cart.GetCartItems();
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
			string userEmailAddress = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

			await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
			await _cart.ClearCartAsync();

			return View("OrderCompleted");
		}
	}
}
