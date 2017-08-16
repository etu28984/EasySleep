using EasySleep.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace EasySleep.ViewModels
{
    class ChangePasswordViewModel : ViewModelBase
    {

        private ServiceApi serviceApi;

        private string _oldPassword;
        private string _newPassword;
        private string confirmPassword;

        public string OldPassword { get => _oldPassword; set => Set(ref _oldPassword, value); }
        public string NewPassword { get => _newPassword; set => Set(ref _newPassword, value); }
        public string ConfirmPassword { get => confirmPassword; set => Set(ref confirmPassword, value); }

        public ChangePasswordViewModel()
        {
            serviceApi = new ServiceApi();
        }

        public async void ChangePassword()
        {
            bool hasPasswordChanged = await serviceApi.ChangePassword(OldPassword, NewPassword, ConfirmPassword);
            if(hasPasswordChanged)
            {
                NavigationService.Navigate(typeof(Views.MainScreen));
            }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            await Task.CompletedTask;
        }

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GoToMainScreen() =>
            NavigationService.Navigate(typeof(Views.MainScreen));

        public void Logout()
        {
            ServiceApi.Token = null;
            NavigationService.Navigate(typeof(Views.MainPage));
        }

    }
}
