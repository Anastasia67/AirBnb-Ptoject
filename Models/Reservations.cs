namespace AirBnb.Models
{
    public class Reservations
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; } = new Location();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CustomerId { get; set; }
        public Customers Customer { get; set; } = new Customers();
        public float Discount { get; set; }
    }
}
