using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace api.Services
{
    public class ContainerService : IContainerService
    {
        private readonly ApplicationDbContext _context;

        public ContainerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Container container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            _context.Containers.Add(container);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string bicCode)
        {
            var container = await _context.Containers.FindAsync(bicCode);
            if (container != null)
            {
                _context.Containers.Remove(container);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Container>> GetAllAsync()
        {
            return await _context.Containers.ToListAsync();
        }

        public async Task<Container> GetByIdAsync(string bicCode)
        {
            return await _context.Containers
                .FirstOrDefaultAsync(c => c.BicCode == bicCode);
        }

        public async Task<int> GetEmptyContainerCountAsync()
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GetEmptyContainerCount";
                command.CommandType = CommandType.StoredProcedure;

                var outputParameter = new MySqlParameter
                {
                    ParameterName = "@emptyCount",
                    MySqlDbType = MySqlDbType.Int32,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParameter);

                if (command.Connection.State != ConnectionState.Open)
                    await command.Connection.OpenAsync();

                await command.ExecuteNonQueryAsync();

                return (int)outputParameter.Value;
            }
        }

        public async Task UpdateAsync(Container container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            var existingContainer = await _context.Containers.FindAsync(container.BicCode);
            if (existingContainer == null)
                throw new KeyNotFoundException($"Container with BIC code {container.BicCode} not found");

            _context.Entry(existingContainer).CurrentValues.SetValues(container);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContainerStatusAsync(string bicCode, bool isEmpty)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "UpdateContainerStatus";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new MySqlParameter("@containerBIC", bicCode));
                command.Parameters.Add(new MySqlParameter("@isEmpty", isEmpty));

                if (command.Connection.State != ConnectionState.Open)
                    await command.Connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
