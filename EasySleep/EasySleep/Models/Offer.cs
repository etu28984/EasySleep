using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySleep.Model
{
    public class Offer
    {

        public int Id { get; set; }
        public Boolean IsActive { get; set; }
        public List<Photo> Photos { get; set; }
        public String Description { get; set; }
        public int MaxPeople { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public ApplicationUser Owner { get; set; }
        public String OwnerId { get; set; }
        public Uri Logo => Windows.ApplicationModel.Package.Current.Logo;

        public Offer(string description, Boolean isActive, Location loc, int maxPeople, ApplicationUser owner, string ownerId)
        {
            IsActive = isActive;
            Description = description;
            MaxPeople = maxPeople;
            Location = loc;
            Owner = owner;
            OwnerId = ownerId;
        }

        public Offer(string description, Boolean isActive, int locationId, int maxPeople, ApplicationUser owner, string ownerId)
        {
            IsActive = isActive;
            Description = description;
            MaxPeople = maxPeople;
            LocationId = locationId;
            Owner = owner;
            OwnerId = ownerId;
        }

        public Offer(string description, Boolean isActive, int locationId, Location loc, int maxPeople, ApplicationUser owner, string ownerId)
        {
            IsActive = isActive;
            Description = description;
            MaxPeople = maxPeople;
            LocationId = locationId;
            Location = loc;
            Owner = owner;
            OwnerId = ownerId;
        }

        [JsonConstructor]
        public Offer(string description, int id, Boolean isActive, Location loc, int locationId, int maxPeople, ApplicationUser owner, string ownerId)
        {
            Id = id;
            IsActive = isActive;
            Description = description;
            MaxPeople = maxPeople;
            Location = loc;
            LocationId = locationId;
            Owner = owner;
            OwnerId = ownerId;
        }

        public override string ToString()
        {
            return Description + Location;
        }

    }
}
