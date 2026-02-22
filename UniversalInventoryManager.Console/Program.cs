using System;
using UniversalInventoryManager.Console.Data;
using UniversalInventoryManager.Console.Models;
using UniversalInventoryManager.Console.Services;

// Initialize Database Context
AppDbContext dbContext = new AppDbContext();


dbContext.Database.EnsureCreated();

// Inject the context into our manager
IProductService productService = new ProductManager(dbContext);

bool isRunning = true;

while (isRunning)
{
    Console.Clear();
    Console.WriteLine("=== Universal Inventory Manager ===");
    Console.WriteLine("1. Add New Tech Product");
    Console.WriteLine("2. Add New Clothing Product");
    Console.WriteLine("3. List All Products");
    Console.WriteLine("4. Exit");
    Console.Write("\nSelect an option (1-4): ");

    string choice = Console.ReadLine();
    Console.WriteLine();

    try
    {
        switch (choice)
        {
            case "1":
                Console.WriteLine("--- Add New Tech Product ---");
                TechProduct newTech = new TechProduct();

                // NOTICE: We removed the manual Id assignment. SQLite will handle it!

                Console.Write("Enter product name: ");
                newTech.Name = Console.ReadLine();

                Console.Write("Enter product price (e.g. 1500): ");
                decimal validTechPrice;
                while (!decimal.TryParse(Console.ReadLine(), out validTechPrice))
                {
                    Console.WriteLine("[ERROR] Invalid input. Price must be a number.");
                    Console.Write("Please enter product price again: ");
                }
                newTech.Price = validTechPrice;

                Console.Write("Does it have a battery? (y/n): ");
                string batteryInput = Console.ReadLine();
                newTech.HasBattery = (batteryInput?.ToLower() == "y");

                productService.AddProduct(newTech);

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                break;

            case "2":
                Console.WriteLine("--- Add New Clothing Product ---");
                ClothingProduct newClothing = new ClothingProduct();

                // NOTICE: We removed the manual Id assignment. SQLite will handle it!

                Console.Write("Enter product name: ");
                newClothing.Name = Console.ReadLine();

                Console.Write("Enter product price: ");
                decimal validClothingPrice;
                while (!decimal.TryParse(Console.ReadLine(), out validClothingPrice))
                {
                    Console.WriteLine("[ERROR] Invalid input. Price must be a number.");
                    Console.Write("Please enter product price again: ");
                }
                newClothing.Price = validClothingPrice;

                Console.Write("Enter size (S/M/L/XL): ");
                newClothing.Size = Console.ReadLine()?.ToUpper();

                productService.AddProduct(newClothing);

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                break;

            case "3":
                Console.WriteLine("--- Product List ---");
                var allProducts = productService.GetAllProducts();

                if (allProducts.Count == 0)
                {
                    Console.WriteLine("Inventory is currently empty.");
                }
                else
                {
                    foreach (var item in allProducts)
                    {
                        Console.WriteLine(item.GetInfo());
                        Console.WriteLine($"Calculated Tax: ${item.CalculateTax()}");
                        Console.WriteLine(new string('-', 20));
                    }
                }
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                break;

            case "4":
                isRunning = false;
                Console.WriteLine("Exiting application... Goodbye!");
                break;

            default:
                Console.WriteLine("Invalid option. Please select a number between 1 and 4.");
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n[CRITICAL ERROR] Something went wrong: {ex.Message}");
        // İç hatayı da görmek için:
        Console.WriteLine($"[DETAILS]: {ex.InnerException?.Message}");
        Console.WriteLine("Press any key to return to menu and try again...");
        Console.ReadKey();
    }
}