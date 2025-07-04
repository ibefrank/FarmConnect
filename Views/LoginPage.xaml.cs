using FarmConnect.ViewModel;
using Microsoft.Maui.Controls;

namespace FarmConnect.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel; 
    }
}
