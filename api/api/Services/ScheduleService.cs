using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext _context;

        public ScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Schedule schedule)
        {
            if (schedule == null)
                throw new ArgumentNullException(nameof(schedule));

            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string shipImo, string departurePortCode, DateTime departureDate)
        {
            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(s =>
                    s.ShipImo == shipImo &&
                    s.DeparturePortCode == departurePortCode &&
                    s.DepartureDate == departureDate);

            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Schedule>> GetAllAsync()
        {
            return await _context.Schedules
                .Include(s => s.Ship)
                .Include(s => s.ArrivalPort)
                .Include(s => s.DeparturePort)
                .ToListAsync();
        }

        public async Task<Schedule> GetByCompositeIdAsync(string shipImo, string departurePortCode, DateTime departureDate)
        {
            return await _context.Schedules
                .Include(s => s.Ship)
                .Include(s => s.ArrivalPort)
                .Include(s => s.DeparturePort)
                .FirstOrDefaultAsync(s =>
                    s.ShipImo == shipImo &&
                    s.DeparturePortCode == departurePortCode &&
                    s.DepartureDate == departureDate);
        }

        public async Task UpdateAsync(Schedule schedule)
        {
            if (schedule == null)
                throw new ArgumentNullException(nameof(schedule));

            var existingSchedule = await _context.Schedules
                .FirstOrDefaultAsync(s =>
                    s.ShipImo == schedule.ShipImo &&
                    s.DeparturePortCode == schedule.DeparturePortCode &&
                    s.DepartureDate == schedule.DepartureDate);

            if (existingSchedule == null)
                throw new KeyNotFoundException("Schedule not found");

            _context.Entry(existingSchedule).CurrentValues.SetValues(schedule);
            await _context.SaveChangesAsync();
        }
    }
}
