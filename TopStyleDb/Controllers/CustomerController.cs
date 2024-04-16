using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopStyleDb.Core.Interfaces;
using TopStyleDb.Models.DTO;
using TopStyleDb.Models.Entities;

namespace TopStyleDb.Controllers
{
    [Route("api/customer/")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IAuthService _authService;
        public CustomerController(ICustomerService customerService, IAuthService authService)
        {
            _service = customerService;
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> LoginUser(LoginDTO user)
        {
            var userDto = await _service.LoginUser(user.Email, user.Password);
            if (userDto == null)
            {
                return Unauthorized("Invalid Email or Password.");
            }

            return Ok(new { userDto.Email, userDto.Token });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            var newCustomer = await _service.CreateCustomer(customer);
            if (newCustomer == null)
            {
                return BadRequest("Unable to create customer.");
            }
            return CreatedAtAction(nameof(GetCustomer), new { id = newCustomer.CustomerId }, newCustomer);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _service.GetCustomer(id);
            if (customer == null)
            {
                return NotFound("No customer with that ID.");
            }
            return Ok(customer);
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _service.GetAllCustomers();
            if (customers == null || !customers.Any())
            {
                return NotFound("No customers found.");
            }
            return Ok(customers);
        }

        
    }
}
