using api.Models;

namespace api.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAllAsync();
        Task<Schedule> GetByCompositeIdAsync(string shipImo, string departurePortCode, DateTime departureDate);
        Task CreateAsync(Schedule schedule);
        Task UpdateAsync(Schedule schedule);
        Task DeleteAsync(string shipImo, string departurePortCode, DateTime departureDate);
    }
}
