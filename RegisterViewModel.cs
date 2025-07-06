using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FarmConnect.Model;
using FarmConnect.Services;

namespace FarmConnect.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        // Injected database service via constructor
        private readonly DatabaseService _databaseService;

        public RegisterViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // === Basic Registration Fields ===

        private string? fullName;
        public string? FullName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }

        private string? email;
        public string? Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string? phoneNumber;
        public string? PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }

        private string? password;
        public string? Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private string? confirmPassword;
        public string? ConfirmPassword
        {
            get => confirmPassword;
            set => SetProperty(ref confirmPassword, value);
        }

        private bool isFarmer;
        public bool IsFarmer
        {
            get => isFarmer;
            set => SetProperty(ref isFarmer, value);
        }

        // === Extra Fields for Farmers ===

        private string? farmName;
        public string? FarmName
        {
            get => farmName;
            set => SetProperty(ref farmName, value);
        }

        private string? farmLocation;
        public string? FarmLocation
        {
            get => farmLocation;
            set => SetProperty(ref farmLocation, value);
        }

        public bool IsFruits { get; set; }
        public bool IsVegetables { get; set; }
        public bool IsDairy { get; set; }
        public bool IsGrains { get; set; }
        public bool IsOtherProduce { get; set; }

        // === Registration Logic ===

        [RelayCommand]
        private async Task Register()
        {
            await _databaseService.InitAsync();

            // Validate inputs
            if (string.IsNullOrWhiteSpace(FullName) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(PhoneNumber) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await Shell.Current.DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await Shell.Current.DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            if (IsFarmer && (string.IsNullOrWhiteSpace(FarmName) || string.IsNullOrWhiteSpace(FarmLocation)))
            {
                await Shell.Current.DisplayAlert("Error", "Please fill in farm details.", "OK");
                return;
            }

            // Create user model
            var user = new User
            {
                FullName = FullName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Password = Password, // You should hash this in production
                Role = IsFarmer ? "Farmer" : "Client"
            };

            await _databaseService.AddUserAsync(user);

            await Shell.Current.DisplayAlert("Success", "Registration successful!", "OK");

            // Clear fields
            FullName = Email = PhoneNumber = Password = ConfirmPassword = FarmName = FarmLocation = string.Empty;
            IsFarmer = false;
            IsFruits = IsVegetables = IsDairy = IsGrains = IsOtherProduce = false;

            // Navigate to login page
            await Shell.Current.GoToAsync("///LoginPage");
        }
    }
}