using DataAcess.DataModels;
using DataAcess.DataServices;
using DataAcess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogics.CustomerBusiness
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly Customer _customer;
        private readonly IDataServices _dataServices;
        public CustomerBusiness(ICustomer customer, IDataServices dataServices)
        {
            _customer = customer as Customer;
            _dataServices = dataServices;
        }
        public Task<Customer> AddCustomer(Stand stand)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomer(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetCustomer(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> UpdateCustomer(string Id, Stand stand)
        {
            throw new NotImplementedException();
        }
    }
}
