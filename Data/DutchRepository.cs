using System;
using System.Collections.Generic;
using System.Linq;
using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        
        private readonly DutchContext _context;
        public ILogger<Product> _logger { get; }

        public DutchRepository(DutchContext context, ILogger<Product> logger)
        {
            _context = context;
            _logger = logger;
        }        

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Getting all products");
                return _context.Products
                        .OrderBy(P => P.Title)
                        .ToList();    
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products
                    .Where(P => P.Category == category)
                    .ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if(includeItems)
            {
                return _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(o => o.Product)
                    .ToList();
            }
            else
            {
                return _context.Orders.ToList();
            }
            
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefault(o => o.Id == id);
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }
    }
}