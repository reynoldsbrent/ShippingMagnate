using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(string customerId)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task<int> GetCustomerContainerCountAsync(string customerId)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GetCustomerContainerCount";
                command.CommandType = CommandType.StoredProcedure;

                var containerCountParam = new MySqlParameter
                {
                    ParameterName = "@containerCount",
                    MySqlDbType = MySqlDbType.Int32,
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(new MySqlParameter("@customerId", customerId));
                command.Parameters.Add(containerCountParam);

                if (command.Connection.State != ConnectionState.Open)
                    await command.Connection.OpenAsync();

                await command.ExecuteNonQueryAsync();

                return (int)containerCountParam.Value;
            }
        }

        public async Task UpdateAsync(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            var existingCustomer = await _context.Customers.FindAsync(customer.CustomerId);
            if (existingCustomer == null)
                throw new KeyNotFoundException($"Customer with ID {customer.CustomerId} not found");

            _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
            await _context.SaveChangesAsync();
        }
    }
}
