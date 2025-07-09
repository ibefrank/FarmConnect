
using System.Diagnostics;

namespace FruitFarmers.ViewModels
{
    public class OrderConfirmationViewModel : BaseViewModel
    {
        private string _orderNumber;
        public string OrderNumber
        {
            get => _orderNumber;
            set => SetProperty(ref _orderNumber, value);
        }
        
        private DateTime _orderDate;
        public DateTime OrderDate
        {
            get => _orderDate;
            set => SetProperty(ref _orderDate, value);
        }
        
        private string _deliveryMethod;
        public string DeliveryMethod
        {
            get => _deliveryMethod;
            set => SetProperty(ref _deliveryMethod, value);
        }
        
        private string _paymentMethod;
        public string PaymentMethod
        {
            get => _paymentMethod;
            set => SetProperty(ref _paymentMethod, value);
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
        
        private decimal _totalPaid;
        public decimal TotalPaid
        {
            get => _totalPaid;
            set => SetProperty(ref _totalPaid, value);
        }
        
        private string _deliveryAddress;
        public string DeliveryAddress
        {
            get => _deliveryAddress;
            set => SetProperty(ref _deliveryAddress, value);
        }
        
        private DateTime _estimatedDelivery;
        public DateTime EstimatedDelivery
        {
            get => _estimatedDelivery;
            set => SetProperty(ref _estimatedDelivery, value);
        }
        
        public OrderConfirmationViewModel()
        {
            Title = "Order Confirmation";
            LoadMockOrderData();
        }
        
        private void LoadMockOrderData()
        {
            try
            {
                // In a real app, this data would come from the order that was just placed
                OrderNumber = "FRF-45879";
                OrderDate = DateTime.Now;
                DeliveryMethod = "Home Delivery";
                PaymentMethod = "MPesa";
                Subtotal = 390;
                DeliveryFee = 50;
                TotalPaid = Subtotal + DeliveryFee;
                DeliveryAddress = "123 Main Street, Nairobi, Kenya";
                EstimatedDelivery = DateTime.Now.AddDays(2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading order data: {ex.Message}");
            }
        }
    }
}
