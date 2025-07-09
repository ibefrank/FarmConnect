
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using FruitFarmers.Models;

namespace FruitFarmers.ViewModels
{
    public class BuyerOrdersViewModel : BaseViewModel
    {
        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }
        
        public ICommand RefreshCommand { get; }
        
        public BuyerOrdersViewModel()
        {
            Title = "My Orders";
            Orders = new ObservableCollection<Order>();
            RefreshCommand = new Command<string>(async (status) => await LoadOrdersAsync(status ?? "Pending"));
        }
        
        public async Task LoadOrdersAsync(string status)
        {
            if (IsBusy) return;
            IsBusy = true;
            
            try
            {
                // In a real app, this would fetch orders from a service
                // For now, we'll load mock data
                await Task.Delay(500); // Simulate network delay
                
                LoadMockOrders(status);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading orders: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        public async Task<bool> CancelOrderAsync(int orderId)
        {
            if (IsBusy) return false;
            IsBusy = true;
            
            try
            {
                // In a real app, this would cancel the order via a service
                // For now, we'll just simulate success
                await Task.Delay(500); // Simulate network delay
                
                // Find the order in our collection
                var order = Orders.FirstOrDefault(o => o.Id == orderId);
                if (order == null)
                {
                    return false;
                }
                
                // Remove the order from the collection
                Orders.Remove(order);
                
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error cancelling order: {ex.Message}");
                return false;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        private void LoadMockOrders(string status)
        {
            Orders.Clear();
            
            if (status == "Pending")
            {
                Orders.Add(new Order
                {
                    Id = 1,
                    OrderNumber = "FRF-45879",
                    OrderDate = DateTime.Now.AddDays(-1),
                    Status = "Pending",
                    StatusColor = Colors.Orange,
                    ItemsSummary = "2x Fresh Mangoes, 3x Oranges",
                    Total = 440,
                    CanBeCancelled = true
                });
                
                Orders.Add(new Order
                {
                    Id = 2,
                    OrderNumber = "FRF-45880",
                    OrderDate = DateTime.Now.AddDays(-1),
                    Status = "Pending",
                    StatusColor = Colors.Orange,
                    ItemsSummary = "1x Avocados, 2x Bananas",
                    Total = 180,
                    CanBeCancelled = true
                });
            }
            else if (status == "Processing")
            {
                Orders.Add(new Order
                {
                    Id = 3,
                    OrderNumber = "FRF-45878",
                    OrderDate = DateTime.Now.AddDays(-2),
                    Status = "Processing",
                    StatusColor = Colors.Blue,
                    ItemsSummary = "3x Strawberries, 1x Pineapple",
                    Total = 550,
                    CanBeCancelled = false
                });
            }
            else if (status == "Completed")
            {
                Orders.Add(new Order
                {
                    Id = 4,
                    OrderNumber = "FRF-45875",
                    OrderDate = DateTime.Now.AddDays(-5),
                    Status = "Completed",
                    StatusColor = Colors.Green,
                    ItemsSummary = "2x Oranges, 1x Peaches",
                    Total = 260,
                    CanBeCancelled = false
                });
                
                Orders.Add(new Order
                {
                    Id = 5,
                    OrderNumber = "FRF-45870",
                    OrderDate = DateTime.Now.AddDays(-10),
                    Status = "Completed",
                    StatusColor = Colors.Green,
                    ItemsSummary = "4x Bananas, 2x Avocados",
                    Total = 360,
                    CanBeCancelled = false
                });
            }
        }
    }
}
