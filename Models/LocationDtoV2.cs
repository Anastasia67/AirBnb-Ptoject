namespace AirBnb.Models
{
    public class LocationDtoV2
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string LandlordAvatarUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Type { get; set; }
    }
}