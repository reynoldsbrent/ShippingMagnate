using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipContainerController : ControllerBase
    {
        private readonly IShipContainerService _shipContainerService;

        public ShipContainerController(IShipContainerService shipContainerService)
        {
            _shipContainerService = shipContainerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipContainer>>> GetAll()
        {
            var shipContainers = await _shipContainerService.GetAllAsync();
            return Ok(shipContainers);
        }

        [HttpGet("{customerId}/{containerBic}/{shippingDate}")]
        public async Task<ActionResult<ShipContainer>> GetById(string customerId, string containerBic, DateTime shippingDate)
        {
            var shipContainer = await _shipContainerService.GetByCompositeIdAsync(customerId, containerBic, shippingDate);
            if (shipContainer == null)
                return NotFound();

            return Ok(shipContainer);
        }

        [HttpPost]
        public async Task<ActionResult<ShipContainer>> Create(ShipContainer shipContainer)
        {
            try
            {
                await _shipContainerService.CreateAsync(shipContainer);
                return CreatedAtAction(nameof(GetById),
                    new
                    {
                        customerId = shipContainer.CustomerId,
                        containerBic = shipContainer.ContainerBic,
                        shippingDate = shipContainer.ShippingDate
                    }, shipContainer);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{customerId}/{containerBic}/{shippingDate}")]
        public async Task<IActionResult> Update(string customerId, string containerBic, DateTime shippingDate, ShipContainer shipContainer)
        {
            if (customerId != shipContainer.CustomerId ||
                containerBic != shipContainer.ContainerBic ||
                shippingDate != shipContainer.ShippingDate)
                return BadRequest();

            try
            {
                await _shipContainerService.UpdateAsync(shipContainer);
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

        [HttpDelete("{customerId}/{containerBic}/{shippingDate}")]
        public async Task<IActionResult> Delete(string customerId, string containerBic, DateTime shippingDate)
        {
            await _shipContainerService.DeleteAsync(customerId, containerBic, shippingDate);
            return NoContent();
        }
    }
}
