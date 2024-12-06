using api.Models;

namespace api.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetByIdAsync(string customerId);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(string customerId);
        Task<int> GetCustomerContainerCountAsync(string customerId);
    }
}
