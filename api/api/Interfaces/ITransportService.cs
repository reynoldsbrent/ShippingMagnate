using api.Models;

namespace api.Interfaces
{
    public interface ITransportService
    {
        Task<IEnumerable<Transport>> GetAllAsync();
        Task<Transport> GetByCompositeIdAsync(string shipImo, string containerBic, DateTime startDate);
        Task CreateAsync(Transport transport);
        Task UpdateAsync(Transport transport);
        Task DeleteAsync(string shipImo, string containerBic, DateTime startDate);
    }
}
