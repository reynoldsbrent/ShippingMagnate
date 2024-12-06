using api.Dtos;
using api.Models;

namespace api.Interfaces
{
    public interface IPortService
    {
        Task<Port> GetByIdAsync(string unCode);
        Task<IEnumerable<Port>> GetAllAsync();
        Task CreateAsync(Port port);
        Task UpdateAsync(Port port);
        Task DeleteAsync(string unCode);
        Task<IEnumerable<ShipAtPortDto>> GetShipsAtPortAsync(string portCode);
        Task<IEnumerable<ContainerAtPortDto>> ListContainersAtPortAsync(string portCode);
    }
}
