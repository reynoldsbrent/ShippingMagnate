using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipController : ControllerBase
    {
        private readonly IShipService _shipService;

        public ShipController(IShipService shipService)
        {
            _shipService = shipService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ship>>> GetAll()
        {
            var ships = await _shipService.GetAllAsync();
            return Ok(ships);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ship>> GetById(string id)
        {
            var ship = await _shipService.GetByIdAsync(id);
            if (ship == null) return NotFound();
            return Ok(ship);
        }

        [HttpPost]
        public async Task<ActionResult<Ship>> Create(Ship ship)
        {
            await _shipService.CreateAsync(ship);
            return CreatedAtAction(nameof(GetById), new { id = ship.ImoNumber }, ship);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Ship ship)
        {
            if (id != ship.ImoNumber) return BadRequest();

            try
            {
                await _shipService.UpdateAsync(ship);
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
            await _shipService.DeleteAsync(id);
            return NoContent();
        }
    }
}
