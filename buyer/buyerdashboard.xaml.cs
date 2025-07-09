
using System.Diagnostics;
using FruitFarmers.Models;
using FruitFarmers.ViewModels;

namespace FruitFarmers.Pages
{
    public partial class BuyerDashboard : ContentPage
    {
        private readonly BuyerDashboardViewModel _viewModel;
        
        public BuyerDashboard()
        {
            InitializeComponent();
            _viewModel = new BuyerDashboardViewModel();
            BindingContext = _viewModel;
            
            Loaded += OnPageLoaded;
        }
        
        private async void OnPageLoaded(object sender, EventArgs e)
        {
            try
            {
                await _viewModel.LoadDataAsync();
                RecommendedProductsCollection.ItemsSource = _viewModel.RecommendedProducts;
                FeaturedFarmsCollection.ItemsSource = _viewModel.FeaturedFarms;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading buyer dashboard data: {ex.Message}");
                await DisplayAlert("Error", "Failed to load data. Please check your connection and try again.", "OK");
            }
        }
        
        private async void OnSearchCompleted(object sender, EventArgs e)
        {
            string searchQuery = SearchEntry.Text;
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                try
                {
                    // Navigate to product listing with search query
                    await Shell.Current.GoToAsync($"productListing?search={searchQuery}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Navigation error: {ex.Message}");
                    await DisplayAlert("Error", "Failed to search products.", "OK");
                }
            }
        }
        
        private async void OnProductSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Product selectedProduct)
            {
                // Clear selection
                ((CollectionView)sender).SelectedItem = null;
                
                try
                {
                    // Navigate to product detail page
                    await Shell.Current.GoToAsync($"productDetail?id={selectedProduct.Id}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Navigation error: {ex.Message}");
                    await DisplayAlert("Error", "Failed to navigate to product detail.", "OK");
                }
            }
        }
        
        private async void OnFarmerSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Farmer selectedFarmer)
            {
                // Clear selection
                ((CollectionView)sender).SelectedItem = null;
                
                try
                {
                    // Navigate to farmer detail page
                    await Shell.Current.GoToAsync($"farmerDetail?id={selectedFarmer.Id}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Navigation error: {ex.Message}");
                    await DisplayAlert("Error", "Failed to navigate to farmer detail.", "OK");
                }
            }
        }
        
        private async void OnCategoryTapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is string category)
            {
                try
                {
                    await Shell.Current.GoToAsync($"productListing?category={category}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Navigation error: {ex.Message}");
                    await DisplayAlert("Error", "Failed to navigate to category page.", "OK");
                }
            }
        }
        
        private async void OnMarketplaceClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("marketplace");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
                await DisplayAlert("Error", "Failed to navigate to marketplace.", "OK");
            }
        }
        
        private async void OnOrdersClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("buyerOrders");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
                await DisplayAlert("Error", "Failed to navigate to orders.", "OK");
            }
        }
        
        // Tab navigation methods
        private async void OnMarketplaceTabClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("marketplace");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
                await DisplayAlert("Error", "Failed to navigate to marketplace.", "OK");
            }
        }
        
        private async void OnOrdersTabClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("buyerOrders");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
                await DisplayAlert("Error", "Failed to navigate to orders.", "OK");
            }
        }
        
        private async void OnMessagesTabClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("buyerMessages");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
                await DisplayAlert("Error", "Failed to navigate to messages.", "OK");
            }
        }
        
        private async void OnProfileTabClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("buyerProfile");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
                await DisplayAlert("Error", "Failed to navigate to profile.", "OK");
            }
        }
    }
}
