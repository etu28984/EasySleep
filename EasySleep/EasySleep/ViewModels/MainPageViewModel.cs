using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using EasySleep.Services;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;

namespace EasySleep.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        ServiceApi serviceApi;

        private string _username;
        private string _password;

        public string Username { get => _username; set => Set(ref _username, value); }
        public string Password { get => _password; set => Set(ref _password, value); }

        public MainPageViewModel()
        {
            serviceApi = new ServiceApi();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            await Task.CompletedTask;
        }
        
        public void GotoRegistrationPage() => NavigationService.Navigate(typeof(Views.RegistrationPage));

        public async void Connexion()
        {
            string error = await serviceApi.Connexion(Username, Password);
            if(error == null)
            {
                NavigationService.Navigate(typeof(Views.MainScreen));
            }
            else
            {
                displayLoginDialog(error);
            }
        }

        public async void displayLoginDialog(string stringError)
        {
            ContentDialog createDialog = new ContentDialog()
            {
                Title = "Errors",
                Content = stringError,
                PrimaryButtonText = "Ok"
            };
            ContentDialogResult result = await createDialog.ShowAsync();
        }

    }
}
