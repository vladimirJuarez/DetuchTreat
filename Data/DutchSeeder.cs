using System.Collections.Generic;
using System.IO;
using System.Linq;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private DutchContext _ctx { get; }
        private readonly IHostingEnvironment _hosting;
        public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }   

        public void Seed()
        {
            _ctx.Database.EnsureCreated();
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