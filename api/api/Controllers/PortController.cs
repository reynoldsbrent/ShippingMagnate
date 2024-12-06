using api.Dtos;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortController : ControllerBase
    {
        private readonly IPortService _portService;

        public PortController(IPortService portService)
        {
            _portService = portService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Port>>> GetAll()
        {
            var ports = await _portService.GetAllAsync();
            return Ok(ports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Port>> GetById(string id)
        {
            var port = await _portService.GetByIdAsync(id);
            if (port == null) return NotFound();
            return Ok(port);
        }

        [HttpPost]
        public async Task<ActionResult<Port>> Create(Port port)
        {
            await _portService.CreateAsync(port);
            return CreatedAtAction(nameof(GetById), new { id = port.UnCode }, port);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Port port)
        {
            if (id != port.UnCode) return BadRequest();

            try
            {
                await _portService.UpdateAsync(port);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _portService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{portCode}/ships")]
        public async Task<ActionResult<IEnumerable<ShipAtPortDto>>> GetShipsAtPort(string portCode)
        {
            var ships = await _portService.GetShipsAtPortAsync(portCode);
            return Ok(ships);
        }

        [HttpGet("{portCode}/containers")]
        public async Task<ActionResult<IEnumerable<ContainerAtPortDto>>> GetContainersAtPort(string portCode)
        {
            var containers = await _portService.ListContainersAtPortAsync(portCode);
            return Ok(containers);
        }
    }
}
