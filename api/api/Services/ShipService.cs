using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class ShipService : IShipService
    {
        private readonly ApplicationDbContext _context;

        public ShipService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Ship ship)
        {
            _context.Ships.Add(ship);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string imoNumber)
        {
            var ship = await _context.Ships.FindAsync(imoNumber);
            if (ship != null)
            {
                _context.Ships.Remove(ship);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Ship>> GetAllAsync()
        {
            return await _context.Ships.ToListAsync();
        }

        public async Task<Ship> GetByIdAsync(string imoNumber)
        {
            return await _context.Ships
            .FirstOrDefaultAsync(s => s.ImoNumber == imoNumber);
        }

        public async Task UpdateAsync(Ship ship)
        {
            // Check if ship exists
            var existingShip = await _context.Ships.FindAsync(ship.ImoNumber);
            if (existingShip == null)
                throw new KeyNotFoundException($"Ship with IMO number {ship.ImoNumber} not found");

            _context.Entry(existingShip).CurrentValues.SetValues(ship);
            await _context.SaveChangesAsync();
        }
    }
}
