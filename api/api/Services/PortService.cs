using api.Data;
using api.Dtos;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace api.Services
{
    public class PortService : IPortService
    {
        private readonly ApplicationDbContext _context;

        public PortService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Port> GetByIdAsync(string unCode)
        {
            return await _context.Ports
                .FirstOrDefaultAsync(p => p.UnCode == unCode);
        }

        public async Task<IEnumerable<Port>> GetAllAsync()
        {
            return await _context.Ports.ToListAsync();
        }

        public async Task CreateAsync(Port port)
        {
            _context.Ports.Add(port);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Port port)
        {
            var existingPort = await _context.Ports.FindAsync(port.UnCode);
            if (existingPort == null)
                throw new KeyNotFoundException($"Port with UN code {port.UnCode} not found");

            _context.Entry(existingPort).CurrentValues.SetValues(port);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string unCode)
        {
            var port = await _context.Ports.FindAsync(unCode);
            if (port != null)
            {
                _context.Ports.Remove(port);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ShipAtPortDto>> GetShipsAtPortAsync(string portCode)
        {
            var result = new List<ShipAtPortDto>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GetShipsAtPort";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("@portCode", portCode));

                if (command.Connection.State != ConnectionState.Open)
                    await command.Connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new ShipAtPortDto
                        {
                            ShipName = reader.GetString("ship_name"),
                            ImoNumber = reader.GetString("IMO_number"),
                            VisitDate = reader.GetDateTime("visit_date")
                        });
                    }
                }
            }

            return result;
        }

        public async Task<IEnumerable<ContainerAtPortDto>> ListContainersAtPortAsync(string portCode)
        {
            var result = new List<ContainerAtPortDto>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "ListContainersAtPort";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("@portCode", portCode));

                if (command.Connection.State != ConnectionState.Open)
                    await command.Connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new ContainerAtPortDto
                        {
                            BicCode = reader.GetString("BIC_code"),
                            SizeTeu = reader.GetDecimal("size_TEU"),
                            IsEmpty = reader.GetBoolean("is_empty"),
                            HandlingDate = reader.GetDateTime("handling_date")
                        });
                    }
                }
            }

            return result;
        }
    }
}
