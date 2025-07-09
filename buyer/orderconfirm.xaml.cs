using Microsoft.Maui.Controls;
using FruitFarmers.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FruitFarmers
{
    public partial class OrderConfirmationPage : ContentPage
    {
        private readonly Order _order;

        public OrderConfirmationPage(Order order)
        {
            InitializeComponent();
            _order = order;
            DisplayOrderDetails();
        }

        // Constructor for testing/preview with sample data
        public OrderConfirmationPage()
        {
            InitializeComponent();
            
            // Create mock order data
            _order = new Order
            {
                Id = "ORD12345",
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                CustomerId = "CUST123",
                CustomerName = "John Doe",
                DeliveryAddress = "123 Main St, Anytown, CA 90210",
                PaymentMethod = "Credit Card",
                PaymentStatus = "Pending",
                TotalAmount = 35.97m,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = "PRD1",
                        ProductName = "Organic Apples",
                        FarmerId = "FRM1",
                        FarmerName = "Smith Family Farm",
                        UnitPrice = 3.99m,
                        Quantity = 3
                    },
                    new OrderItem
                    {
                        ProductId = "PRD2",
                        ProductName = "Fresh Strawberries",
                        FarmerId = "FRM2",
                        FarmerName = "Berry Good Farms",
                        UnitPrice = 4.99m,
                        Quantity = 2
                    },
                    new OrderItem
                    {
                        ProductId = "PRD3",
                        ProductName = "Organic Honey",
                        FarmerId = "FRM3",
                        FarmerName = "Honey Bee Farms",
                        UnitPrice = 7.99m,
                        Quantity = 1
                    }
                }
            };
            
            DisplayOrderDetails();
        }

        private void DisplayOrderDetails()
        {
            try
            {
                OrderNumberLabel.Text = $"Order #{_order.Id}";
                OrderDateLabel.Text = $"{_order.OrderDate:MMMM d, yyyy 'at' h:mm tt}";
                OrderStatusLabel.Text = $"Status: {_order.Status}";
                
                OrderItemsCollection.ItemsSource = _order.Items;
                
                decimal subtotal = 0;
                foreach (var item in _order.Items)
                {
                    subtotal += item.Subtotal;
                }
                
                decimal deliveryFee = 3.99m; // Hardcoded for now
                decimal total = subtotal + deliveryFee;
                
                SubtotalLabel.Text = $"${subtotal:F2}";
                DeliveryFeeLabel.Text = $"${deliveryFee:F2}";
                TotalLabel.Text = $"${total:F2}";
                
                DeliveryAddressLabel.Text = _order.DeliveryAddress;
                // In a real app, you'd have a phone field in the Order model
                DeliveryPhoneLabel.Text = "(555) 123-4567";
                
                PaymentMethodLabel.Text = _order.PaymentMethod;
                PaymentStatusLabel.Text = $"Payment Status: {_order.PaymentStatus}";
                
                // Calculate estimated delivery date (2 days from order)
                DateTime estimatedDelivery = _order.OrderDate.AddDays(2);
                EstimatedDeliveryLabel.Text = $"Estimated Delivery: {estimatedDelivery:MMMM d, yyyy}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error displaying order details: {ex.Message}");
                DisplayAlert("Error", "An error occurred while loading order details.", "OK");
            }
        }

        private async void OnTrackOrderClicked(object sender, EventArgs e)
        {
            try
            {
                // In a real app, this would navigate to an order tracking page
                await DisplayAlert("Feature Coming Soon", 
                    "Order tracking will be available in a future update.", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error navigating to order tracking: {ex.Message}");
                await DisplayAlert("Error", "An error occurred.", "OK");
            }
        }

        private async void OnContinueShoppingClicked(object sender, EventArgs e)
        {
            try
            {
                // Navigate back to home page
                await Navigation.PopToRootAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
                await DisplayAlert("Error", "An error occurred while navigating.", "OK");
            }
        }
    }
}

waaaah


using System.Diagnostics;
using FruitFarmers.ViewModels;

namespace FruitFarmers.Pages
{
    public partial class OrderConfirmationPage : ContentPage
    {
        private readonly OrderConfirmationViewModel _viewModel;
        
        public OrderConfirmationPage()
        {
            InitializeComponent();
            _viewModel = new OrderConfirmationViewModel();
            BindingContext = _viewModel;
        }
        
        private async void OnTrackOrderClicked(object sender, EventArgs e)
        {
            try
            {
                // In a real app, this would navigate to an order tracking page
                await DisplayAlert("Feature Coming Soon", 
                    "Order tracking will be available in a future update.", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error navigating to order tracking: {ex.Message}");
                await DisplayAlert("Error", "An error occurred.", "OK");
            }
        }
        
        private async void OnContinueShoppingClicked(object sender, EventArgs e)
        {
            try
            {
                // Navigate back to home page
                await Shell.Current.GoToAsync("//buyerDashboard");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
                await DisplayAlert("Error", "An error occurred while navigating.", "OK");
            }
        }
    }
}
