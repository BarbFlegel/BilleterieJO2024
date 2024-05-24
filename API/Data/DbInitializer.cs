using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data;

public static class DbInitializer
{
    public static async Task Initialize(StoreContext context, UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User
            {
                UserName = "bob",
                Email = "bob@test.com"
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Member");

            var admin = new User
            {
                UserName = "admin",
                Email = "admin@test.com"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Member" });
        }

        if (context.Products.Any()) return;

        var products = new List<Product>
            {
                new Product
                {
                    Name = "Solo Ticket",
                    Description =
                        "Solo Ticket for a one visitor for the Olympic Games 2024. Enjoy the Discovery Pass! Come and enjoy a unique experience by attending several sessions of different sports.",
                    Price = 50000,
                    PictureUrl = "/images/products/solo.png",
                    Brand = "Discovery Pass",
                    Type = "Solo",
                    QuantityInStock = 1000
                },
                new Product
                {
                    Name = "Duo Ticket",
                    Description =
                        "Duo Ticket for a two visitors to the Olympic Games 2024. Enjoy the Discovery Pass! Come and enjoy a unique experience by attending several sessions of different sports.",
                    Price = 100000,
                    PictureUrl = "/images/products/duo.png",
                    Brand = "Discovery Pass",
                    Type = "Duo",
                    QuantityInStock = 700
                },
                new Product
                {
                    Name = "Family Ticket",
                    Description =
                        "Family Ticket for a family to the Olympic Games 2024.Enjoy the Discovery Pass! Come and enjoy a unique experience by attending several sessions of different sports.",
                    Price = 120000,
                    PictureUrl = "/images/products/family.png",
                    Brand = "Discovery Pass",
                    Type = "Family",
                    QuantityInStock = 500
                },
                new Product
                {
                    Name = "Solo Ticket",
                    Description =
                        "Solo Ticket for a one visitor for the Olympic Games 2024. Enjoy the Standard Pass! Come and enjoy a unique experience by attending one session of chosen sport.",
                    Price = 20000,
                    PictureUrl = "/images/products/solo.png",
                    Brand = "Standard Pass",
                    Type = "Solo",
                    QuantityInStock = 1000
                },
                new Product
                {
                    Name = "Duo Ticket",
                    Description =
                        "Duo Ticket for a one visitor for the Olympic Games 2024. Enjoy the Standard Pass! Come and enjoy a unique experience by attending one session of chosen sport.",
                    Price = 40000,
                    PictureUrl = "/images/products/duo.png",
                    Brand = "Standard Pass",
                    Type = "Duo",
                    QuantityInStock = 700
                },
                new Product
                {
                    Name = "Family Ticket",
                    Description =
                        "Family Ticket for a one visitor for the Olympic Games 2024. Enjoy the Standard Pass! Come and enjoy a unique experience by attending one session of chosen sport.",
                    Price = 50000,
                    PictureUrl = "/images/products/family.png",
                    Brand = "Standard Pass",
                    Type = "Family",
                    QuantityInStock = 500
                },

            };

        foreach (var product in products)
        {
            context.Products.Add(product);
        }

        context.SaveChanges();
    }
}
