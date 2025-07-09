
using System.Diagnostics;
using FruitFarmers.ViewModels;

namespace FruitFarmers.Pages
{
    public partial class BuyerCartPage : ContentPage
    {
        private readonly BuyerCartViewModel _viewModel;
        
        public BuyerCartPage()
        {
            InitializeComponent();
            _viewModel = new BuyerCartViewModel();
            BindingContext = _viewModel;
            
            Loaded += OnPageLoaded;
        }
        
        private async void OnPageLoaded(object sender, EventArgs e)
        {
            try
            {
                await _viewModel.LoadCartItemsAsync();
                CartItemsCollection.ItemsSource = _viewModel.CartItems;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading cart items: {ex.Message}");
                await DisplayAlert("Error", "Failed to load cart items. Please check your connection and try again.", "OK");
            }
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            try
            {
                // Refresh cart items every time the page appears
                await _viewModel.LoadCartItemsAsync();
                CartItemsCollection.ItemsSource = _viewModel.CartItems;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error refreshing cart items: {ex.Message}");
            }
        }
        
        private async void OnDecreaseQuantityClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int productId = Convert.ToInt32(button?.CommandParameter);
            
            try
            {
                await _viewModel.DecreaseQuantityAsync(productId);
                CartItemsCollection.ItemsSource = _viewModel.CartItems;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error decreasing quantity: {ex.Message}");
                await DisplayAlert("Error", "Failed to update quantity.", "OK");
            }
        }
        
        private async void OnIncreaseQuantityClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int productId = Convert.ToInt32(button?.CommandParameter);
            
            try
            {
                await _viewModel.IncreaseQuantityAsync(productId);
                CartItemsCollection.ItemsSource = _viewModel.CartItems;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error increasing quantity: {ex.Message}");
                await DisplayAlert("Error", "Failed to update quantity.", "OK");
            }
        }
        
        private async void OnRemoveItemClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int productId = Convert.ToInt32(button?.CommandParameter);
            
            try
            {
                bool confirm = await DisplayAlert("Remove Item", "Are you sure you want to remove this item from your cart?", "Yes", "No");
                if (confirm)
                {
                    await _viewModel.RemoveItemAsync(productId);
                    CartItemsCollection.ItemsSource = _viewModel.CartItems;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error removing item: {ex.Message}");
                await DisplayAlert("Error", "Failed to remove item from cart.", "OK");
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
        
        private void OnDeliveryOptionChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value && sender is RadioButton radioButton)
            {
                string option = radioButton.Value?.ToString();
                _viewModel.SetDeliveryOption(option);
            }
        }
        
        private void OnPaymentMethodChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value && sender is RadioButton radioButton)
            {
                string method = radioButton.Value?.ToString();
                _viewModel.SetPaymentMethod(method);
            }
        }
        
        private async void OnCheckoutClicked(object sender, EventArgs e)
        {
            try
            {
                bool confirm = await DisplayAlert("Confirm Order", 
                    $"Place your order for KSH {_viewModel.Total} using {_viewModel.PaymentMethod}?", 
                    "Place Order", "Cancel");
                
                if (confirm)
                {
                    bool success = await _viewModel.PlaceOrderAsync();
                    if (success)
                    {
                        // Navigate to order confirmation page
                        await Shell.Current.GoToAsync("orderConfirmation");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to place order. Please try again.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error checking out: {ex.Message}");
                await DisplayAlert("Error", "Failed to process checkout. Please try again later.", "OK");
            }
        }
    }
}
