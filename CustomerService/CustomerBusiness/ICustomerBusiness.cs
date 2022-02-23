using DataAcess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.CustomerBusinessLayer
{
    interface ICustomerBusiness
    {
        Task<Customer> GetCustomer(string Id);
        Task<List<Customer>> GetCustomers();
        Task<Customer> AddCustomer(Stand stand);
        Task<Customer> UpdateCustomer(string Id, Stand stand);
        Task DeleteCustomer(string Id);
    }
}
