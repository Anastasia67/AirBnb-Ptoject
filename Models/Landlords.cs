using System.Collections.Generic;

namespace AirBnb.Models
{
    public class Landlord
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public int AvatarId { get; set; }
        public Image Avatar { get; set; } = new Image();
        public ICollection<Location> Locations { get; set; } = new List<Location>();
    }
}
