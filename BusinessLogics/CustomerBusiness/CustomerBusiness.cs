using AutoMapper;
using BusinessLogics.DTO;
using BusinessLogics.Extensions;
using BusinessLogics.MallBusiness;
using BusinessLogics.StandBusiness;
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
        private readonly IMapper _mapper;
        private readonly IStandBusiness _standBusiness;
        private readonly IMallBusiness _mallBusiness;

        public bool ConfigValue { get; private set; }

        public CustomerBusiness(ICustomer customer, IDataServices dataServices, IMapper mapper, IStandBusiness standBusiness, IMallBusiness mallBusiness)
        {
            _customer = customer as Customer;
            _dataServices = dataServices;
            _mapper = mapper;
            _standBusiness = standBusiness;
            _mallBusiness = mallBusiness;
        }

        /// <summary>
        /// Adds a customer
        /// </summary>
        /// <param name="standID"></param>
        /// <param name="customerDTO"></param>
        /// <returns>A customer DTO</returns>
        public async Task<OperationalResult> AddCustomer(string standID, CustomerDTO customerDTO)
        {
            try
            {
                var result = new OperationalResult();

                if (_mallBusiness.GetMallOpenedStatus().OpenedState == DataAcess.Enums.States.Closed)
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 500, ErrorMessage = "Sorry the Mall is closed, no new customers are accepted. Kindly process only the in-mall users." });
                    return result;
                }

                // A customer is added to a queue on a stand
                var stand = await _standBusiness.GetStand(standID);
                if (stand == null)
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 404, ErrorMessage = "The stand specified could not be found" });
                    return result;
                }

                // A customer is created
                var customer = _mapper.Map<Customer>(customerDTO);

                // Check and add customer to queue
                if (!stand.CustomerQueue.Contains(customer.Id.ToString()))
                    stand.CustomerQueue.Add(customer.Id.ToString());

                // Update the stand the Customer joined
                var updatedStand = _mapper.Map<StandDTO>(stand);
                await _standBusiness.UpdateStandQueue(updatedStand);

                // Update the customer with his currently joined queue
                customer.CurrentStandJoined = stand.Id;
                customer.Products.Add(new Product { Id = stand.Product.Id.ToString(), DisplayName = stand.Product.DisplayName, Name = stand.Product.Name });
                customer.DoneShopping = false;
                await _dataServices.AddData(customer);

                // A customerDTO is created
                var returncustomerDTO = _mapper.Map<CustomerDTO>(customer);
                result.Data.Add(returncustomerDTO);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Returns a customer DTO</returns>
        public async Task<OperationalResult> GetCustomer(string customerId)
        {
            try
            {
                var result = new OperationalResult();

                // first get the customer
                var customer = await _dataServices.GetDataByID<Customer>(customerId);
                if (customer == null)
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 404, ErrorMessage = "The Customer specified could not be found" });
                    return result;
                }

                // A customerDTO is created
                var returncustomerDTO = _mapper.Map<CustomerDTO>(customer);
                result.Data.Add(returncustomerDTO);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a list of customers
        /// </summary>
        /// <returns>Returns List<customer> CustomersDTO</customer></returns>
        public async Task<OperationalResult> GetCustomers()
        {
            try
            {
                var result = new OperationalResult();
                var customers = await _dataServices.GetAllData<Customer>();

                // A customerDTO is created
                var returncustomersDTO = _mapper.Map<List<CustomerDTO>>(customers);
                result.Data.Add(returncustomersDTO);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get customers by list of Ids
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns>Returns List of customer DTOs</returns>
        public async Task<OperationalResult> GetCustomers(List<string> Ids)
        {
            try
            {
                var result = new OperationalResult();

                List<CustomerDTO> customers = new List<CustomerDTO>();
                foreach (var Id in Ids)
                {
                    var customer = await GetCustomer(Id);
                    if (customer.Data.Count != 0)
                        customers.Add(customer.Data.FirstOrDefault() as CustomerDTO);
                }

                result.Data.Add(customers);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// handles the product purchases
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Returns the customerDTO</returns>
        public async Task<OperationalResult> BuyProduct(string customerId)
        {
            try
            {
                var result = new OperationalResult();

                // first get the customer
                var customer = await _dataServices.GetDataByID<Customer>(customerId);
                var stands = await _standBusiness.GetStands();

                if (customer == null)
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 404, ErrorMessage = "The Customer specified could not be found" });
                    return result;
                }

                var customerProductIds = customer.Products.Select(c => c.Id).ToList();
                if (!customerProductIds.Any(c => stands.Select(s => s.Product.Id).Any(s => s == c)))
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 500, ErrorMessage = "The Customer has some products which has their stands removed. The purchase cannot continue now." });
                    return result;
                }

                if (customer.Products.Count == 0)
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 500, ErrorMessage = "The Customer specified has no products to purchase." });
                    return result;
                }

                if (customer.DoneShopping)
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 500, ErrorMessage = "Sorry this Customer already left completed purchases and left the Mall." });
                    return result;
                }

                // update the customers current stand and save the transaction info
                customer.CurrentStandJoined = string.Empty;
                customer.DoneShopping = true;

                var transactionData = new Transactions { CustomerId = customer.Id, BoughtProducts = customer.Products };
                await _dataServices.UpSertData(customer.Id, customer);
                await _dataServices.UpSertData(customer.Id, transactionData);

                // A customerDTO is created
                var returncustomerDTO = _mapper.Map<CustomerDTO>(customer);
                result.Data.Add(returncustomerDTO);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Handles the product addition operation
        ///
        /// We assume that a
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="standDTO">A valid stand Id must be provided. That is an assumption</param>
        /// <returns>Returns the customerDTO</returns>
        public async Task<OperationalResult> AddProduct(string customerId, string standId)
        {
            try
            {
                var result = new OperationalResult();

                // first get the customer
                var customer = await _dataServices.GetDataByID<Customer>(customerId);
                if (customer == null)
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 404, ErrorMessage = "The Customer specified could not be found" });
                    return result;
                }

                // first get the customer
                var stand = await _dataServices.GetDataByID<Stand>(standId);
                if (stand == null)
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 404, ErrorMessage = "The Stand specified could not be found" });
                    return result;
                }
                else if (stand.OutOfProducts)
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 500, ErrorMessage = "This Stand is out of products. Kindly move to another Stand" });
                    return result;
                }

                var customerTransaction = await _dataServices.GetDataByID<Transactions>(customerId);
                if (customerTransaction != null && customerTransaction.BoughtProducts.Any(p => p.Id == stand.Product.Id))
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 500, ErrorMessage = "The product has already been bought, You cannot purchase from this stand again." });
                    return result;
                }

                if (customer.Products.Any(p => p.Id == stand.Product.Id))
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 500, ErrorMessage = "The product is already in the list of Items to buy." });
                    return result;
                }
                else if (customer.DoneShopping)
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 500, ErrorMessage = "Sorry this Customer already left completed purchases and left the Mall" });
                    return result;
                }

                // update the customers current stand and save the transaction info
                customer.CurrentStandJoined = stand.Id;
                customer.Products.Add(stand.Product);
                await _dataServices.UpSertData(customer.Id, customer);

                // A customerDTO is created
                var returncustomerDTO = _mapper.Map<CustomerDTO>(customer);
                result.Data.Add(returncustomerDTO);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Handles the customer update operation
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="stand"></param>
        /// <returns></returns>
        public async Task<OperationalResult> UpdateCustomer(CustomerDTO customerDTO)
        {
            try
            {
                var result = new OperationalResult();

                // A customer is added to a queue on a stand
                var customerResult = await GetCustomer(customerDTO.Id);
                var customerResultDTO = customerResult.Data.FirstOrDefault() as CustomerDTO;
                if (customerResultDTO == null)
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 404, ErrorMessage = "The Customer specified could not be found" });
                    return result;
                }

                // A customer is created
                var customer = _mapper.Map<Customer>(customerResultDTO);
                await _dataServices.UpSertData(customer.Id, customer);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Handles the update of a list of Customers
        /// </summary>
        /// <param name="customerDTOs"></param>
        /// <returns></returns>
        public async Task<OperationalResult> UpdateCustomers(List<CustomerDTO> customerDTOs)
        {
            try
            {
                var result = new OperationalResult();
                foreach (var customerDTO in customerDTOs)
                {
                    // A customer is added to a queue on a stand
                    var customerResult = await GetCustomer(customerDTO.Id);
                    var customerResultDTO = customerResult.Data.FirstOrDefault() as CustomerDTO;
                    if (customerResultDTO == null)
                    {
                        result.Status = true;
                        result.ErrorList.Add(new Error { ErrorCode = 404, ErrorMessage = $"The Customer {customerDTO.Id} specified could not be found" });
                    }
                    else
                    {
                        // A customer is created
                        var customer = _mapper.Map<Customer>(customerResultDTO);
                        await _dataServices.UpSertData(customer.Id, customer);
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Handles the customer deletion operation
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OperationalResult> DeleteCustomer(string Id)
        {
            try
            {
                var result = new OperationalResult();

                // A customer is added to a queue on a stand
                var customerResult = await GetCustomer(Id);
                var customerResultDTO = customerResult.Data.FirstOrDefault() as CustomerDTO;
                if (customerResultDTO == null)
                {
                    result.Status = true;
                    result.ErrorList.Add(new Error { ErrorCode = 404, ErrorMessage = "The Customer specified could not be found" });
                    return result;
                }

                await _dataServices.DeleteData<Customer>(customerResultDTO.Id); ;

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteCustomers(List<string> Ids)
        {
            try
            {
                await _dataServices.DeleteDataBulk<Customer>(Ids);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}