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
    class ApplyViewModel : ViewModelBase
    {

        ServiceApi serviceApi;

        private ApplicationUser _applicant;
        private Request _request;
        public ApplicationUser Applicant { get { return _applicant; } set { Set(ref _applicant, value); } }
        public Request Request { get { return _request; } set { Set(ref _request, value); } }


        private String _lastName;
        private String _firstName;
        private String _bornDate;
        public string LastName { get => _lastName; set => Set(ref _lastName, value); }
        public string FirstName { get => _firstName; set => Set(ref _firstName, value); }
        public String BornDate { get => _bornDate; set => Set(ref _bornDate, value); }

        public ApplyViewModel()
        {
            serviceApi = new ServiceApi();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            await Task.CompletedTask;
            Request = (Request)parameter;
            await GetApplicant(Request.ApplicantId);
            LastName = Applicant.LastName;
            FirstName = Applicant.FirstName;
            BornDate = Applicant.BornDate.Date.ToString("d");
        }

        public async void AcceptRequest()
        {
            Request.IsRequestAccepted = true;
            if (await serviceApi.UpdateRequest(Request.Id, Request))
                NavigationService.Navigate(typeof(Views.MainScreen));
        }

        public async void DeclineRequest()
        {
            if (await serviceApi.DelRequest(Request.Id))
                NavigationService.Navigate(typeof(Views.MainScreen));

        }

        public async Task GetApplicant(string applicantId) =>
            Applicant = await serviceApi.GetApplicant(applicantId);

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
