
using System.Collections.ObjectModel;
using System.Diagnostics;
using FruitFarmers.Models;
using System.Windows.Input;

namespace FruitFarmers.ViewModels
{
    public class MarketplaceViewModel : BaseViewModel
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        
        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }
        
        private string _category;
        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }
        
        private string _activeFilter = "all";
        
        public ICommand RefreshCommand { get; }
        
        public MarketplaceViewModel()
        {
            Title = "Marketplace";
            Products = new ObservableCollection<Product>();
            RefreshCommand = new Command(async () => await LoadProductsAsync());
        }
        
        public async Task LoadProductsAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            
            try
            {
                // In a real app, this would fetch data from a service
                // For now, we'll load mock data and filter based on category and active filter
                await Task.Delay(500); // Simulate network delay
                
                LoadMockProducts();
                
                // Apply category filter if specified
                if (!string.IsNullOrEmpty(Category))
                {
                    FilterByCategory();
                }
                
                // Apply active filter
                ApplyFilterLogic();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading marketplace products: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        public async Task SearchProductsAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            
            try
            {
                // In a real app, this would search products from a service
                // For now, we'll filter the mock data
                await Task.Delay(300); // Simulate network delay
                
                if (string.IsNullOrWhiteSpace(SearchQuery))
                {
                    await LoadProductsAsync();
                    return;
                }
                
                LoadMockProducts();
                
                // Filter by search query
                var searchResults = Products.Where(p => 
                    p.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    p.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    p.FarmerName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                
                Products.Clear();
                foreach (var product in searchResults)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error searching products: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        public void ApplyFilter(string filter)
        {
            _activeFilter = filter;
        }
        
        private void ApplyFilterLogic()
        {
            // Apply filter logic based on active filter
            var filteredProducts = Products.ToList();
            
            switch (_activeFilter)
            {
                case "seasonal":
                    // In a real app, this would filter seasonal products
                    // For now, just take a subset of products
                    filteredProducts = filteredProducts.Take(3).ToList();
                    break;
                case "nearby":
                    // In a real app, this would filter nearby products
                    // For now, just sort by location
                    filteredProducts = filteredProducts.OrderBy(p => p.Location).ToList();
                    break;
                case "price":
                    // Sort by price (low to high)
                    filteredProducts = filteredProducts.OrderBy(p => p.Price).ToList();
                    break;
                case "rated":
                    // In a real app, this would sort by rating
                    // For now, just sort by a random property
                    filteredProducts = filteredProducts.OrderByDescending(p => p.Id).ToList();
                    break;
                default:
                    // No specific filter, use the default list
                    break;
            }
            
            Products.Clear();
            foreach (var product in filteredProducts)
            {
                Products.Add(product);
            }
        }
        
        private void FilterByCategory()
        {
            var categoryProducts = Products.Where(p => 
                p.Category.Equals(Category, StringComparison.OrdinalIgnoreCase)
            ).ToList();
            
            Products.Clear();
            foreach (var product in categoryProducts)
            {
                Products.Add(product);
            }
        }
        
        public async Task<bool> AddToCartAsync(int productId)
        {
            if (IsBusy) return false;
            IsBusy = true;
            
            try
            {
                // In a real app, this would add the product to the cart via a service
                // For now, we'll just simulate success
                await Task.Delay(500); // Simulate network delay
                
                // Find the product in our collection
                var product = Products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    return false;
                }
                
                // Add to cart service call would go here
                
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding product to cart: {ex.Message}");
                return false;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        private void LoadMockProducts()
        {
            Products.Clear();
            
            Products.Add(new Product
            {
                Id = 1,
                Name = "Fresh Mangoes",
                Price = 120,
                ImageUrl = "mango.png",
                Description = "Sweet and juicy mangoes fresh from the farm",
                FarmerId = 1,
                FarmerName = "John Doe (Sunshine Farms)",
                Location = "Nairobi, Kenya",
                Category = "Tropical",
                HarvestDate = DateTime.Now.AddDays(-2)
            });
            
            Products.Add(new Product
            {
                Id = 2,
                Name = "Avocados",
                Price = 80,
                ImageUrl = "avocado.png",
                Description = "Creamy and fresh avocados",
                FarmerId = 2,
                FarmerName = "Jane Smith (Green Valley)",
                Location = "Nakuru, Kenya",
                Category = "Tropical",
                HarvestDate = DateTime.Now.AddDays(-1)
            });
            
            Products.Add(new Product
            {
                Id = 3,
                Name = "Bananas",
                Price = 50,
                ImageUrl = "banana.png",
                Description = "Sweet and ripe bananas",
                FarmerId = 1,
                FarmerName = "John Doe (Sunshine Farms)",
                Location = "Nairobi, Kenya",
                Category = "Tropical",
                HarvestDate = DateTime.Now.AddDays(-3)
            });
            
            Products.Add(new Product
            {
                Id = 4,
                Name = "Oranges",
                Price = 70,
                ImageUrl = "orange.png",
                Description = "Juicy and tangy oranges",
                FarmerId = 3,
                FarmerName = "Michael Brown (Fruitopia)",
                Location = "Mombasa, Kenya",
                Category = "Citrus",
                HarvestDate = DateTime.Now.AddDays(-2)
            });
            
            Products.Add(new Product
            {
                Id = 5,
                Name = "Strawberries",
                Price = 150,
                ImageUrl = "strawberry.png",
                Description = "Sweet and juicy strawberries",
                FarmerId = 2,
                FarmerName = "Jane Smith (Green Valley)",
                Location = "Nakuru, Kenya",
                Category = "Berries",
                HarvestDate = DateTime.Now.AddDays(-1)
            });
            
            Products.Add(new Product
            {
                Id = 6,
                Name = "Pineapples",
                Price = 100,
                ImageUrl = "pineapple.png",
                Description = "Sweet and tangy pineapples",
                FarmerId = 3,
                FarmerName = "Michael Brown (Fruitopia)",
                Location = "Mombasa, Kenya",
                Category = "Tropical",
                HarvestDate = DateTime.Now.AddDays(-4)
            });
            
            Products.Add(new Product
            {
                Id = 7,
                Name = "Peaches",
                Price = 120,
                ImageUrl = "peach.png",
                Description = "Sweet and juicy peaches",
                FarmerId = 1,
                FarmerName = "John Doe (Sunshine Farms)",
                Location = "Nairobi, Kenya",
                Category = "Stone Fruits",
                HarvestDate = DateTime.Now.AddDays(-2)
            });
        }
    }
}
