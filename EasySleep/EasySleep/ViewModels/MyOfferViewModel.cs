using EasySleep.Model;
using EasySleep.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Popups;

namespace EasySleep.ViewModels
{
    class MyOfferViewModel : ViewModelBase
    {

        ServiceApi serviceApi;

        public bool HasOffer { get; set; }
        public Offer MyOffer { get; set; }
        public ApplicationUser User { get; set; }
        public Location OfferLocation { get; set; }

        private string _city;
        private string _street;
        private int _postalCode;
        private int _houseNum;
        private string _description;
        private bool _isActive;
        private int _maxPeople;
        private int _doorNum;
        public string City { get => _city; set => Set(ref _city, value); }
        public string Street { get => _street; set => Set(ref _street, value); }
        public int PostalCode { get => _postalCode; set => Set(ref _postalCode, value); }
        public int HouseNum { get => _houseNum; set => Set(ref _houseNum, value); }
        public string Description { get => _description; set => Set(ref _description, value); }
        public bool IsActive { get => _isActive; set => Set(ref _isActive, value); }
        public int MaxPeople { get => _maxPeople; set => Set(ref _maxPeople, value); }
        public int DoorNum { get => _doorNum; set => Set(ref _doorNum, value); }

        public MyOfferViewModel()
        {
            serviceApi = new ServiceApi();
            GetOffer();
        }

        public async void GetOffer()
        {
            User = await serviceApi.GetUser();
            HasOffer = await serviceApi.CheckOfferExists(User.Id);
            if (HasOffer)
            {
                MyOffer = await serviceApi.GetOffer(User.Id);
                OfferLocation = await serviceApi.GetOfferLocation(MyOffer.LocationId);
                City = OfferLocation.City;
                Street = OfferLocation.Street;
                PostalCode = OfferLocation.PostalCode;
                HouseNum = OfferLocation.HouseNum;
                DoorNum = OfferLocation.DoorNum;
                Description = MyOffer.Description;
                IsActive = MyOffer.IsActive;
                MaxPeople = MyOffer.MaxPeople;
            }
        }

        public async void AddOffer(Offer offer)
        {
            string error = null;
            if (City != null && City != "")
            {
                if (Street != null && Street != "")
                {
                    if (PostalCode >= 1000 && PostalCode <= 9999 && PostalCode.GetType() == typeof(int))
                    {
                        if (HouseNum > 0 && HouseNum <= 999 && HouseNum.GetType() == typeof(int))
                        {
                            if (Description != null && Description != "")
                            {
                                if (MaxPeople > 0 && MaxPeople.GetType() == typeof(int))
                                {
                                    if (await serviceApi.AddOffer(offer))
                                        NavigationService.Navigate(typeof(Views.MainScreen));
                                }
                                else
                                    error = "Vous devez renseigner un nombre maximal correct de personnes";
                            }
                            else
                                error = "Vous devez renseigner une description";
                        }
                        else
                            error = "Vous devez renseigner un numéro de maison correct (Entre 1 et 999)";
                    }
                    else
                        error = "Vous devez renseigner un code postal correct (Entre 1000 et 9999)";
                }
                else
                    error = "Vous devrez renseigner un nom de rue";
            }
            else
                error = "Vous devrez renseigner un nom de ville";

            if (error != null)
            {
                await new MessageDialog(error, "Erreur").ShowAsync();
            }

        }

        public async void UpdateOffer(int offerId, Offer offer)
        {
            string error = null;
            if (City != null && City != "")
            {
                if (Street != null && Street != "")
                {
                    if (PostalCode >= 1000 && PostalCode <= 9999 && PostalCode.GetType() == typeof(int))
                    {
                        if (HouseNum > 0 && HouseNum <= 999 && HouseNum.GetType() == typeof(int))
                        {
                            if (Description != null && Description != "")
                            {
                                if (MaxPeople > 0 && MaxPeople.GetType() == typeof(int))
                                {
                                    if (await serviceApi.UpdateOffer(offerId, offer))
                                        NavigationService.Navigate(typeof(Views.MainScreen));
                                }
                                else
                                    error = "Vous devez renseigner un nombre maximal correct de personnes";
                            }
                            else
                                error = "Vous devez renseigner une description";
                        }
                        else
                            error = "Vous devez renseigner un numéro de maison correct (Entre 1 et 999)";
                    }
                    else
                        error = "Vous devez renseigner un code postal correct (Entre 1000 et 9999)";
                }
                else
                    error = "Vous devrez renseigner un nom de rue";
            }
            else
                error = "Vous devrez renseigner un nom de ville";

            if (error != null)
            {
                await new MessageDialog(error, "Erreur").ShowAsync();
            }
        }

        public void Submit()
        {
            Location submitLocation; 
            
            if (HasOffer)
            {
                submitLocation = new Location(MyOffer.LocationId, Street, HouseNum, PostalCode, DoorNum, City);
                Offer offer = new Offer(Description, MyOffer.Id, IsActive, submitLocation, MyOffer.LocationId,  MaxPeople, null, User.Id);
                UpdateOffer(MyOffer.Id, offer);
            }
            else
            {
                submitLocation = new Location(Street, HouseNum, PostalCode, DoorNum, City);
                Offer offer = new Offer(Description, IsActive, submitLocation, MaxPeople, null, User.Id);
                AddOffer(offer);
            }
        }

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GoToMainScreen() =>
            NavigationService.Navigate(typeof(Views.MainScreen));

        public void Cancel() =>
            NavigationService.Navigate(typeof(Views.MainScreen));

        public void Logout()
        {
            ServiceApi.Token = null;
            NavigationService.Navigate(typeof(Views.MainPage));
        }
    }
}
