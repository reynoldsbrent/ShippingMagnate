using api.Models;

namespace api.Interfaces
{
    public interface IContainerService
    {
        Task<int> GetEmptyContainerCountAsync();
        Task UpdateContainerStatusAsync(string bicCode, bool isEmpty);
        Task<Container> GetByIdAsync(string bicCode);
        Task<IEnumerable<Container>> GetAllAsync();
        Task CreateAsync(Container container);
        Task UpdateAsync(Container container);
        Task DeleteAsync(string bicCode);
    }
}
