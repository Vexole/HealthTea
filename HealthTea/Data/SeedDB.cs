using HealthTea.Data.Enums;
using HealthTea.Data.Static;
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

				// Origin
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

						}
					});
					context.SaveChanges();
				}

				if (!context.Companies.Any())
				{
					context.Companies.AddRange(new List<Company>()
					{
						new Company()
						{
							Name = "Lipton",
							ImageUrl = "https://res.cloudinary.com/dbmmtklps/image/upload/v1690641876/lipton_q8s1nf.jpg",
						}
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
						}
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
						}
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
					await userManager.CreateAsync(newAdmin, "Coding@1234?");
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
					await userManager.CreateAsync(newUser, "Coding@1234?");
					await userManager.AddToRoleAsync(newUser, UserRoles.User);
				}
			}
		}
	}
}
