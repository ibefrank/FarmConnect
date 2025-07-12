using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;
using System;
using Microsoft.Maui.Controls;
using System.Linq;

namespace Farmconnect.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        // Properties for binding
        private bool _isClient = true;
        private bool _isFarmer;
        private bool _isTermsAccepted;
        private string _fullName = string.Empty; // Initialize to avoid CS8618
        private string _email = string.Empty; // Initialize to avoid CS8618
        private string _phoneNumber = string.Empty; // Initialize to avoid CS8618
        private string _password = string.Empty; // Initialize to avoid CS8618
        private string _confirmPassword = string.Empty; // Initialize to avoid CS8618
        private string _farmName = string.Empty; // Initialize to avoid CS8618
        private string _farmLocation = string.Empty; // Initialize to avoid CS8618
        private bool _isFruits;
        private bool _isVegetables;
        private bool _isDairy;
        private bool _isGrains;
        private bool _isOtherProduce;

        public bool IsClient
        {
            get => _isClient;
            set
            {
                if (_isClient != value)
                {
                    _isClient = value;
                    if (value) IsFarmer = false;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsFarmer
        {
            get => _isFarmer;
            set
            {
                if (_isFarmer != value)
                {
                    _isFarmer = value;
                    if (value) IsClient = false;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsTermsAccepted
        {
            get => _isTermsAccepted;
            set { _isTermsAccepted = value; OnPropertyChanged(); }
        }

        public string FullName
        {
            get => _fullName;
            set { _fullName = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        public string FarmName
        {
            get => _farmName;
            set { _farmName = value; OnPropertyChanged(); }
        }

        public string FarmLocation
        {
            get => _farmLocation;
            set { _farmLocation = value; OnPropertyChanged(); }
        }

        public bool IsFruits
        {
            get => _isFruits;
            set { _isFruits = value; OnPropertyChanged(); }
        }

        public bool IsVegetables
        {
            get => _isVegetables;
            set { _isVegetables = value; OnPropertyChanged(); }
        }

        public bool IsDairy
        {
            get => _isDairy;
            set { _isDairy = value; OnPropertyChanged(); }
        }

        public bool IsGrains
        {
            get => _isGrains;
            set { _isGrains = value; OnPropertyChanged(); }
        }

        public bool IsOtherProduce
        {
            get => _isOtherProduce;
            set { _isOtherProduce = value; OnPropertyChanged(); }
        }

        // Commands
        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged; // Marked nullable to avoid CS8618

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () => await RegisterAsync());
            LoginCommand = new Command(async () => await Shell.Current.GoToAsync("///LoginPage"));
        }

        private async Task RegisterAsync()
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(FullName) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(PhoneNumber) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                if (mainPage != null)
                {
                    await mainPage.DisplayAlert("Error", "Please fill in all required fields.", "OK");
                }
                return;
            }

            if (Password != ConfirmPassword)
            {
                var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                if (mainPage != null)
                {
                    await mainPage.DisplayAlert("Error", "Passwords do not match.", "OK");
                }
                return;
            }

            if (!IsTermsAccepted)
            {
                var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                if (mainPage != null)
                {
                    await mainPage.DisplayAlert("Error", "You must accept the terms and privacy policy.", "OK");
                }
                return;
            }

            if (IsFarmer)
            {
                if (string.IsNullOrWhiteSpace(FarmName) || string.IsNullOrWhiteSpace(FarmLocation))
                {
                    var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                    if (mainPage != null)
                    {
                        await mainPage.DisplayAlert("Error", "Please provide your farm name and location.", "OK");
                    }
                    return;
                }

                if (!IsFruits && !IsVegetables && !IsDairy && !IsGrains && !IsOtherProduce)
                {
                    var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                    if (mainPage != null)
                    {
                        await mainPage.DisplayAlert("Error", "Please select at least one type of produce.", "OK");
                    }
                    return;
                }
            }

            // Simulate registration logic (replace with your backend call)
            await Task.Delay(1000);

            var successMessage = IsFarmer ? "Farmer account created successfully!" : "Client account created successfully!";
            var mainPageSuccess = Application.Current?.Windows.FirstOrDefault()?.Page;
            if (mainPageSuccess != null)
            {
                await mainPageSuccess.DisplayAlert("Success", successMessage, "OK");
            }

            // Optionally, navigate to login or main page
            await Shell.Current.GoToAsync("///LoginPage");
        }

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}