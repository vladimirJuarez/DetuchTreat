using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private DutchContext _ctx { get; }
        private readonly IWebHostEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext ctx, IWebHostEnvironment hosting, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }   

        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();
            var user = await _userManager.FindByEmailAsync("vlad@gmail.com");

            if(user == null)
            {
                user = new StoreUser{
                    FirstName = "Vlad",
                    LastName = "Juarez",
                    Email = "vlad@gmail.com",
                    UserName = "vlad@gmail.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");

                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }

            if(!_ctx.Products.Any())
            {
                var filePath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                //Need tocreate sample data
                 var json = File.ReadAllText(filePath);
                 var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                 _ctx.Products.AddRange(products);

                 var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();

                 if(order != null)
                 {
                     order.User = user;
                     order.Items = new List<OrderItem>()
                     {
                         new OrderItem()
                         {
                             Product = products.First(),
                             Quantity = 5,
                             UnitPrice = products.First().Price
                         }
                     };
                 }

                 _ctx.SaveChanges();
            }
        }
    }
}