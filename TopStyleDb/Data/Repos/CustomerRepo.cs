using Microsoft.EntityFrameworkCore;
using TopStyleDb.Data.Interfaces;
using TopStyleDb.Models.Entities;

namespace TopStyleDb.Data.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly TopStyleDbContext _context;
        public CustomerRepo(TopStyleDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> GetUserByEmail(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
