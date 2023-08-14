using HealthTea.Data.Constants;
using HealthTea.Data.Enums;
using HealthTea.Models;
using Microsoft.AspNetCore.Identity;

namespace HealthTea.Data
{
    public class SeedDB
	{
		public static void Seed(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

				context.Database.EnsureCreated();

				if (!context.Origins.Any())
				{
					context.Origins.AddRange(new List<Origin>()
					{
						new Origin()
						{
							Name = "China",
							ImageUrl = "https://res.cloudinary.com/dbmmtklps/image/upload/v1690641568/china_wgzivp.png",
						},
						new Origin()
						{
							Name = "Taiwan",
							ImageUrl = "https://res.cloudinary.com/dbmmtklps/image/upload/v1690641568/taiwan_zysquu.png",
						},
						new Origin()
						{
							Name = "Japan",
							ImageUrl = "https://res.cloudinary.com/dbmmtklps/image/upload/v1690641568/japan_qaybfj.png",
						},
						new Origin()
						{
							Name = "India",
							ImageUrl = "https://res.cloudinary.com/dbmmtklps/image/upload/v1690641568/india_dbj1qz.png",
						},
						new Origin()
						{
							Name = "Sri Lanka",
							ImageUrl = "https://res.cloudinary.com/dbmmtklps/image/upload/v1690641568/srilanka_qo0xsr.png",
						},
					});
					context.SaveChanges();
				}

				if (!context.Ingredients.Any())
				{
					context.Ingredients.AddRange(new List<Ingredient>()
					{
						new Ingredient()
						{
							Name = "Peppermint leaves",
							ImageUrl = "https://res.cloudinary.com/dbmmtklps/image/upload/v1690641767/peppermint_leaves_kj4so1.jpg",

						},
                        new Ingredient()
                        {
                            Name = "Orange",
                            ImageUrl = "https://www.allrecipes.com/thmb/y_uvjwXWAuD6T0RxaS19jFvZyFU=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/GettyImages-1205638014-2000-d0fbf9170f2d43eeb046f56eec65319c.jpg",

                        },
                        new Ingredient()
                        {
                            Name = "Blueberry",
                            ImageUrl = "https://encrypted-tbn1.gstatic.com/licensed-image?q=tbn:ANd9GcSLBRJfSJABUvT7tJeDy4RGOsuQb6Li-p-D-8zYZCyqtLRhgHtItinCALqqOp-sjx85b1nJqPyBx865SqU",

                        },
                        new Ingredient()
                        {
                            Name = "Ginseng",
                            ImageUrl = "https://i0.wp.com/images-prod.healthline.com/hlcmsresource/images/AN_images/ginseng-root-on-cutting-board-1296x728.jpg?w=1155&h=989",

                        },
                        new Ingredient()
                        {
                            Name = "Vanilla",
                            ImageUrl = "https://www.escoffier.edu/wp-content/uploads/vanilla-pods-and-beans-can-be-used-to-bring-fresh-vanilla-flavor-to-your_1028_40079119_1_14104505_500.jpg",

                        },
                        new Ingredient()
                        {
                            Name = "Honey",
                            ImageUrl = "https://i0.wp.com/post.healthline.com/wp-content/uploads/2021/04/honey-1296x728-header.jpg?w=1155&h=1528",

                        },
                        new Ingredient()
                        {
                            Name = "Lemon",
                            ImageUrl = "https://i0.wp.com/images-prod.healthline.com/hlcmsresource/images/AN_images/lemon-health-benefits-1296x728-feature.jpg?w=1155&h=1528",

                        },
                        new Ingredient()
                        {
                            Name = "Strawberry",
                            ImageUrl = "https://hips.hearstapps.com/clv.h-cdn.co/assets/15/22/1432664914-strawberry-facts1.jpg?resize=980:*",

                        }
                    });
					context.SaveChanges();
				}

				if (!context.Companies.Any())
				{
					context.Companies.AddRange(new List<Company>()
					{
						new Company
						{
							Name = "Lipton",
							ImageUrl = "https://res.cloudinary.com/dbmmtklps/image/upload/v1690641876/lipton_q8s1nf.jpg",
						},
						new Company {
                            Name = "Tetley",
                            ImageUrl = "https://nxtby.com/media/codazon_cache/brand/400x400/Brand-logo/Tetley.jpg",
                        },
                        new Company {
                            Name = "Tim Hortons",
                            ImageUrl = "https://fiu-original.b-cdn.net/fontsinuse.com/use-images/168/168941/168941.png?filename=Tim_Hortons_Logo.png",
                        },
                        new Company {
                            Name = "Red Rose",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/70/Red_Rose_Tea.jpg",
                        },
                        new Company {
                            Name = "Vahdam India",
                            ImageUrl = "https://cdn.shopify.com/s/files/1/1141/4138/files/logo_2ae4451b-2b26-43a0-a141-a1a2f1382048.png?v=1617252682",
                        },
                    });
					context.SaveChanges();
				}

				if (!context.Teas.Any())
				{
					context.Teas.AddRange(new List<Tea>()
					{
						new Tea()
						{
							Name = "Lipton Green Tea",
							Description = "Lipton Green Tea for a refreshing and uplifting sensation 100% Rainforest Alliance Certified 200 g 100 tea bags",
							Price = 9.50,
							ImageUrl = "https://res.cloudinary.com/dbmmtklps/image/upload/v1690642069/lipton_green_tea_k5blim.jpg",
							OriginId = 3,
							CompanyId = 1,
							TeaCategory = TeaCategory.Green,
							Status = true
						},
                        new Tea()
                        {
                            Name = "Lipton Yellow Label Tea",
                            Description = "Lipton Yellow Label Black Tea, Medium Caffeine, for an uniquely refreshing taste, 100% Rainforest Alliance Certified, 100 tea bags",
                            Price = 9.50,
                            ImageUrl = "https://m.media-amazon.com/images/I/71nFC0WWdUL._AC_SX679_.jpg",
                            OriginId = 4,
                            CompanyId = 1,
                            TeaCategory = TeaCategory.Yellow,
                            Status = true
                        },
                        new Tea()
                        {
                            Name = "Tetley Tea Strawberry",
                            Description = "Tetley Tea Strawberry Specialty Tea, 24-Count",
                            Price = 4.27,
                            ImageUrl = "https://m.media-amazon.com/images/I/517QpadTF4L._AC_SX679_.jpg",
                            OriginId = 2,
                            CompanyId = 2,
                            TeaCategory = TeaCategory.Earl_Grey,
                            Status = true
                        },
                        new Tea()
                        {
                            Name = "Tetley Tea Blueberry",
                            Description = "Tetley Tea Blueberry Green Tea, 24-Count",
                            Price = 5.36,
                            ImageUrl = "https://m.media-amazon.com/images/I/61W2cEV4+wL._AC_SX679_.jpg",
                            OriginId = 1,
                            CompanyId = 2,
                            TeaCategory = TeaCategory.Green,
                            Status = true
                        },
                        new Tea()
                        {
                            Name = "Tetley Orange Pekoe",
                            Description = "Tetley Orange Pekoe Black Tea - 72 Tea Bags, 227 Grams, Contains Caffeine",
                            Price = 5.01,
                            ImageUrl = "https://m.media-amazon.com/images/I/71HWNi28mvL._AC_SX679_.jpg",
                            OriginId = 1,
                            CompanyId = 2,
                            TeaCategory = TeaCategory.Black,
                            Status = true
                        },
                        new Tea()
                        {
                            Name = "Tim Hortons Orange Pekoe",
                            Description = "Tim Hortons Orange Pekoe Tea Bags, Classic Steeped Tea, 108ct Pack",
                            Price = 8.97,
                            ImageUrl = "https://m.media-amazon.com/images/I/71otWPdmdCL._AC_SX679_.jpg",
                            OriginId = 5,
                            CompanyId = 3,
                            TeaCategory = TeaCategory.Herbal,
                            Status = true
                        },
                        new Tea()
                        {
                            Name = "Red Rose Tea Bags",
                            Description = "Red RoseTea Bags, 100-Count",
                            Price = 19.99,
                            ImageUrl = "https://m.media-amazon.com/images/I/61G+wG+RDEL._AC_SX679_.jpg",
                            OriginId = 2,
                            CompanyId = 4,
                            TeaCategory = TeaCategory.White,
                            Status = true
                        },
                        new Tea()
                        {
                            Name = "VAHDAM, Organic Turmeric Ginger Tea",
                            Description = "VAHDAM, Organic Turmeric Ginger Herbal Tea (100 Tea Bags) | USDA Certified",
                            Price = 34.99,
                            ImageUrl = "https://m.media-amazon.com/images/I/710DZ+WiZOL._AC_SX679_.jpg",
                            OriginId = 3,
                            CompanyId = 4,
                            TeaCategory = TeaCategory.Herbal,
                            Status = true
                        },
                    });
					context.SaveChanges();
				}

				if (!context.Ingredients_Teas.Any())
				{
					context.Ingredients_Teas.AddRange(new List<Ingredient_Tea>()
					{
						new Ingredient_Tea()
						{
							IngredientId = 1,
							TeaId = 1
						},
                        new Ingredient_Tea()
                        {
                            IngredientId = 5,
                            TeaId = 2
                        },
                        new Ingredient_Tea()
                        {
                            IngredientId = 8,
                            TeaId = 3
                        },
                        new Ingredient_Tea()
                        {
                            IngredientId = 3,
                            TeaId = 4
                        },
                        new Ingredient_Tea()
                        {
                            IngredientId = 2,
                            TeaId = 5
                        },
                        new Ingredient_Tea()
                        {
                            IngredientId = 2,
                            TeaId = 6
                        },
                        new Ingredient_Tea()
                        {
                            IngredientId = 6,
                            TeaId = 7
                        },
                        new Ingredient_Tea()
                        {
                            IngredientId = 4,
                            TeaId = 8
                        },
                    });
					context.SaveChanges();
				}
			}
		}

		public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{

				var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

				if (!await roleManager.RoleExistsAsync(UserRoles.User))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
				string adminUserEmail = "admin@healthtea.com";

				var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
				if (adminUser == null)
				{
					var newAdmin = new ApplicationUser
					{
						FullName = "Admin",
						UserName = "admin",
						Email = adminUserEmail,
						EmailConfirmed = true
					};
					await userManager.CreateAsync(newAdmin, "HealthTea@123!");
					await userManager.AddToRoleAsync(newAdmin, UserRoles.Admin);
				}

				string userEmail = "john@doe.com";
				var appUser = await userManager.FindByEmailAsync(userEmail);
				if (appUser == null)
				{
					var newUser = new ApplicationUser
					{
						FullName = "John Doe",
						UserName = "john.doe",
						Email = userEmail,
						EmailConfirmed = true
					};
					await userManager.CreateAsync(newUser, "HealthTea@123!");
					await userManager.AddToRoleAsync(newUser, UserRoles.User);
				}
			}
		}
	}
}
