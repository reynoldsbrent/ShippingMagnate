using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(string id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST: api/customer
        [HttpPost]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            try
            {
                await _customerService.CreateAsync(customer);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = customer.CustomerId },
                    customer);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        // PUT: api/customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Customer customer)
        {
            if (id != customer.CustomerId)
                return BadRequest();

            try
            {
                await _customerService.UpdateAsync(customer);
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

        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _customerService.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/customer/{id}/container-count
        [HttpGet("{id}/container-count")]
        public async Task<ActionResult<int>> GetContainerCount(string id)
        {
            try
            {
                var count = await _customerService.GetCustomerContainerCountAsync(id);
                return Ok(new { CustomerId = id, ContainerCount = count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error retrieving container count", Error = ex.Message });
            }
        }
    }
}
