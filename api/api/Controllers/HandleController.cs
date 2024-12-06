using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandleController : ControllerBase
    {
        private readonly IHandleService _handleService;

        public HandleController(IHandleService handleService)
        {
            _handleService = handleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Handle>>> GetAll()
        {
            var handles = await _handleService.GetAllAsync();
            return Ok(handles);
        }

        [HttpGet("{portCode}/{containerBic}/{handlingDate}")]
        public async Task<ActionResult<Handle>> GetById(string portCode, string containerBic, DateTime handlingDate)
        {
            var handle = await _handleService.GetByCompositeIdAsync(portCode, containerBic, handlingDate);
            if (handle == null)
                return NotFound();

            return Ok(handle);
        }

        [HttpPost]
        public async Task<ActionResult<Handle>> Create(Handle handle)
        {
            try
            {
                await _handleService.CreateAsync(handle);
                return CreatedAtAction(nameof(GetById),
                    new
                    {
                        portCode = handle.PortCode,
                        containerBic = handle.ContainerBic,
                        handlingDate = handle.HandlingDate
                    }, handle);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{portCode}/{containerBic}/{handlingDate}")]
        public async Task<IActionResult> Update(string portCode, string containerBic, DateTime handlingDate, Handle handle)
        {
            if (portCode != handle.PortCode ||
                containerBic != handle.ContainerBic ||
                handlingDate != handle.HandlingDate)
                return BadRequest();

            try
            {
                await _handleService.UpdateAsync(handle);
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

        [HttpDelete("{portCode}/{containerBic}/{handlingDate}")]
        public async Task<IActionResult> Delete(string portCode, string containerBic, DateTime handlingDate)
        {
            await _handleService.DeleteAsync(portCode, containerBic, handlingDate);
            return NoContent();
        }
    }
}
