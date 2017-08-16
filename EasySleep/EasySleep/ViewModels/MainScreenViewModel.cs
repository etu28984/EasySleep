using EasySleep.Model;
using EasySleep.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace EasySleep.ViewModels
{
    class MainScreenViewModel : ViewModelBase
    {

        ServiceApi serviceApi;

        private int _selectedItem;
        public int SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value) { return; }
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public ObservableCollection<Offer> _offers;
        public ObservableCollection<Offer> Offers { get { return _offers; } set { Set(ref _offers, value); } }
        private ApplicationUser _user;
        public ApplicationUser User { get { return _user; } set { Set(ref _user, value); } }

        public MainScreenViewModel()
        {
            serviceApi = new ServiceApi();
        }

        public async Task GetUser()
        {
            User = await serviceApi.GetUser();
        }

        public async Task GetAllOffers()
        {
            Offers = await serviceApi.GetAllOffers(User.Id);
        }

        public void HandleSelected(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine(SelectedItem);
            NavigationService.Navigate(typeof(Views.DetailledOfferPage), Offers[SelectedItem]);
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            await Task.CompletedTask;
            await GetUser();
            await GetAllOffers();
        }

        public void GoToMainScreen() =>
            NavigationService.Navigate(typeof(Views.MainScreen));

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);
        
        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void Logout()
        {
            ServiceApi.Token = null;
            NavigationService.Navigate(typeof(Views.MainPage));
        }

    }
}
