﻿using TopStyleDb.Models.DTO;
using TopStyleDb.Models.Entities;

namespace TopStyleDb.Core.Interfaces
{
    public interface ICustomerService
    {
        public Task<CustomerDTO> NewCustomer(Customer customer);
        public Task<CustomerDTO> GetCustomer(int id);
        public Task<List<CustomerDTO>> GetAllCustomers();
        public Task<AuthDTO> LoginUser(string username, string password);
    }
}
