namespace AirBnb.Models
{
    public class LocationDto
    {
        public string Title { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int NumberOfGuests { get; set; }
        public float PricePerDay { get; set; }
        public int Type { get; set; }
        public int Features { get; set; }
        public string ImageUrl { get; set; }
        public string LandlordAvatarUrl { get; set; }
        public IEnumerable<ImageDto> Images { get; set; }
        public LandlordDto Landlord { get; set; }
    }

    public class SearchRequestDto
    {
        public int? Features { get; set; }
        public int? Type { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }

    public class MaxPriceDto
    {
        public decimal Price { get; set; }
    }

    public class ImageDto
    {
        public string Url { get; set; }
        public bool IsCover { get; set; }
    }

    public class LandlordDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
    }

    public class UnAvailableDatesResponseDto
    {
        public List<DateTime> UnAvailableDates { get; set; }
    }

    public class CreateReservationRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LocationId { get; set; }
        public float? Discount { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CreateReservationResponseDto
    {
        public string LocationName { get; set; }
        public string CustomerName { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
    }
}