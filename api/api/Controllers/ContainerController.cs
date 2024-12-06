using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IContainerService _containerService;

        public ContainerController(IContainerService containerService)
        {
            _containerService = containerService;
        }

        // GET: api/container
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Container>>> GetAll()
        {
            var containers = await _containerService.GetAllAsync();
            return Ok(containers);
        }

        // GET: api/container/ABCD1234567
        [HttpGet("{id}")]
        public async Task<ActionResult<Container>> GetById(string id)
        {
            var container = await _containerService.GetByIdAsync(id);
            if (container == null)
                return NotFound();

            return Ok(container);
        }

        // POST: api/container
        [HttpPost]
        public async Task<ActionResult<Container>> Create(Container container)
        {
            try
            {
                await _containerService.CreateAsync(container);
                return CreatedAtAction(nameof(GetById), new { id = container.BicCode }, container);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        // PUT: api/container/ABCD1234567
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Container container)
        {
            if (id != container.BicCode)
                return BadRequest();

            try
            {
                await _containerService.UpdateAsync(container);
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

        // DELETE: api/container/ABCD1234567
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _containerService.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/container/empty/count
        [HttpGet("empty/count")]
        public async Task<ActionResult<int>> GetEmptyContainerCount()
        {
            var count = await _containerService.GetEmptyContainerCountAsync();
            return Ok(new { EmptyContainers = count });
        }

        // PUT: api/container/ABCD1234567/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] bool isEmpty)
        {
            await _containerService.UpdateContainerStatusAsync(id, isEmpty);
            return NoContent();
        }
    }
}
