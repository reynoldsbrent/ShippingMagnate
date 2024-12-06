using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _transportService;

        public TransportController(ITransportService transportService)
        {
            _transportService = transportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transport>>> GetAll()
        {
            var transports = await _transportService.GetAllAsync();
            return Ok(transports);
        }

        [HttpGet("{shipImo}/{containerBic}/{startDate}")]
        public async Task<ActionResult<Transport>> GetById(string shipImo, string containerBic, DateTime startDate)
        {
            var transport = await _transportService.GetByCompositeIdAsync(shipImo, containerBic, startDate);
            if (transport == null)
                return NotFound();

            return Ok(transport);
        }

        [HttpPost]
        public async Task<ActionResult<Transport>> Create(Transport transport)
        {
            try
            {
                await _transportService.CreateAsync(transport);
                return CreatedAtAction(nameof(GetById),
                    new
                    {
                        shipImo = transport.ShipImo,
                        containerBic = transport.ContainerBic,
                        startDate = transport.StartDate
                    }, transport);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{shipImo}/{containerBic}/{startDate}")]
        public async Task<IActionResult> Update(string shipImo, string containerBic, DateTime startDate, Transport transport)
        {
            if (shipImo != transport.ShipImo ||
                containerBic != transport.ContainerBic ||
                startDate != transport.StartDate)
                return BadRequest();

            try
            {
                await _transportService.UpdateAsync(transport);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{shipImo}/{containerBic}/{startDate}")]
        public async Task<IActionResult> Delete(string shipImo, string containerBic, DateTime startDate)
        {
            await _transportService.DeleteAsync(shipImo, containerBic, startDate);
            return NoContent();
        }
    }
}
