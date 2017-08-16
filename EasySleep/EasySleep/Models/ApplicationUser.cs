using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySleep.Model
{
    public class ApplicationUser
    {

        public String Id { get; set; }
        public String UserName { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String Password { get; set; }
        public String ConfirmPassword { get; set; }
        public String Email { get; set; }
        public DateTime BornDate { get; set; }
        public String PhoneNumber { get; set; }
        public Boolean IsBanned { get; set; }
        public String ProfilePicture { get; set; }
        public Boolean HasRegistered { get; set; }
        public String LoginProvider { get; set; }

        public ApplicationUser(string username, string password)
        {
            UserName = username;
            Password = password;
        }

        public ApplicationUser(string lastName, string firstName, string password, string confirmPassword, string email, DateTime bornDate, string phoneNum, Boolean isBanned)
        {
            UserName = email;
            LastName = lastName;
            FirstName = firstName;
            Password = password;
            ConfirmPassword = confirmPassword;
            Email = email;
            BornDate = bornDate;
            PhoneNumber = phoneNum;
            IsBanned = isBanned;
        }

        [JsonConstructor]
        public ApplicationUser(string id, string email, string lastName, string firstName, string phoneNum, DateTime bornDate, Boolean isBanned,
            bool hasRegistered, string loginProvider)
        {
            Id = id;
            UserName = email;
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            BornDate = bornDate;
            PhoneNumber = phoneNum;
            IsBanned = isBanned;
            HasRegistered = hasRegistered;
            LoginProvider = loginProvider;
        }

        public override string ToString()
        {
            return Id + " " + UserName;
        }

    }
}
