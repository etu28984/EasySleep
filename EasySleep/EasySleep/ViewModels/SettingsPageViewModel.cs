using EasySleep.Model;
using EasySleep.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.SettingsService;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace EasySleep.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private ServiceApi serviceApi;

        public SettingsPartViewModel SettingsPartViewModel { get; } = new SettingsPartViewModel();
        public AboutPartViewModel AboutPartViewModel { get; } = new AboutPartViewModel();

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

        private List<Request> _requests;
        private ApplicationUser _user;
        private ApplicationUser _applicant;
        private List<ApplicationUser> _applicants;
        private Offer _off;
        public List<Request> Requests { get { return _requests; } set { Set(ref _requests, value); } }
        public ApplicationUser User { get { return _user; } set { Set(ref _user, value); } }
        public ApplicationUser Applicant { get { return _applicant; } set { Set(ref _applicant, value); } }
        public List<ApplicationUser> Applicants { get { return _applicants; } set { Set(ref _applicants, value); } }
        public Offer Off { get { return _off; } set { Set(ref _off, value); } }

        public SettingsPageViewModel()
        {
            serviceApi = new ServiceApi();
        }

        public async Task GetUser() => User = await serviceApi.GetUser();

        public async Task GetRequests(int offId) => Requests = await serviceApi.GetRequests(offId);

        public async Task GetOffer(string id) => Off = await serviceApi.GetOffer(id);

        public async Task GetApplicant(string userId) => Applicant = await serviceApi.GetApplicant(userId);

        public void HandleSelected(object sender, SelectionChangedEventArgs e)
        {
            if(SelectedItem < Requests.Count())
                NavigationService.Navigate(typeof(Views.ApplyPage), Requests[SelectedItem]);
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            await Task.CompletedTask;
            await GetUser();
            await GetOffer(User.Id);
            if (Off != null)
            {
                await GetRequests(Off.Id);
                if (Requests != null)
                {
                    Applicants = new List<ApplicationUser>();
                    foreach (var req in Requests)
                    {
                        await GetApplicant(req.ApplicantId);
                        Applicants.Add(Applicant);
                    }
                }
            }
        }

        public void ChangePassword() =>
            NavigationService.Navigate(typeof(Views.ChangePasswordPage));

        public void GoToMainScreen() =>
            NavigationService.Navigate(typeof(Views.MainScreen));

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GoToMyOffer() =>
            NavigationService.Navigate(typeof(Views.MyOfferPage));


        public void Logout()
        {
            ServiceApi.Token = null;
            NavigationService.Navigate(typeof(Views.MainPage));
        }

    }
    
    public class SettingsPartViewModel : ViewModelBase
    {
        Services.SettingsServices.SettingsService _settings;

        public SettingsPartViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                // designtime
            }
            else
            {
                _settings = Services.SettingsServices.SettingsService.Instance;
            }
        }

        public bool ShowHamburgerButton
        {
            get { return _settings.ShowHamburgerButton; }
            set { _settings.ShowHamburgerButton = value; base.RaisePropertyChanged(); }
        }

        public bool IsFullScreen
        {
            get { return _settings.IsFullScreen; }
            set
            {
                _settings.IsFullScreen = value;
                base.RaisePropertyChanged();
                if (value)
                {
                    ShowHamburgerButton = false;
                }
                else
                {
                    ShowHamburgerButton = true;
                }
            }
        }

        public bool UseShellBackButton
        {
            get { return _settings.UseShellBackButton; }
            set { _settings.UseShellBackButton = value; base.RaisePropertyChanged(); }
        }

        public bool UseLightThemeButton
        {
            get { return _settings.AppTheme.Equals(ApplicationTheme.Light); }
            set { _settings.AppTheme = value ? ApplicationTheme.Light : ApplicationTheme.Dark; base.RaisePropertyChanged(); }
        }

        private string _BusyText = "Please wait...";
        public string BusyText
        {
            get { return _BusyText; }
            set
            {
                Set(ref _BusyText, value);
                _ShowBusyCommand.RaiseCanExecuteChanged();
            }
        }

        DelegateCommand _ShowBusyCommand;
        public DelegateCommand ShowBusyCommand
            => _ShowBusyCommand ?? (_ShowBusyCommand = new DelegateCommand(async () =>
            {
                Views.Busy.SetBusy(true, _BusyText);
                await Task.Delay(5000);
                Views.Busy.SetBusy(false);
            }, () => !string.IsNullOrEmpty(BusyText)));
    }

    public class AboutPartViewModel : ViewModelBase
    {
        private ServiceApi serviceApi;

        public Uri Logo => Windows.ApplicationModel.Package.Current.Logo;

        private int _selectedItem;
        public int SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value) { return; }
                _selectedItem = value;
                RaisePropertyChanged(() => _selectedItem);
            }
        }

        public ApplicationUser User { get; set; }

        private String _lastName;
        private String _firstName;
        private String _bornDate;
        public string LastName { get => _lastName; set => Set(ref _lastName, value); }
        public string FirstName { get => _firstName; set => Set(ref _firstName, value); }
        public String BornDate { get => _bornDate; set => Set(ref _bornDate, value); }

        public AboutPartViewModel()
        {
            serviceApi = new ServiceApi();
            GetUser();
            
        }

        public async void GetUser()
        {
            User = await serviceApi.GetUser();
            LastName = User.LastName;
            FirstName = User.FirstName;
            BornDate = User.BornDate.Date.ToString("d");
        }
        
    }
}
