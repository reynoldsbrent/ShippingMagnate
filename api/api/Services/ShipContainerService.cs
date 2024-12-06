using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class ShipContainerService : IShipContainerService
    {
        private readonly ApplicationDbContext _context;

        public ShipContainerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(ShipContainer shipContainer)
        {
            if (shipContainer == null)
                throw new ArgumentNullException(nameof(shipContainer));

            _context.ShipContainers.Add(shipContainer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string customerId, string containerBic, DateTime shippingDate)
        {
            var shipContainer = await _context.ShipContainers
                .FirstOrDefaultAsync(sc =>
                    sc.CustomerId == customerId &&
                    sc.ContainerBic == containerBic &&
                    sc.ShippingDate == shippingDate);

            if (shipContainer != null)
            {
                _context.ShipContainers.Remove(shipContainer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ShipContainer>> GetAllAsync()
        {
            return await _context.ShipContainers
                .Include(sc => sc.Customer)
                .Include(sc => sc.Container)
                .ToListAsync();
        }

        public async Task<ShipContainer> GetByCompositeIdAsync(string customerId, string containerBic, DateTime shippingDate)
        {
            return await _context.ShipContainers
                .Include(sc => sc.Customer)
                .Include(sc => sc.Container)
                .FirstOrDefaultAsync(sc =>
                    sc.CustomerId == customerId &&
                    sc.ContainerBic == containerBic &&
                    sc.ShippingDate == shippingDate);
        }

        public async Task UpdateAsync(ShipContainer shipContainer)
        {
            if (shipContainer == null)
                throw new ArgumentNullException(nameof(shipContainer));

            var existingShipContainer = await _context.ShipContainers
                .FirstOrDefaultAsync(sc =>
                    sc.CustomerId == shipContainer.CustomerId &&
                    sc.ContainerBic == shipContainer.ContainerBic &&
                    sc.ShippingDate == shipContainer.ShippingDate);

            if (existingShipContainer == null)
                throw new KeyNotFoundException("ShipContainer record not found");

            _context.Entry(existingShipContainer).CurrentValues.SetValues(shipContainer);
            await _context.SaveChangesAsync();
        }
    }
}
