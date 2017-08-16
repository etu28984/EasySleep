using EasySleep.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Template10.Services.NavigationService;

namespace EasySleep.Services
{
    class ServiceApi
    {

        private String url;
        public static String Token;

        public ServiceApi()
        {
            url = "http://localhost:5281/api";
        }

        public String GetUrl()
        {
            return url;
        }

        public async Task<string> GetToken(ApplicationUser user)
        {
            string error = null;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/x-www-form-urlencoded");
                string stringJSON = "Username=" + user.UserName + "&Password=" + user.Password + "&grant_type=password";
                var req = new StringContent(stringJSON, Encoding.UTF8);
                try
                {
                    var responseRequest = await client.PostAsync("http://localhost:5281/token", req);
                    if (!responseRequest.IsSuccessStatusCode)
                    {
                        error = await responseRequest.Content.ReadAsStringAsync();
                    }
                    var resContent = await responseRequest.Content.ReadAsStringAsync();
                    var raw = JObject.Parse(resContent);
                    var token = raw["token_type"].Value<string>() + " " + raw["access_token"].Value<string>();
                    Token = token;

                    client.DefaultRequestHeaders.Add("Authorization", token);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
            return error;
        }

        public async Task<string> Connexion(String _username, String _password)
        {
            ApplicationUser user = new ApplicationUser(_username, _password);
            return await GetToken(user);
        }

        public async Task<Boolean> CreateAppUser(ApplicationUser user)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage responseRegister = await client.PostAsJsonAsync<ApplicationUser>(GetUrl() + "/Account/Register", user);
                    if (!responseRegister.IsSuccessStatusCode)
                    {
                        String response = await responseRegister.Content.ReadAsStringAsync();
                    }
                    responseRegister.EnsureSuccessStatusCode();
                    return true;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    return false;
                }
            }
        }

        public async Task<ApplicationUser> GetUser()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Token);
                try
                {
                    var json = await client.GetStringAsync(new Uri(GetUrl() + "/Account/UserInfo"));
                    ApplicationUser user = (JsonConvert.DeserializeObject<ApplicationUser>(json));
                    return user;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    Debug.WriteLine(msg.ToString());
                }
            }
            return null;
        }

        public async Task<Boolean> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Token);
                var requestJson = new 
                {
                    OldPassword = oldPassword,
                    NewPassword = newPassword,
                    ConfirmPassword = confirmPassword
                };
                string reqJson = JsonConvert.SerializeObject(requestJson);
                var req = new StringContent(reqJson, Encoding.UTF8, "application/json");
                try
                {
                    var responseRequest = await client.PostAsync(GetUrl() + "/Account/ChangePassword", req);
                    if (!responseRequest.IsSuccessStatusCode)
                    {
                        String response = await responseRequest.Content.ReadAsStringAsync();
                    }
                    responseRequest.EnsureSuccessStatusCode();
                    return true;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    Debug.WriteLine(msg.ToString());
                }
            }
            return false;
        }

        public async Task<Boolean> CheckOfferExists(string userId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Token);
                try
                {
                    var json = await client.GetStringAsync(GetUrl() + "/Offers/OfferByUserId?userId=" + userId);
                    List<Offer> offers = (JsonConvert.DeserializeObject<List<Offer>>(json));
                    return (offers.First() != null) ? true : false;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    Debug.WriteLine(msg.ToString());
                }
                return false;
            }
        }

        public async Task<Offer> GetOffer(string userId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Token);
                try
                {
                    var json = await client.GetStringAsync(GetUrl() + "/Offers/OfferByUserId?userId=" + userId);
                    List<Offer> offers = (JsonConvert.DeserializeObject<List<Offer>>(json));
                    return offers.First();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    Debug.WriteLine(msg.ToString());
                }
                return null;
            }
        }

        public async Task<Location> GetOfferLocation(int locationId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Token);
                try
                {
                    var json = await client.GetStringAsync(GetUrl() + "/Locations/" + locationId);
                    Location location = (JsonConvert.DeserializeObject<Location>(json));
                    return location;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    Debug.WriteLine(msg.ToString());
                }
                return null;
            }
        }

        public async Task<ObservableCollection<Offer>> GetAllOffers(string userId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Token);
                try
                {
                    var json = await client.GetStringAsync(GetUrl() + "/Offers/AllActiveOffers?userId=" + userId);
                    List<Offer> offs = JsonConvert.DeserializeObject<List<Offer>>(json);
                    foreach (var off in offs)
                    {
                        off.Location = await GetOfferLocation(off.LocationId);
                    }
                    return new ObservableCollection<Offer>(offs);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    Debug.WriteLine(msg.ToString());
                }
                return null;
            }
        }
    
        public async Task<Boolean> AddOffer(Offer offer)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Token);
                string reqJson = JsonConvert.SerializeObject(offer);
                var req = new StringContent(reqJson, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage responseAddOffer = await client.PostAsync(GetUrl() + "/Offers", req);
                    if (!responseAddOffer.IsSuccessStatusCode)
                    {
                        String response = await responseAddOffer.Content.ReadAsStringAsync();
                    }
                    responseAddOffer.EnsureSuccessStatusCode();
                    return true;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    Debug.WriteLine(msg.ToString());
                }
                return false;
            }
        }

        public async Task<Boolean> UpdateOffer(int offerId, Offer offer)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Token);
                try
                {
                    string reqJson = JsonConvert.SerializeObject(offer.Location);
                    var req = new StringContent(reqJson, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseUpdateLoc = await client.PutAsync(GetUrl() + "/Locations/" + offer.LocationId, req);
                    if (!responseUpdateLoc.IsSuccessStatusCode)
                    {
                        String response = await responseUpdateLoc.Content.ReadAsStringAsync();
                        responseUpdateLoc.EnsureSuccessStatusCode();
                    }
                    else
                    {
                        offer.Location = null;
                        reqJson = JsonConvert.SerializeObject(offer);
                        req = new StringContent(reqJson, Encoding.UTF8, "application/json");
                        HttpResponseMessage responseUpdateOffer = await client.PutAsync(GetUrl() + "/Offers/" + offerId, req);
                        if (!responseUpdateOffer.IsSuccessStatusCode)
                        {
                            String response = await responseUpdateOffer.Content.ReadAsStringAsync();
                        }
                        responseUpdateOffer.EnsureSuccessStatusCode();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    Debug.WriteLine(msg.ToString());
                }
                return false;
            }
        }

        public async Task<List<Request>> GetRequests(int offId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Token);
                try
                {
                    var json = await client.GetStringAsync(GetUrl() + "/Requests/RequestByOffer?offerId=" + offId);
                    List<Request> requests = (JsonConvert.DeserializeObject<List<Request>>(json));
                    return requests;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    Debug.WriteLine(msg.ToString());
                }
                return null;
            }
        }

        public async Task<ApplicationUser> GetApplicant(string userId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Token);
                try
                {
                    var json = await client.GetStringAsync(GetUrl() + "/Account/User?id=" + userId);
                    ApplicationUser user = (JsonConvert.DeserializeObject<ApplicationUser>(json));
                    return user;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    Debug.WriteLine(msg.ToString());
                }
                return null;
            }
        }

        public async Task<Boolean> AddRequest(Request request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Token);
                string reqJson = JsonConvert.SerializeObject(request);
                var req = new StringContent(reqJson, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage responseAddRequest = await client.PostAsync(GetUrl() + "/Requests", req);
                    if (!responseAddRequest.IsSuccessStatusCode)
                    {
                        String response = await responseAddRequest.Content.ReadAsStringAsync();
                    }
                    responseAddRequest.EnsureSuccessStatusCode();
                    return true;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    Debug.WriteLine(msg.ToString());
                }
                return false;
            }
        }

        public async Task<Boolean> UpdateRequest(int requestId, Request req)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Token);
                string reqJson = JsonConvert.SerializeObject(req);
                var reqDb = new StringContent(reqJson, Encoding.UTF8, "application/json");
                HttpResponseMessage responseUpdateRequest = await client.PutAsync(GetUrl() + "/Requests/" + requestId, reqDb);
                try
                {
                    if (!responseUpdateRequest.IsSuccessStatusCode)
                    {
                        String response = await responseUpdateRequest.Content.ReadAsStringAsync();
                    }
                    responseUpdateRequest.EnsureSuccessStatusCode();
                    return true;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    Debug.WriteLine(msg.ToString());
                }
                return false;
            }
        }

        public async Task<Boolean> DelRequest(int requestId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Token);
                try
                {
                    HttpResponseMessage responseAddRequest = await client.DeleteAsync(GetUrl() + "/Requests/" + requestId);
                    if (!responseAddRequest.IsSuccessStatusCode)
                    {
                        String response = await responseAddRequest.Content.ReadAsStringAsync();
                    }
                    responseAddRequest.EnsureSuccessStatusCode();
                    return true;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    Debug.WriteLine(msg.ToString());
                }
                return false;
            }
        }

    }
}
