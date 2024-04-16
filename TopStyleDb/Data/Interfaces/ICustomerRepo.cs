using TopStyleDb.Models.Entities;

namespace TopStyleDb.Data.Interfaces
{
    public interface ICustomerRepo
    {
        public Task<Customer> CreateCustomer(Customer customer);
        public Task<Customer> GetCustomer(int id);
        public Task<List<Customer>> GetAllCustomers();
        public Task<Customer> GetUserByEmail(string email);
    }
}
