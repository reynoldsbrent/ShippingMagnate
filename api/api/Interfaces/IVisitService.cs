using api.Models;

namespace api.Interfaces
{
    public interface IVisitService
    {
        Task<IEnumerable<Visit>> GetAllAsync();
        Task<Visit> GetByCompositeIdAsync(string shipImo, string portCode, DateTime visitDate);
        Task CreateAsync(Visit visit);
        Task UpdateAsync(Visit visit);
        Task DeleteAsync(string shipImo, string portCode, DateTime visitDate);
    }
}
