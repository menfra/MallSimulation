using Autofac.Extras.Moq;
using BusinessLogics.CustomerBusiness;
using BusinessLogics.DTO;
using BusinessLogics.MallBusiness;
using BusinessLogics.StandBusiness;
using DataAcess.DataModels;
using DataAcess.DataServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using Moq;

namespace MallServiceTest
{
    public class CustomerBusinessTest
    {
        [Fact]
        public async Task AddCustomer_ShouldAddValidCustomer()
        {
            // Arrange
            Stand stand = new Stand
            {
                Id = "S1",
                Name = "Vegetables",
                DisplayName = "Vegetables",
                Duration = 10,
                OutOfProducts = false,
                CustomerQueue = new List<string> { "C1" },
                Product = new Product { Id = "P1", Name = "Coke", DisplayName = "Coke" }

            };
            StandDTO standDTO = new StandDTO
            {
                Id = "S1",
                Name = "Vegetables",
                DisplayName = "Vegetables",
                Duration = 10,
                OutOfProducts = false,
                CustomerQueue = new List<string> { "C1" },
                Product = new ProductDTO { Id = "P1", Name = "Coke", DisplayName = "Coke" }

            };
            CustomerDTO customerDTO = new CustomerDTO
            {
                Id = "C2",
                Name = "Frank",
                DisplayName = "Frank",
                CurrentStandJoined = "S1",
                DoneShopping = false,
                Products = new List<ProductDTO>
                {
                    { new ProductDTO{ Id = "P1", Name = "Coke", DisplayName = "Coke"} }
                }

            };
            Customer customer = new Customer
            {
                Id = "C1",
                Name = "Frank",
                DisplayName = "Frank",
                CurrentStandJoined = "S1",
                DoneShopping = false,
                Products = new List<Product>
                {
                    { new Product{ Id = "P1", Name = "Coke", DisplayName = "Coke"} }
                }

            };
            Mall mall = new Mall
            {
                Id = "M1",
                Name = "Spintex Mall",
                DisplayName = "Spintex Mall",
                MallCapacity = DataAcess.Enums.Capacity.Limited,
                OpenClosedDuration = 10,
                OpenedState = DataAcess.Enums.States.Opened
            };



            // Act
            using var mock = AutoMock.GetLoose();

            mock.Mock<IMallBusiness>()
               .Setup(m => m.GetMallOpenedStatus())
               .Returns(mall);

            mock.Mock<IStandBusiness>()
               .Setup(m => m.GetStand(stand.Id))
               .Returns(GetResults(stand));

            mock.Mock<IMapper>()
            .Setup(m => m.Map<Customer>(customerDTO))
            .Returns(customer);

            mock.Mock<IMapper>()
           .Setup(m => m.Map<CustomerDTO>(customer))
           .Returns(customerDTO);

            mock.Mock<IMapper>()
            .Setup(m => m.Map<StandDTO>(stand))
            .Returns(standDTO);

            mock.Mock<IDataServices>()
               .Setup(s => s.AddData(customer))
               .Returns(GetResults(customer));

            var sut = mock.Create<CustomerBusiness>();
            var expected = await GetResults(customerDTO);
            var actual = await sut.AddCustomer(stand.Id, customerDTO);

            // Asset
            Assert.True(actual != null);
            Assert.Equal(expected.Status, actual.Status);
            Assert.Equal((expected.Data.FirstOrDefault() as CustomerDTO).Id, (actual.Data.FirstOrDefault() as CustomerDTO).Id);
        }

        [Fact]
        public async Task GetCustomer_ShouldGetValidCustomer()
        {
            // Arrange
            Stand stand = new Stand
            {
                Id = "S1",
                Name = "Vegetables",
                DisplayName = "Vegetables",
                Duration = 10,
                OutOfProducts = false,
                CustomerQueue = new List<string> { "C1" },
                Product = new Product { Id = "P1", Name = "Coke", DisplayName = "Coke" }

            };
            CustomerDTO customerDTO = new CustomerDTO
            {
                Id = "C1",
                Name = "Frank",
                DisplayName = "Frank",
                CurrentStandJoined = "S1",
                DoneShopping = false,
                Products = new List<ProductDTO>
                {
                    { new ProductDTO{ Id = "P1", Name = "Coke", DisplayName = "Coke"} }
                }

            };
            Customer customer = new Customer
            {
                Id = "C1",
                Name = "Frank",
                DisplayName = "Frank",
                CurrentStandJoined = "S1",
                DoneShopping = false,
                Products = new List<Product>
                {
                    { new Product{ Id = "P1", Name = "Coke", DisplayName = "Coke"} }
                }

            };
            Mall mall = new Mall
            {
                Id = "M1",
                Name = "Spintex Mall",
                DisplayName = "Spintex Mall",
                MallCapacity = DataAcess.Enums.Capacity.Limited,
                OpenClosedDuration = 10,
                OpenedState = DataAcess.Enums.States.Opened
            };



            // Act
            using var mock = AutoMock.GetLoose();

            mock.Mock<IDataServices>()
             .Setup(s => s.GetDataByID<Customer>(customer.Id))
             .Returns(GetResults(customer));

            mock.Mock<IMapper>()
           .Setup(m => m.Map<CustomerDTO>(customer))
           .Returns(customerDTO);

            var sut = mock.Create<CustomerBusiness>();
            var expected = await GetResults(customerDTO);
            var actual = await sut.GetCustomer(customer.Id);

            // Asset
            Assert.True(actual != null);
            Assert.Equal(expected.Status, actual.Status);
            Assert.Equal((expected.Data.FirstOrDefault() as CustomerDTO).Id, (actual.Data.FirstOrDefault() as CustomerDTO).Id);
        }

        [Fact]
        public async Task GetCustomer_ShouldCheckEmptyCustomerObject()
        { // Arrange
            CustomerDTO customerDTO = new CustomerDTO
            {
                Id = "C1",
                Name = "Frank",
                DisplayName = "Frank",
                CurrentStandJoined = "S1",
                DoneShopping = false,
                Products = new List<ProductDTO>
                {
                    { new ProductDTO{ Id = "P1", Name = "Coke", DisplayName = "Coke"} }
                }

            };
            Customer customer = null;

            // Act
            using var mock = AutoMock.GetLoose();

            mock.Mock<IDataServices>()
             .Setup(s => s.GetDataByID<Customer>("C1"))
             .Returns(GetResults(customer));


            var sut = mock.Create<CustomerBusiness>();
            var expected = await GetResults(customerDTO);
            var actual = await sut.GetCustomer("C1");

            // Asset
            Assert.NotNull(actual);
            Assert.True(actual.Status);
            Assert.NotSame(expected, actual);
        }

        private async Task<OperationalResult> GetResults(CustomerDTO customerDTO)
        {
            var result = new OperationalResult();
            await Task.Delay(1);
            result.Data.Add(customerDTO);

            return result;
        }

        private async Task<Stand> GetResults(Stand stand)
        {
            await Task.Delay(1);
            return stand;
        }

        private async Task<Customer> GetResults(Customer customer)
        {
            await Task.Delay(1);
            return customer;
        }
    }
}
