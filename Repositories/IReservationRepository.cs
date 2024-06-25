using AirBnb.Models;

public interface IReservationRepository
{
    Task<Reservations> GetById(int id, CancellationToken cancellationToken = default);
    Task Add(Reservations reservation, CancellationToken cancellationToken = default);
    Task Delete(int id, CancellationToken cancellationToken = default);
}