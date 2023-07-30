using HealthTea.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTea.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders
                .Include(t => t.OrderItems)
                .ThenInclude(t => t.Tea)
                .Include(t => t.User)
                .ToListAsync();

            if (userRole != "Admin")
            {
                orders = orders.Where(order => order.UserId == userId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<CartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            items.ForEach(async item =>
            {
                var orderItem = new OrderItem
                {
                    Quantity = item.Quantity,
                    TeaId = item.Tea.Id,
                    OrderId = order.Id,
                    Price = item.Tea.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            });

            await _context.SaveChangesAsync();
        }
    }
}
