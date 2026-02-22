using System.Collections.Generic;
using UniversalInventoryManager.Console.Models;

namespace UniversalInventoryManager.Console.Services
{
    public interface IProductService
    {
        void AddProduct(Product product);
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void DeleteProduct(int id);
    }
}