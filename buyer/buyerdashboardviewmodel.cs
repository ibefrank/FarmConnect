
using System.Collections.ObjectModel;
using System.Diagnostics;
using FruitFarmers.Models;

namespace FruitFarmers.ViewModels
{
    public class BuyerDashboardViewModel : BaseViewModel
    {
        private ObservableCollection<Product> _recommendedProducts;
        public ObservableCollection<Product> RecommendedProducts
        {
            get => _recommendedProducts;
            set => SetProperty(ref _recommendedProducts, value);
        }

        private ObservableCollection<Farmer> _featuredFarms;
        public ObservableCollection<Farmer> FeaturedFarms
        {
            get => _featuredFarms;
            set => SetProperty(ref _featuredFarms, value);
        }

        public BuyerDashboardViewModel()
        {
            RecommendedProducts = new ObservableCollection<Product>();
            FeaturedFarms = new ObservableCollection<Farmer>();
        }

        public async Task LoadDataAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                // In a real app, this would fetch data from a service
                // For now, we'll load mock data
                LoadMockRecommendedProducts();
                LoadMockFeaturedFarms();
                
                await Task.Delay(500); // Simulate network delay
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading buyer dashboard data: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void LoadMockRecommendedProducts()
        {
            RecommendedProducts.Clear();
            
            RecommendedProducts.Add(new Product
            {
                Id = 1,
                Name = "Fresh Mangoes",
                Price = 120,
                ImageUrl = "mango.png",
                Description = "Sweet and juicy mangoes fresh from the farm",
                FarmerId = 1
            });
            
            RecommendedProducts.Add(new Product
            {
                Id = 2,
                Name = "Avocados",
                Price = 80,
                ImageUrl = "avocado.png",
                Description = "Creamy and fresh avocados",
                FarmerId = 2
            });
            
            RecommendedProducts.Add(new Product
            {
                Id = 3,
                Name = "Bananas",
                Price = 50,
                ImageUrl = "banana.png",
                Description = "Sweet and ripe bananas",
                FarmerId = 1
            });
            
            RecommendedProducts.Add(new Product
            {
                Id = 4,
                Name = "Oranges",
                Price = 70,
                ImageUrl = "orange.png",
                Description = "Juicy and tangy oranges",
                FarmerId = 3
            });
        }

        private void LoadMockFeaturedFarms()
        {
            FeaturedFarms.Clear();
            
            FeaturedFarms.Add(new Farmer
            {
                Id = 1,
                Name = "John Doe",
                FarmName = "Sunshine Farms",
                Location = "Nairobi, Kenya",
                ImageUrl = "farm1.png",
                Rating = 4.8
            });
            
            FeaturedFarms.Add(new Farmer
            {
                Id = 2,
                Name = "Jane Smith",
                FarmName = "Green Valley",
                Location = "Nakuru, Kenya",
                ImageUrl = "farm2.png",
                Rating = 4.5
            });
            
            FeaturedFarms.Add(new Farmer
            {
                Id = 3,
                Name = "Michael Brown",
                FarmName = "Fruitopia",
                Location = "Mombasa, Kenya",
                ImageUrl = "farm3.png",
                Rating = 4.7
            });
        }
    }
}
