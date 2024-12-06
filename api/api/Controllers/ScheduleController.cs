using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAll()
        {
            var schedules = await _scheduleService.GetAllAsync();
            return Ok(schedules);
        }

        [HttpGet("{shipImo}/{departurePortCode}/{departureDate}")]
        public async Task<ActionResult<Schedule>> GetById(string shipImo, string departurePortCode, DateTime departureDate)
        {
            var schedule = await _scheduleService.GetByCompositeIdAsync(shipImo, departurePortCode, departureDate);
            if (schedule == null)
                return NotFound();

            return Ok(schedule);
        }

        [HttpPost]
        public async Task<ActionResult<Schedule>> Create(Schedule schedule)
        {
            try
            {
                await _scheduleService.CreateAsync(schedule);
                return CreatedAtAction(nameof(GetById),
                    new
                    {
                        shipImo = schedule.ShipImo,
                        departurePortCode = schedule.DeparturePortCode,
                        departureDate = schedule.DepartureDate
                    }, schedule);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{shipImo}/{departurePortCode}/{departureDate}")]
        public async Task<IActionResult> Update(string shipImo, string departurePortCode, DateTime departureDate, Schedule schedule)
        {
            if (shipImo != schedule.ShipImo ||
                departurePortCode != schedule.DeparturePortCode ||
                departureDate != schedule.DepartureDate)
                return BadRequest();

            try
            {
                await _scheduleService.UpdateAsync(schedule);
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

        [HttpDelete("{shipImo}/{departurePortCode}/{departureDate}")]
        public async Task<IActionResult> Delete(string shipImo, string departurePortCode, DateTime departureDate)
        {
            await _scheduleService.DeleteAsync(shipImo, departurePortCode, departureDate);
            return NoContent();
        }
    }
}
