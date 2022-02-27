using BusinessLogics.DTO;
using DataAcess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogics.CustomerBusiness
{
    public interface ICustomerBusiness
    {
        Task<OperationalResult> GetCustomer(string Id);
        Task<OperationalResult> GetCustomers();
        Task<OperationalResult> AddCustomer(string standID, CustomerDTO customerDTO);
        Task<OperationalResult> UpdateCustomer(CustomerDTO customerDTO);
        Task<OperationalResult> UpdateCustomers(List<CustomerDTO> customerDTOs);
        Task DeleteCustomer(string Id);
    }
}
