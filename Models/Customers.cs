namespace AirBnb.Models
{
    public class Customers
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<Reservations> Reservations { get; set; } = new HashSet<Reservations>();
    }
}
