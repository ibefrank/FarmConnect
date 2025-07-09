
using System.Diagnostics;
using FruitFarmers.Models;
using FruitFarmers.ViewModels;

namespace FruitFarmers.Pages
{
    public partial class MarketplacePage : ContentPage
    {
        private readonly MarketplaceViewModel _viewModel;
        private string _category;
        private string _searchQuery;
        
        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                _viewModel.Category = value;
            }
        }
        
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                _viewModel.SearchQuery = value;
            }
        }
        
        public MarketplacePage()
        {
            InitializeComponent();
            _viewModel = new MarketplaceViewModel();
            BindingContext = _viewModel;
            
            Loaded += OnPageLoaded;
        }
        
        private async void OnPageLoaded(object sender, EventArgs e)
        {
            try
            {
                await _viewModel.LoadProductsAsync();
                ProductsCollection.ItemsSource = _viewModel.Products;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading marketplace products: {ex.Message}");
                await DisplayAlert("Error", "Failed to load products. Please check your connection and try again.", "OK");
            }
        }
        
        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            // Don't trigger search on every keystroke to avoid too many requests
            // In a real app, you'd implement debouncing here
        }
        
        private async void OnSearchButtonPressed(object sender, EventArgs e)
        {
            try
            {
                await _viewModel.SearchProductsAsync();
                ProductsCollection.ItemsSource = _viewModel.Products;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error searching products: {ex.Message}");
                await DisplayAlert("Error", "Failed to search products.", "OK");
            }
        }
        
        private async void OnFilterClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string filter = button?.CommandParameter?.ToString();
            
            // Reset all filter buttons
            foreach (var child in ((HorizontalStackLayout)((ScrollView)((StackLayout)button.Parent.Parent).Children[2]).Content).Children)
            {
                if (child is Button filterButton)
                {
                    filterButton.BackgroundColor = (Color)Application.Current.Resources["LightGray"];
                    filterButton.TextColor = Colors.Black;
                }
            }
            
            // Highlight selected filter
            button.BackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
            button.TextColor = Colors.White;
            
            // Apply filter to view model
            try
            {
                _viewModel.ApplyFilter(filter);
                await _viewModel.LoadProductsAsync();
                ProductsCollection.ItemsSource = _viewModel.Products;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error applying filter: {ex.Message}");
                await DisplayAlert("Error", "Failed to apply filter.", "OK");
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
        
        private async void OnAddToCartClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int productId = Convert.ToInt32(button?.CommandParameter);
            
            try
            {
                bool success = await _viewModel.AddToCartAsync(productId);
                if (success)
                {
                    await DisplayAlert("Success", "Product added to cart.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Failed to add product to cart.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding to cart: {ex.Message}");
                await DisplayAlert("Error", "Failed to add product to cart.", "OK");
            }
        }
        
        // Tab navigation methods
        private async void OnHomeTabClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("//buyerDashboard");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
                await DisplayAlert("Error", "Failed to navigate to home.", "OK");
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
