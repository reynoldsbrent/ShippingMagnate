using api.Models;

namespace api.Interfaces
{
    public interface IHandleService
    {
        Task<IEnumerable<Handle>> GetAllAsync();
        Task<Handle> GetByCompositeIdAsync(string portCode, string containerBic, DateTime handlingDate);
        Task CreateAsync(Handle handle);
        Task UpdateAsync(Handle handle);
        Task DeleteAsync(string portCode, string containerBic, DateTime handlingDate);
    }
}
