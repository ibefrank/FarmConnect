
using System.Collections.ObjectModel;
using System.Diagnostics;
using FruitFarmers.Models;

namespace FruitFarmers.ViewModels
{
    public class BuyerCartViewModel : BaseViewModel
    {
        private ObservableCollection<CartItem> _cartItems;
        public ObservableCollection<CartItem> CartItems
        {
            get => _cartItems;
            set => SetProperty(ref _cartItems, value);
        }
        
        private int _cartItemCount;
        public int CartItemCount
        {
            get => _cartItemCount;
            set => SetProperty(ref _cartItemCount, value);
        }
        
        private decimal _subtotal;
        public decimal Subtotal
        {
            get => _subtotal;
            set => SetProperty(ref _subtotal, value);
        }
        
        private decimal _deliveryFee;
        public decimal DeliveryFee
        {
            get => _deliveryFee;
            set => SetProperty(ref _deliveryFee, value);
        }
        
        private decimal _total;
        public decimal Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }
        
        private string _deliveryOption = "HomeDelivery";
        public string DeliveryOption
        {
            get => _deliveryOption;
            set => SetProperty(ref _deliveryOption, value);
        }
        
        private string _paymentMethod = "MPesa";
        public string PaymentMethod
        {
            get => _paymentMethod;
            set => SetProperty(ref _paymentMethod, value);
        }
        
        private bool _hasItems;
        public bool HasItems
        {
            get => _hasItems;
            set
            {
                SetProperty(ref _hasItems, value);
                OnPropertyChanged(nameof(HasNoItems));
            }
        }
        
        public bool HasNoItems => !HasItems;
        
        public BuyerCartViewModel()
        {
            Title = "Shopping Cart";
            CartItems = new ObservableCollection<CartItem>();
            DeliveryFee = 50; // Default delivery fee
        }
        
        public async Task LoadCartItemsAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            
            try
            {
                // In a real app, this would fetch cart items from a service
                // For now, we'll load mock data
                await Task.Delay(300); // Simulate network delay
                
                LoadMockCartItems();
                UpdateCartTotals();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading cart items: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        public async Task IncreaseQuantityAsync(int productId)
        {
            if (IsBusy) return;
            IsBusy = true;
            
            try
            {
                var item = CartItems.FirstOrDefault(i => i.Product.Id == productId);
                if (item != null)
                {
                    item.Quantity++;
                    item.UpdateItemTotal();
                    UpdateCartTotals();
                }
                
                await Task.Delay(100); // Small delay for UI response
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error increasing quantity: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        public async Task DecreaseQuantityAsync(int productId)
        {
            if (IsBusy) return;
            IsBusy = true;
            
            try
            {
                var item = CartItems.FirstOrDefault(i => i.Product.Id == productId);
                if (item != null && item.Quantity > 1)
                {
                    item.Quantity--;
                    item.UpdateItemTotal();
                    UpdateCartTotals();
                }
                else if (item != null && item.Quantity == 1)
                {
                    // If quantity would go to 0, remove the item instead
                    await RemoveItemAsync(productId);
                }
                
                await Task.Delay(100); // Small delay for UI response
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error decreasing quantity: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        public async Task RemoveItemAsync(int productId)
        {
            if (IsBusy) return;
            IsBusy = true;
            
            try
            {
                var item = CartItems.FirstOrDefault(i => i.Product.Id == productId);
                if (item != null)
                {
                    CartItems.Remove(item);
                    UpdateCartTotals();
                }
                
                await Task.Delay(100); // Small delay for UI response
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error removing item: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        public void SetDeliveryOption(string option)
        {
            DeliveryOption = option;
            
            // Update delivery fee based on option
            if (option == "HomeDelivery")
            {
                DeliveryFee = 50;
            }
            else // Pickup
            {
                DeliveryFee = 0;
            }
            
            UpdateCartTotals();
        }
        
        public void SetPaymentMethod(string method)
        {
            PaymentMethod = method;
        }
        
        public async Task<bool> PlaceOrderAsync()
        {
            if (IsBusy) return false;
            IsBusy = true;
            
            try
            {
                // In a real app, this would place the order via a service
                // For now, we'll just simulate success
                await Task.Delay(1000); // Simulate network delay
                
                // Clear cart after successful order
                CartItems.Clear();
                UpdateCartTotals();
                
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error placing order: {ex.Message}");
                return false;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        private void UpdateCartTotals()
        {
            CartItemCount = CartItems.Count;
            Subtotal = CartItems.Sum(item => item.ItemTotal);
            Total = Subtotal + DeliveryFee;
            HasItems = CartItems.Count > 0;
        }
        
        private void LoadMockCartItems()
        {
            // Clear existing items
            CartItems.Clear();
            
            // Add mock items - in a real app these would come from storage or API
            CartItems.Add(new CartItem
            {
                Product = new Product
                {
                    Id = 1,
                    Name = "Fresh Mangoes",
                    Price = 120,
                    ImageUrl = "mango.png",
                    Description = "Sweet and juicy mangoes fresh from the farm",
                    FarmerId = 1,
                    FarmerName = "John Doe (Sunshine Farms)",
                    Location = "Nairobi, Kenya"
                },
                Quantity = 2
            });
            
            CartItems.Add(new CartItem
            {
                Product = new Product
                {
                    Id = 4,
                    Name = "Oranges",
                    Price = 70,
                    ImageUrl = "orange.png",
                    Description = "Juicy and tangy oranges",
                    FarmerId = 3,
                    FarmerName = "Michael Brown (Fruitopia)",
                    Location = "Mombasa, Kenya"
                },
                Quantity = 3
            });
            
            // Update item totals
            foreach (var item in CartItems)
            {
                item.UpdateItemTotal();
            }
        }
    }
    
    public class CartItem : ObservableObject
    {
        private Product _product;
        public Product Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }
        
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                SetProperty(ref _quantity, value);
                UpdateItemTotal();
            }
        }
        
        private decimal _itemTotal;
        public decimal ItemTotal
        {
            get => _itemTotal;
            set => SetProperty(ref _itemTotal, value);
        }
        
        public void UpdateItemTotal()
        {
            ItemTotal = Product.Price * Quantity;
        }
    }
}
