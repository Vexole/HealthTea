using HealthTea.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTea.Data.ShoppingCart
{
	public class Cart
	{
		private readonly AppDbContext _context;
		public string CartId { get; set; }
		public List<CartItem> CartItems { get; set; }

		public Cart(AppDbContext context)
		{
			_context = context;
		}

		public static Cart GetCart(IServiceProvider services)
		{
			ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			var context = services.GetService<AppDbContext>();

			string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
			session.SetString("CartId", cartId);

			return new Cart(context) { CartId = cartId };
		}

		public void AddItemToCart(Tea tea)
		{
			var cartItem = _context.CartItems
				.FirstOrDefault(t => t.Tea.Id == tea.Id && t.CartId == CartId);

			if (cartItem == null)
			{
				cartItem = new CartItem
				{
					CartId = CartId,
					Tea = tea,
					Quantity = 1
				};

				_context.CartItems.Add(cartItem);
			}
			else
			{
				cartItem.Quantity++;
			}
			_context.SaveChanges();
		}

		public void RemoveItemFromCart(Tea tea)
		{
			var cartItem = _context.CartItems
				.FirstOrDefault(t => t.Tea.Id == tea.Id && t.CartId == CartId);

			if (cartItem != null)
			{
				if (cartItem.Quantity > 1)
				{
					cartItem.Quantity--;
				}
				else
				{
					_context.CartItems.Remove(cartItem);
				}
			}
			_context.SaveChanges();
		}

		public List<CartItem> GetCartItems()
		{
			return CartItems ?? (CartItems = _context.CartItems
				.Where(t => t.CartId == CartId)
				.Include(t => t.Tea)
				.ToList());
		}

		public double GetCartTotal() => _context.CartItems
			.Where(t => t.CartId == CartId)
			.Select(t => t.Tea.Price * t.Quantity)
			.Sum();

		public async Task ClearCartAsync()
		{
			var items = await _context.CartItems
				.Where(t => t.CartId == CartId)
				.ToListAsync();
			_context.CartItems.RemoveRange(items);
			await _context.SaveChangesAsync();
		}
	}
}
