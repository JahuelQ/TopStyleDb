using TopStyleDb.Core.Interfaces;
using TopStyleDb.Data.Interfaces;
using TopStyleDb.Models.DTO;
using TopStyleDb.Models.Entities;

namespace TopStyleDb.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _repo;
        private readonly IAuthService _authService;
        public CustomerService(ICustomerRepo repo, IAuthService authService)
        {
            _repo = repo;
            _authService = authService;
        }

        public async Task<CustomerDTO> NewCustomer(Customer customer)
        {
            var createdCustomer = await _repo.NewCustomer(customer);
            if (createdCustomer == null)
            {
                return null;
            }

            return new CustomerDTO
            {
                CustomerId = createdCustomer.CustomerId,
                FullName = createdCustomer.FirstName + ", " + createdCustomer.LastName,
                Email = createdCustomer.Email
            };
        }

        public async Task<List<CustomerDTO>> GetAllCustomers()
        {
            var customers = await _repo.GetAllCustomers();
            if (customers == null || !customers.Any())
            {
                return null;
            }

            return customers.Select(c => new CustomerDTO
            {
                CustomerId = c.CustomerId,
                FullName = c.FirstName + ", " + c.LastName,
                Email = c.Email
            }).ToList();
        }

        public async Task<CustomerDTO> GetCustomer(int id)
        {
            var customer = await _repo.GetCustomer(id);
            if (customer == null)
            {
                return null;
            }

            return new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FirstName + ", " + customer.LastName,
                Email = customer.Email
            };
        }

        public async Task<AuthDTO> LoginUser(string email, string password)
        {
            var user = await _repo.GetUserByEmail(email);
            if (user != null && user.Password == password)
            {
                var token = _authService.GenerateToken(user);
                return new AuthDTO { CustomerId = user.CustomerId, Email = user.Email, Token = token };
            }
            return null;
        }
    }
}
