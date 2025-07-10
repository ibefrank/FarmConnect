using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoreSpotlight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmConnect.Model;
using System.Collections.ObjectModel;

namespace FarmConnect.ViewModel
{
    public partial class MarketplaceViewModel : ObservableObject
    {
        public ObservableCollection<Produce> ProduceList { get; } = new();

        [ObservableProperty]
        private string searchQuery;

        public MarketplaceViewModel()
        {
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            ProduceList.Clear();
            ProduceList.Add(new Produce { Name = "Tomatoes", Description = "Fresh farm tomatoes", Price = 50 });
            ProduceList.Add(new Produce { Name = "Milk", Description = "Daily fresh milk", Price = 60 });
            ProduceList.Add(new Produce { Name = "Maize", Description = "Dried yellow maize", Price = 80 });
        }

        [RelayCommand]
        private void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                LoadSampleData();
                return;
            }

            var filtered = ProduceList.Where(p => p.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();

            ProduceList.Clear();
            foreach (var item in filtered)
                ProduceList.Add(item);
        }
    }
}
