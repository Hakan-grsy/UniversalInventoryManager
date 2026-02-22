using System;
using System.Collections.Generic;
using System.Linq;
using UniversalInventoryManager.Console.Data;
using UniversalInventoryManager.Console.Models;

namespace UniversalInventoryManager.Console.Services
{
    public class ProductManager : IProductService
    {
        private readonly AppDbContext _context;

        // Constructor Injection (Best Practice for Dependency Injection)
        // We inject the database context into our service when it's created.
        public ProductManager(AppDbContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            // 1. Add the product to the EF Core tracking system
            _context.Products.Add(product);

            // 2. Actually execute the SQL INSERT command and save to the file
            _context.SaveChanges();

            System.Console.WriteLine($"[SYSTEM LOG] Product successfully saved to SQLite DB: {product.Name}");
        }

        public List<Product> GetAllProducts()
        {
            // Execute SQL SELECT * FROM Products and return as a List
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            // Execute SQL SELECT * FROM Products WHERE Id = @id LIMIT 1
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }
        public void DeleteProduct(int id)
        {
            // 1. Find the product in the database
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                // 2. Remove it from tracking
                _context.Products.Remove(product);

                // 3. Save changes to the SQLite file
                _context.SaveChanges();

                System.Console.WriteLine($"[SYSTEM LOG] Product deleted: ID {id}");
            }
        }
    }
}