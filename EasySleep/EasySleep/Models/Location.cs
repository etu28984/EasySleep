using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySleep.Model
{
    public class Location
    {

        public int Id { get; set; }
        public String Street { get; set; }
        public int HouseNum { get; set; }
        public int PostalCode { get; set; }
        public int DoorNum { get; set; }
        public String City { get; set; }

        public Location (String street, int houseNum, int postalCode, int doorNum, String city)
        {
            Street = street;
            HouseNum = houseNum;
            PostalCode = postalCode;
            DoorNum = doorNum;
            City = city;
        }

        [JsonConstructor]
        public Location(int id, String street, int houseNum, int postalCode, int doorNum, String city)
        {
            Id = id;
            Street = street;
            HouseNum = houseNum;
            PostalCode = postalCode;
            DoorNum = doorNum;
            City = city;
        }

        public override string ToString()
        {
            return Street + " " + HouseNum + " " + PostalCode + " " + DoorNum + " " + City;
        }

    }
}
