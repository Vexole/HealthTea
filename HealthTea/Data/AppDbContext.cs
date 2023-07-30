using HealthTea.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthTea.Data
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
        public DbSet<Company> Companies { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Origin> Origins { get; set; }
        public DbSet<Tea> Teas { get; set; }
		public DbSet<Ingredient_Tea> Ingredients_Teas { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Ingredient_Tea>().HasKey(it => new
			{
				it.IngredientId,
				it.TeaId
			});

			modelBuilder.Entity<Ingredient_Tea>().HasOne(t => t.Tea)
				.WithMany(it => it.Ingredients_Teas)
				.HasForeignKey(t => t.TeaId);

			modelBuilder.Entity<Ingredient_Tea>().HasOne(i => i.Ingredient)
				.WithMany(it => it.Ingredients_Teas)
				.HasForeignKey(i => i.IngredientId);

			base.OnModelCreating(modelBuilder);
		}
	}
}
