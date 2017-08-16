using EasySleep.Model;
using EasySleep.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace EasySleep.ViewModels
{
    class DetailledOfferViewModel : ViewModelBase
    {

        ServiceApi serviceApi;

        public Offer Off { get; set; }

        private ApplicationUser _user;
        public ApplicationUser User { get { return _user; } set { Set(ref _user, value); } }

        private string _city;
        private string _street;
        private int _postalCode;
        private int _houseNum;
        private string _description;
        private int _maxPeople;
        public string City { get => _city; set => Set(ref _city, value); }
        public string Street { get => _street; set => Set(ref _street, value); }
        public int PostalCode { get => _postalCode; set => Set(ref _postalCode, value); }
        public int HouseNum { get => _houseNum; set => Set(ref _houseNum, value); }
        public string Description { get => _description; set => Set(ref _description, value); }
        public int MaxPeople { get => _maxPeople; set => Set(ref _maxPeople, value); }

        public DetailledOfferViewModel()
        {
            serviceApi = new ServiceApi();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            await Task.CompletedTask;
            await GetUser();
            Off = (Offer)parameter;
            City = Off.Location.City;
            Street = Off.Location.Street;
            PostalCode = Off.Location.PostalCode;
            HouseNum = Off.Location.HouseNum;
            Description = Off.Description;
            MaxPeople = Off.MaxPeople;
        }

        public async void SendRequest()
        {
            Request req = new Request(User.Id, Off.Id, false);
            Boolean IsRequestCreated = await serviceApi.AddRequest(req);
            if (IsRequestCreated)
                NavigationService.Navigate(typeof(Views.MainScreen));
        }

        public async Task GetUser()
        {
            User = await serviceApi.GetUser();
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
