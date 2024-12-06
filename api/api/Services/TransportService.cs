using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class TransportService : ITransportService
    {
        private readonly ApplicationDbContext _context;

        public TransportService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Transport transport)
        {
            if (transport == null)
                throw new ArgumentNullException(nameof(transport));

            _context.Transports.Add(transport);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string shipImo, string containerBic, DateTime startDate)
        {
            var transport = await _context.Transports
                .FirstOrDefaultAsync(t =>
                    t.ShipImo == shipImo &&
                    t.ContainerBic == containerBic &&
                    t.StartDate == startDate);

            if (transport != null)
            {
                _context.Transports.Remove(transport);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Transport>> GetAllAsync()
        {
            return await _context.Transports
                .Include(t => t.Ship)
                .Include(t => t.Container)
                .ToListAsync();
        }

        public async Task<Transport> GetByCompositeIdAsync(string shipImo, string containerBic, DateTime startDate)
        {
            return await _context.Transports
                .Include(t => t.Ship)
                .Include(t => t.Container)
                .FirstOrDefaultAsync(t =>
                    t.ShipImo == shipImo &&
                    t.ContainerBic == containerBic &&
                    t.StartDate == startDate);
        }

        public async Task UpdateAsync(Transport transport)
        {
            if (transport == null)
                throw new ArgumentNullException(nameof(transport));

            var existingTransport = await _context.Transports
                .FirstOrDefaultAsync(t =>
                    t.ShipImo == transport.ShipImo &&
                    t.ContainerBic == transport.ContainerBic &&
                    t.StartDate == transport.StartDate);

            if (existingTransport == null)
                throw new KeyNotFoundException("Transport record not found");

            _context.Entry(existingTransport).CurrentValues.SetValues(transport);
            await _context.SaveChangesAsync();
        }
    }
}
