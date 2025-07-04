using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FarmConnect.Model;
using FarmConnect.Services;
using System.Threading.Tasks;

namespace FarmConnect.ViewModel
{   
public partial class RegisterViewModel : ObservableObject
    {
        private string? _fullName;
        public string? FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string? _email;
        public string? Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string? _phoneNumber;
        public string? PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        private string? _password;
        public string? Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string? _confirmPassword;
        public string? ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private bool _isFarmer;
        public bool IsFarmer
        {
            get => _isFarmer;
            set => SetProperty(ref _isFarmer, value);
        }

        [RelayCommand]
        private async Task Register()
        {
            await DatabaseService.InitAsync();

            // Basic validation
            if (string.IsNullOrWhiteSpace(FullName) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(PhoneNumber) ||
                string.IsNullOrWhiteSpace(Password) ||
                Password != ConfirmPassword)
            {
                // Show error message (implement as needed)
                return;
            }

            var user = new User
            {
                FullName = FullName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Password = Password, // Hash in production!
                Role = IsFarmer ? "Farmer" : "Client"
            };

            await DatabaseService.AddUserAsync(user);

            // Optionally, navigate to login or marketplace page
            // await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
