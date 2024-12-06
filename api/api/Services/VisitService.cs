using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class VisitService : IVisitService
    {
        private readonly ApplicationDbContext _context;

        public VisitService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Visit visit)
        {
            if (visit == null)
                throw new ArgumentNullException(nameof(visit));

            _context.Visits.Add(visit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string shipImo, string portCode, DateTime visitDate)
        {
            var visit = await _context.Visits
                .FirstOrDefaultAsync(v =>
                    v.ShipImo == shipImo &&
                    v.PortCode == portCode &&
                    v.VisitDate == visitDate);

            if (visit != null)
            {
                _context.Visits.Remove(visit);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Visit>> GetAllAsync()
        {
            return await _context.Visits
                .Include(v => v.Ship)
                .Include(v => v.Port)
                .ToListAsync();
        }

        public async Task<Visit> GetByCompositeIdAsync(string shipImo, string portCode, DateTime visitDate)
        {
            return await _context.Visits
                .Include(v => v.Ship)
                .Include(v => v.Port)
                .FirstOrDefaultAsync(v =>
                    v.ShipImo == shipImo &&
                    v.PortCode == portCode &&
                    v.VisitDate == visitDate);
        }

        public async Task UpdateAsync(Visit visit)
        {
            if (visit == null)
                throw new ArgumentNullException(nameof(visit));

            var existingVisit = await _context.Visits
                .FirstOrDefaultAsync(v =>
                    v.ShipImo == visit.ShipImo &&
                    v.PortCode == visit.PortCode &&
                    v.VisitDate == visit.VisitDate);

            if (existingVisit == null)
                throw new KeyNotFoundException("Visit record not found");

            _context.Entry(existingVisit).CurrentValues.SetValues(visit);
            await _context.SaveChangesAsync();
        }
    }
}
