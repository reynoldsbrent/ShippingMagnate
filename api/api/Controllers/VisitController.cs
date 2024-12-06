using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Visit>>> GetAll()
        {
            var visits = await _visitService.GetAllAsync();
            return Ok(visits);
        }

        [HttpGet("{shipImo}/{portCode}/{visitDate}")]
        public async Task<ActionResult<Visit>> GetById(string shipImo, string portCode, DateTime visitDate)
        {
            var visit = await _visitService.GetByCompositeIdAsync(shipImo, portCode, visitDate);
            if (visit == null)
                return NotFound();

            return Ok(visit);
        }

        [HttpPost]
        public async Task<ActionResult<Visit>> Create(Visit visit)
        {
            try
            {
                await _visitService.CreateAsync(visit);
                return CreatedAtAction(nameof(GetById),
                    new
                    {
                        shipImo = visit.ShipImo,
                        portCode = visit.PortCode,
                        visitDate = visit.VisitDate
                    }, visit);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{shipImo}/{portCode}/{visitDate}")]
        public async Task<IActionResult> Update(string shipImo, string portCode, DateTime visitDate, Visit visit)
        {
            if (shipImo != visit.ShipImo ||
                portCode != visit.PortCode ||
                visitDate != visit.VisitDate)
                return BadRequest();

            try
            {
                await _visitService.UpdateAsync(visit);
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

        [HttpDelete("{shipImo}/{portCode}/{visitDate}")]
        public async Task<IActionResult> Delete(string shipImo, string portCode, DateTime visitDate)
        {
            await _visitService.DeleteAsync(shipImo, portCode, visitDate);
            return NoContent();
        }
    }
}
