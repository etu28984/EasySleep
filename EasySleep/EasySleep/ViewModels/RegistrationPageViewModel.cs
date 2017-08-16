using EasySleep.Model;
using EasySleep.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace EasySleep.ViewModels
{
    public class RegistrationPageViewModel : ViewModelBase
    {

        ServiceApi serviceApi;

        private string _Value = "Default";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        private string _password;
        private string _confirmPassword;
        private string _lastName;
        private string _firstName;
        private string _email;
        private DateTimeOffset _dateToConvert;
        private string _phoneNumber;
        private bool _isBanned;

        public string Password { get => _password; set => Set(ref _password, value); }
        public string ConfirmPassword { get => _confirmPassword; set => Set(ref _confirmPassword, value); }
        public string LastName { get => _lastName; set => Set(ref _lastName, value); }
        public string FirstName { get => _firstName; set => Set(ref _firstName, value); }
        public string Email { get => _email; set => Set(ref _email, value); }
        public DateTimeOffset DateToConvert { get => _dateToConvert; set => Set(ref _dateToConvert, value); }
        public string PhoneNumber { get => _phoneNumber; set => Set(ref _phoneNumber, value); }
        public bool IsBanned { get => _isBanned; set => Set(ref _isBanned, value); }

        public RegistrationPageViewModel()
        {
            serviceApi = new ServiceApi();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Value = (suspensionState.ContainsKey(nameof(Value))) ? suspensionState[nameof(Value)]?.ToString() : parameter?.ToString();
            await Task.CompletedTask;
        }

        public void Cancel() => NavigationService.Navigate(typeof(Views.MainPage));

        public async void CreateAppUser()
        {
            DateTime BornDate = _dateToConvert.DateTime;
            ApplicationUser user = new ApplicationUser(LastName, FirstName, Password, ConfirmPassword, Email, BornDate, PhoneNumber, false);
            string error = null;
            if (LastName != null && LastName != "")
            {
                if (FirstName != null && FirstName != "")
                {
                    if (Password.Equals(ConfirmPassword) && Password != null && Password != "" && ConfirmPassword != null && ConfirmPassword != "")
                    {
                        if (Password.Any(c => char.IsUpper(c)) && ConfirmPassword.Any(c => char.IsUpper(c)))
                        {
                            if (Password.Length <= 12 && ConfirmPassword.Length <= 12)
                            {
                                if (Email != null && Email != "")
                                {
                                    if (PhoneNumber != null && PhoneNumber != "")
                                    {
                                        if (await serviceApi.CreateAppUser(user))
                                        {
                                            NavigationService.Navigate(typeof(Views.MainPage));
                                        }
                                        else
                                            error = "Cet Email est déjà utilisé ou l'api est injoignable";
                                    }
                                    else
                                        error = "Vous devez renseigner votre numéro de téléphone";
                                }
                                else
                                    error = "Vous devez renseigner une adresse mail";
                            }
                            else
                                error = "Vos mots de passe ne doivent pas dépasser 12 caractères";
                        }
                        else
                            error = "Vos mots de passe doivent contenir une majuscule";
                    }
                    else
                        error = "Vos mots de passe ne correspondent pas";
                }
                else
                    error = "Vous devez renseigner votre prénom";
            }
            else
                error = "Vous devez renseigner votre nom de famille";

            if(error != null)
            {
                await new MessageDialog(error, "Erreur").ShowAsync();
            }
                                    
        }

    }
}
