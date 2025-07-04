using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FarmConnect.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace FarmConnect.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        // Backing fields
        private string _email = string.Empty;
        private string _password = string.Empty;
        private bool _isRememberMe;
        private string _errorMessage = string.Empty;
        private bool _isPassword = true;
        private string _passwordToggleIcon = "eye_closed_icon.png";

        // Properties with SetProperty (AOT-safe)
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsRememberMe
        {
            get => _isRememberMe;
            set => SetProperty(ref _isRememberMe, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool IsPassword
        {
            get => _isPassword;
            set => SetProperty(ref _isPassword, value);
        }

        public string PasswordToggleIcon
        {
            get => _passwordToggleIcon;
            set => SetProperty(ref _passwordToggleIcon, value);
        }

        // Constructor
        private readonly DatabaseService _databaseService;
        public LoginViewModel(DatabaseService databaseService)
        {
            
            _databaseService = databaseService;
        }

        [RelayCommand]
        private void TogglePasswordVisibility()
        {
            IsPassword = !IsPassword;
            PasswordToggleIcon = IsPassword ? "eye_closed_icon.png" : "eye_open_icon.png";
        }

        [RelayCommand]
        private async Task Login()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter both email and password.";
                return;
            }

            var user = await DatabaseService.GetUserByEmailAsync(Email);
            if (user != null && user.Password == Password)
            {
                if (IsSupportedPlatform())
                {
                    await Shell.Current.GoToAsync("//MarketplacePage");
                }
            }
            else
            {
                ErrorMessage = "Invalid email or password.";
            }
        }

        [RelayCommand]
        private async Task SignUp()
        {
            if (IsSupportedPlatform())
            {
                await Shell.Current.GoToAsync("RegisterPage");
            }
        }

        [RelayCommand]
        private async Task ForgotPassword()
        {
            if (IsSupportedPlatform())
            {
                await Shell.Current.GoToAsync("ForgotPasswordPage");
            }
        }

        [RelayCommand]
        private void GoogleLogin()
        {
            Console.WriteLine("Google login tapped");
        }

        [RelayCommand]
        private void FacebookLogin()
        {
            Console.WriteLine("Facebook login tapped");
        }

        private bool IsSupportedPlatform()
        {
            return DeviceInfo.Platform == DevicePlatform.Android
                || DeviceInfo.Platform == DevicePlatform.iOS
                || DeviceInfo.Platform == DevicePlatform.WinUI
                || DeviceInfo.Platform == DevicePlatform.macOS;
        }
    }
}
