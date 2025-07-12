using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace Farmconnect.ViewModels
{
    public class LoginViewModel
    {
        public ICommand SignUpCommand { get; }

        public LoginViewModel()
        {
            SignUpCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///RegisterPage");
            });
        }
    }
}