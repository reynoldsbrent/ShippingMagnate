using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class HandleService : IHandleService
    {
        private readonly ApplicationDbContext _context;

        public HandleService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Handle handle)
        {
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            _context.Handles.Add(handle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string portCode, string containerBic, DateTime handlingDate)
        {
            var handle = await _context.Handles
                .FirstOrDefaultAsync(h =>
                    h.PortCode == portCode &&
                    h.ContainerBic == containerBic &&
                    h.HandlingDate == handlingDate);

            if (handle != null)
            {
                _context.Handles.Remove(handle);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Handle>> GetAllAsync()
        {
            return await _context.Handles
                .Include(h => h.Port)
                .Include(h => h.Container)
                .ToListAsync();
        }

        public async Task<Handle> GetByCompositeIdAsync(string portCode, string containerBic, DateTime handlingDate)
        {
            return await _context.Handles
                .Include(h => h.Port)
                .Include(h => h.Container)
                .FirstOrDefaultAsync(h =>
                    h.PortCode == portCode &&
                    h.ContainerBic == containerBic &&
                    h.HandlingDate == handlingDate);
        }

        public async Task UpdateAsync(Handle handle)
        {
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            var existingHandle = await _context.Handles
                .FirstOrDefaultAsync(h =>
                    h.PortCode == handle.PortCode &&
                    h.ContainerBic == handle.ContainerBic &&
                    h.HandlingDate == handle.HandlingDate);

            if (existingHandle == null)
                throw new KeyNotFoundException("Handle record not found");

            _context.Entry(existingHandle).CurrentValues.SetValues(handle);
            await _context.SaveChangesAsync();
        }
    }
}
