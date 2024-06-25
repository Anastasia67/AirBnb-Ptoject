using AirBnb.Data;
using AirBnb.Models;

public class ReservationRepository : IReservationRepository
{
    private readonly AirBnBDbContext _context;

    public ReservationRepository(AirBnBDbContext context)
    {
        _context = context;
    }

    public async Task<Reservations> GetById(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Reservations.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task Add(Reservations reservation, CancellationToken cancellationToken = default)
    {
        await _context.Reservations.AddAsync(reservation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(int id, CancellationToken cancellationToken = default)
    {
        var reservation = await GetById(id, cancellationToken);
        if (reservation != null)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}