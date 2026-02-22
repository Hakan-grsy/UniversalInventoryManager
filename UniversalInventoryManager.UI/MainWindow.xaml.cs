using System;
using System.Windows;
using UniversalInventoryManager.Console.Data;
using UniversalInventoryManager.Console.Models;
using UniversalInventoryManager.Console.Services;

namespace UniversalInventoryManager.UI
{
    public partial class MainWindow : Window
    {
        // Interface for our business logic layer
        private readonly IProductService _productService;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize EF Core Database Context
            AppDbContext dbContext = new AppDbContext();

            // Ensure the SQLite database file is created and ready
            dbContext.Database.EnsureCreated();

            // Inject the context into our manager
            _productService = new ProductManager(dbContext);

            // Fetch data from database and populate the DataGrid on startup
            RefreshGrid();
        }

        // Refreshes the DataGrid by pulling the latest data from the database
        private void RefreshGrid()
        {
            DgProducts.ItemsSource = null;
            DgProducts.ItemsSource = _productService.GetAllProducts();
        }

        // Handles the radio button changes to show/hide specific property panels dynamically
        private void Category_Checked(object sender, RoutedEventArgs e)
        {
            // Safety check: Ensure panels are loaded before trying to change visibility
            if (PanelTech == null || PanelClothing == null) return;

            if (RbTech.IsChecked == true)
            {
                PanelTech.Visibility = Visibility.Visible;
                PanelClothing.Visibility = Visibility.Collapsed;
            }
            else if (RbClothing.IsChecked == true)
            {
                PanelTech.Visibility = Visibility.Collapsed;
                PanelClothing.Visibility = Visibility.Visible;
            }
        }

        // Universal add button that checks which category is selected and creates the proper object
        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Retrieve common data
                string name = TxtName.Text;
                decimal price = decimal.Parse(TxtPrice.Text);

                if (RbTech.IsChecked == true)
                {
                    // Create and save a Tech Product
                    TechProduct newTech = new TechProduct
                    {
                        Name = name,
                        Price = price,
                        HasBattery = ChkBattery.IsChecked ?? false
                    };
                    _productService.AddProduct(newTech);
                }
                else
                {
                    // Create and save a Clothing Product
                    ClothingProduct newClothing = new ClothingProduct
                    {
                        Name = name,
                        Price = price,
                        Size = TxtSize.Text
                    };
                    _productService.AddProduct(newClothing);
                }

                MessageBox.Show("Product successfully added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                RefreshGrid();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Invalid input. Please check your data formatting.\n\nError: {ex.Message}", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Deletes the selected product from the DataGrid and Database
        private void BtnDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            // Check if the user actually clicked on a row in the table
            if (DgProducts.SelectedItem is Product selectedProduct)
            {
                // Ask for confirmation
                MessageBoxResult result = MessageBox.Show(
                    $"Are you sure you want to delete '{selectedProduct.Name}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Delete from Database
                        _productService.DeleteProduct(selectedProduct.Id);

                        // Refresh UI
                        RefreshGrid();

                        MessageBox.Show("Product deleted successfully.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                // User clicked delete without selecting a row
                MessageBox.Show("Please select a product from the list first.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Resets all UI fields after a successful addition
        private void ClearInputs()
        {
            TxtName.Clear();
            TxtPrice.Clear();

            // Null checks added for safety during initial load
            if (TxtSize != null) TxtSize.Clear();
            if (ChkBattery != null) ChkBattery.IsChecked = false;
            if (RbTech != null) RbTech.IsChecked = true; // Reset back to Tech category by default
        }
    }
}