
using System.Diagnostics;
using FruitFarmers.Models;
using FruitFarmers.ViewModels;

namespace FruitFarmers.Pages
{
    public partial class BuyerOrdersPage : ContentPage
    {
        private readonly BuyerOrdersViewModel _viewModel;
        private string _currentStatus = "Pending";
        
        public BuyerOrdersPage()
        {
            InitializeComponent();
            _viewModel = new BuyerOrdersViewModel();
            BindingContext = _viewModel;
            
            Loaded += OnPageLoaded;
        }
        
        private async void OnPageLoaded(object sender, EventArgs e)
        {
            try
            {
                await _viewModel.LoadOrdersAsync(_currentStatus);
                OrdersCollection.ItemsSource = _viewModel.Orders;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading orders: {ex.Message}");
                await DisplayAlert("Error", "Failed to load orders. Please check your connection and try again.", "OK");
            }
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            try
            {
                // Refresh orders every time the page appears
                await _viewModel.LoadOrdersAsync(_currentStatus);
                OrdersCollection.ItemsSource = _viewModel.Orders;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error refreshing orders: {ex.Message}");
            }
        }
        
        private void ResetTabButtons()
        {
            PendingButton.BackgroundColor = Colors.LightGray;
            PendingButton.TextColor = Colors.Black;
            
            ProcessingButton.BackgroundColor = Colors.LightGray;
            ProcessingButton.TextColor = Colors.Black;
            
            CompletedButton.BackgroundColor = Colors.LightGray;
            CompletedButton.TextColor = Colors.Black;
        }
        
        private async void OnPendingOrdersClicked(object sender, EventArgs e)
        {
            _currentStatus = "Pending";
            
            ResetTabButtons();
            PendingButton.BackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
            PendingButton.TextColor = Colors.White;
            
            try
            {
                await _viewModel.LoadOrdersAsync(_currentStatus);
                OrdersCollection.ItemsSource = _viewModel.Orders;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading pending orders: {ex.Message}");
                await DisplayAlert("Error", "Failed to load orders.", "OK");
            }
        }
        
        private async void OnProcessingOrdersClicked(object sender, EventArgs e)
        {
            _currentStatus = "Processing";
            
            ResetTabButtons();
            ProcessingButton.BackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
            ProcessingButton.TextColor = Colors.White;
            
            try
            {
                await _viewModel.LoadOrdersAsync(_currentStatus);
                OrdersCollection.ItemsSource = _viewModel.Orders;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading processing orders: {ex.Message}");
                await DisplayAlert("Error", "Failed to load orders.", "OK");
            }
        }
        
        private async void OnCompletedOrdersClicked(object sender, EventArgs e)
        {
            _currentStatus = "Completed";
            
            ResetTabButtons();
            CompletedButton.BackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
            CompletedButton.TextColor = Colors.White;
            
            try
            {
                await _viewModel.LoadOrdersAsync(_currentStatus);
                OrdersCollection.ItemsSource = _viewModel.Orders;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading completed orders: {ex.Message}");
                await DisplayAlert("Error", "Failed to load orders.", "OK");
            }
        }
        
        private void OnOrderSelected(object sender, SelectionChangedEventArgs e)
        {
            // Clear selection
            ((CollectionView)sender).SelectedItem = null;
        }
        
        private async void OnViewOrderDetailsClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int orderId = Convert.ToInt32(button?.CommandParameter);
            
            try
            {
                // Navigate to order details page
                await Shell.Current.GoToAsync($"orderDetails?id={orderId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
                await DisplayAlert("Error", "Failed to navigate to order details.", "OK");
            }
        }
        
        private async void OnCancelOrderClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int orderId = Convert.ToInt32(button?.CommandParameter);
            
            try
            {
                bool confirm = await DisplayAlert("Cancel Order", "Are you sure you want to cancel this order?", "Yes", "No");
                if (confirm)
                {
                    bool success = await _viewModel.CancelOrderAsync(orderId);
                    if (success)
                    {
                        await DisplayAlert("Success", "Order cancelled successfully.", "OK");
                        await _viewModel.LoadOrdersAsync(_currentStatus);
                        OrdersCollection.ItemsSource = _viewModel.Orders;
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to cancel order.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error cancelling order: {ex.Message}");
                await DisplayAlert("Error", "Failed to cancel order.", "OK");
            }
        }
        
        private async void OnBrowseProductsClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("//marketplace");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
                await DisplayAlert("Error", "Failed to navigate to marketplace.", "OK");
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
        
        private async void OnMarketplaceTabClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("//marketplace");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
                await DisplayAlert("Error", "Failed to navigate to marketplace.", "OK");
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
