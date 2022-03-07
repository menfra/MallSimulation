using Autofac.Extras.Moq;
using AutoMapper;
using BusinessLogics.DTO;
using BusinessLogics.StandBusiness;
using DataAcess.DataModels;
using DataAcess.DataServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MallServiceTest
{
    public class StandBusinessTest
    {
        [Fact]
        public async Task AddStand()
        {
            // Arrange
            StandDTO standDTO = new StandDTO
            {
                Id = "S1",
                Name = "Vegetables",
                DisplayName = "Vegetables",
                Duration = 10,
                OutOfProducts = true,
                CustomerQueue = new List<string> { "C1" },
                Product = new ProductDTO { Id = "P1", Name = "Coke", DisplayName = "Coke" }

            };
            Stand stand = new Stand
            {
                Id = "S1",
                Name = "Vegetables",
                DisplayName = "Vegetables",
                Duration = 10,
                OutOfProducts = true,
                CustomerQueue = new List<string> { "C1" },
                Product = new Product{ Id = "P1", Name = "Coke", DisplayName = "Coke" }

            };

            // Act
            using var mock = AutoMock.GetLoose();

            mock.Mock<IDataServices>()
              .Setup(s => s.AddData(stand))
              .Returns(GetResults(stand));

            mock.Mock<IMapper>()
             .Setup(m => m.Map<Stand>(standDTO))
             .Returns(stand);

            var sut = mock.Create<StandBusiness>();
            var expected = await GetResults(stand);
            var actual = await sut.AddStand(standDTO);

            // Asset
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual.Id);
            Assert.True(expected.OutOfProducts == actual.OutOfProducts);

        }

        [Fact]
        public async Task GetStand()
        {
            // Arrange
            StandDTO standDTO = new StandDTO
            {
                Id = "S1",
                Name = "Vegetables",
                DisplayName = "Vegetables",
                Duration = 10,
                OutOfProducts = true,
                CustomerQueue = new List<string> { "C1" },
                Product = new ProductDTO { Id = "P1", Name = "Coke", DisplayName = "Coke" }

            };
            Stand stand = new Stand
            {
                Id = "S1",
                Name = "Vegetables",
                DisplayName = "Vegetables",
                Duration = 10,
                OutOfProducts = true,
                CustomerQueue = new List<string> { "C1" },
                Product = new Product { Id = "P1", Name = "Coke", DisplayName = "Coke" }

            };

            // Act
            using var mock = AutoMock.GetLoose();

            mock.Mock<IDataServices>()
               .Setup(s => s.GetAllData<Stand>())
               .Returns(GetStands());

            var sut = mock.Create<StandBusiness>();
            var expected = await GetStands();
            var actual = await sut.GetStands();

            // Asset
            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
        }

        private async Task<Stand> GetResults(Stand stand)
        {
            await Task.Delay(1);
            return stand;
        }

        private async Task<List<Stand>> GetStands()
        {
            await Task.Delay(1);
            List<Stand> stands = new List<Stand>();
            for(int i = 0; i<5; i++)
            {
                stands.Add( new Stand { Id = i.ToString() });
            }
            return stands;
        }
    }
}
