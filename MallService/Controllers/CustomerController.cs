using BusinessLogics.CustomerBusiness;
using BusinessLogics.DTO;
using DataAcess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBusiness _customerBusiness;
        public CustomerController(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        // POST api/<CustomerController>
        [HttpPost("addCustomer/{standId}")]
        public async Task<IActionResult> AddCustomer(string standId, [FromBody] CustomerDTO customerDTO)
        {
            try
            {
                var result = await _customerBusiness.AddCustomer(standId, customerDTO);
                if (result.Status == false)
                {
                    var customer = result.Data.FirstOrDefault() as CustomerDTO;
                    return Created($"Customer with Id: {customer.Id} has been added.", customer);
                }
                else
                {
                    var error = result.ErrorList.FirstOrDefault();
                    return Problem(error.ErrorMessage, null, error.ErrorCode);
                }
                
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Get api/<CustomerController>
        [HttpGet("getCustomer/{customerId}")]
        public async Task<IActionResult> GetCustomer(string customerId)
        {
            try
            {
                var result = await _customerBusiness.GetCustomer(customerId);
                if (result.Status == false)
                {
                    var customer = result.Data.FirstOrDefault() as CustomerDTO;
                    return Ok($"Retrieved Customer with Id: {customer.Id}");
                }
                else
                {
                    var error = result.ErrorList.FirstOrDefault();
                    return Problem(error.ErrorMessage, null, error.ErrorCode);
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Get api/<CustomerController>
        [HttpGet("getAllCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var result = await _customerBusiness.GetCustomers();
                if (result.Status == false)
                {
                    var customers = result.Data.FirstOrDefault() as List<CustomerDTO>;
                    return Ok($"Retrieved Customers with Ids: {string.Join(",", customers.Select(i=>i.Id))}");
                }
                else
                {
                    var error = result.ErrorList.FirstOrDefault();
                    return Problem(error.ErrorMessage, null, error.ErrorCode);
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Get api/<CustomerController>
        [HttpPost("getCustomersByIds")]
        public async Task<IActionResult> GetCustomers([FromBody] List<string> Ids)
        {
            try
            {
                var result = await _customerBusiness.GetCustomers(Ids);
                if (result.Status == false)
                {
                    var customers = result.Data.FirstOrDefault() as List<CustomerDTO>;
                    return Ok($"Retrieved Customers with Ids: {string.Join(",", customers.Select(i => i.Id))}");
                }
                else
                {
                    var error = result.ErrorList.FirstOrDefault();
                    return Problem(error.ErrorMessage, null, error.ErrorCode);
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Put api/<CustomerController>
        [HttpPut("updateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDTO customerDTO)
        {
            try
            {
                var result = await _customerBusiness.UpdateCustomer(customerDTO);
                if (result.Status == false)
                {
                    var customers = result.Data.FirstOrDefault() as List<CustomerDTO>;
                    return Ok($"Customers with Id: {customerDTO.Id} updated.");
                }
                else
                {
                    var error = result.ErrorList.FirstOrDefault();
                    return Problem(error.ErrorMessage, null, error.ErrorCode);
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Put api/<CustomerController>
        [HttpPut("updateCustomers")]
        public async Task<IActionResult> UpdateCustomers([FromBody] List<CustomerDTO> customerDTOs)
        {
            try
            {
                var result = await _customerBusiness.UpdateCustomers(customerDTOs);
                if (result.Status == false)
                {
                    return Ok($"Customers have been updated.");
                }
                else
                {
                    var errors = string.Join(" and also ", result.ErrorList.Select(e=>e.ErrorMessage));
                    return Problem(errors, null, 500);
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Delete api/<CustomerController>
        [HttpDelete("deleteCustomer/{customerId}")]
        public async Task<IActionResult> DeleteCustomer(string customerId)
        {
            try
            {
                var result = await _customerBusiness.DeleteCustomer(customerId);
                if (result.Status == false)
                {
                    return Ok($"Customer with Id: {customerId} has been deleted.");
                }
                else
                {
                    var error = result.ErrorList.FirstOrDefault();
                    return Problem(error.ErrorMessage, null, error.ErrorCode);
                }
                
                
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Delete api/<CustomerController>
        [HttpPost("deleteCustomersByIds")]
        public async Task<IActionResult> DeleteStandBulk([FromBody] List<string> Ids)
        {
            try
            {
                await _customerBusiness.DeleteCustomers(Ids);
                return Ok($"Customers with Ids: {string.Join(",", Ids)} have been deleted.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Post api/<CustomerController>
        [HttpPost("buyProductBy/{customerId}")]
        public async Task<IActionResult> BuyProduct(string customerId)
        {
            try
            {
                var result = await _customerBusiness.BuyProduct(customerId);
                if (result.Status == false)
                {
                    return Ok($"Customers {customerId} has completed shopping.");
                }
                else
                {
                    var error = result.ErrorList.FirstOrDefault();
                    return Problem(error.ErrorMessage, null, error.ErrorCode);
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }

        // Post api/<CustomerController>
        [HttpPost("addProduct/{customerId}/{standId}")]
        public async Task<IActionResult> AddProduct(string customerId, string standId)
        {
            try
            {
                var result = await _customerBusiness.AddProduct(customerId, standId);
                if (result.Status == false)
                {
                    var customer = result.Data.FirstOrDefault() as CustomerDTO;
                    return Ok($"Customer {customer.Id} has these products list ${string.Join("\n,", customer.Products.Select(p=>p.Id))}");
                }
                else
                {
                    var error = result.ErrorList.FirstOrDefault();
                    return Problem(error.ErrorMessage, null, error.ErrorCode);
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }
        }
    }
}
