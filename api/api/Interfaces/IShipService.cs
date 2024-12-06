using api.Models;

namespace api.Interfaces
{
    public interface IShipService
    {
        Task<Ship> GetByIdAsync(string imoNumber);
        Task<IEnumerable<Ship>> GetAllAsync();
        Task CreateAsync(Ship ship);
        Task UpdateAsync(Ship ship);
        Task DeleteAsync(string imoNumber);
    }
}
