using api.Models;

namespace api.Interfaces
{
    public interface IShipContainerService
    {
        Task<IEnumerable<ShipContainer>> GetAllAsync();
        Task<ShipContainer> GetByCompositeIdAsync(string customerId, string containerBic, DateTime shippingDate);
        Task CreateAsync(ShipContainer shipContainer);
        Task UpdateAsync(ShipContainer shipContainer);
        Task DeleteAsync(string customerId, string containerBic, DateTime shippingDate);
    }
}
