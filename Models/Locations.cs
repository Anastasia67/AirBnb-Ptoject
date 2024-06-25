using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace AirBnb.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LocationType Type { get; set; }
        public int Rooms { get; set; }
        public int NumberOfGuests { get; set; }
        public Features Features { get; set; }
        public float PricePerDay { get; set; }
        public int LandlordId { get; set; }
        public Landlord? Landlord { get; set; } 
        public ICollection<Image>? Images { get; set; } 
        public ICollection<Reservations>? Reservations { get; set; } 
    }
}
