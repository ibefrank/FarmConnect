using System;
using System.Collections.Generic;

namespace FruitFarmers.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // Pending, Accepted, Completed, Canceled
        public string DeliveryAddress { get; set; }
        public string ItemsList { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public string ImageUrl { get; set; }
    }
}
using System.Diagnostics;
using Microsoft.Maui.Graphics;

namespace FruitFarmers.Models
{
    public class Order : ObservableObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        
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
        
        private string _status;
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }
        
        private Color _statusColor;
        public Color StatusColor
        {
            get => _statusColor;
            set => SetProperty(ref _statusColor, value);
        }
        
        private string _itemsSummary;
        public string ItemsSummary
        {
            get => _itemsSummary;
            set => SetProperty(ref _itemsSummary, value);
        }
        
        private decimal _total;
        public decimal Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }
        
        private bool _canBeCancelled;
        public bool CanBeCancelled
        {
            get => _canBeCancelled;
            set => SetProperty(ref _canBeCancelled, value);
        }
        
        // Additional properties for order details would be included here
    }
}
waah

using SQLite;

namespace FruitFarmers.Models
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Indexed]
        public int CustomerId { get; set; }
        
        public string OrderNumber { get; set; }
        
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        public decimal TotalAmount { get; set; }
        
        public string Status { get; set; } // Pending, Processing, Shipped, Delivered, Cancelled
        
        public string ShippingAddress { get; set; }
        
        public string ShippingCity { get; set; }
        
        public string ShippingState { get; set; }
        
        public string ShippingZipCode { get; set; }
        
        public string PaymentMethod { get; set; }
        
        public string PaymentStatus { get; set; } // Pending, Completed, Failed, Refunded
        
        public string TransactionId { get; set; }
        
        public decimal ShippingCost { get; set; }
        
        public decimal Tax { get; set; }
        
        public decimal Discount { get; set; }
        
        public string PromoCode { get; set; }
        
        public DateTime? DeliveryDate { get; set; }
        
        public string Notes { get; set; }
    }
}