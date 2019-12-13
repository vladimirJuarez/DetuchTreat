using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
        void AddEntity(object model);
        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(int id);
    } 
}