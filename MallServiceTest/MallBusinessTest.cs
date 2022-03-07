using Autofac.Extras.Moq;
using BusinessLogics.MallBusiness;
using DataAcess.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MallServiceTest
{

    public class MallBusinessTest
    {
        [Fact]
        public void GetMallOpenedStatus()
        {
            // Arrange
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
           .Setup(s => s.GetMallOpenedStatus())
           .Returns(mall);

            var sut = mock.Create<IMallBusiness>();
            var expected = mall;
            var actual = sut.GetMallOpenedStatus();

            // Asset
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.MallCapacity, actual.MallCapacity);
            Assert.True(expected.OpenedState == actual.OpenedState);
        }

        [Fact]
        public void GetMallOpenCloseDuration()
        {
            // Arrange
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
           .Setup(s => s.GetMallOpenCloseDuration())
           .Returns(mall);

            var sut = mock.Create<IMallBusiness>();
            var expected = mall;
            var actual = sut.GetMallOpenCloseDuration();

            // Asset
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.MallCapacity, actual.MallCapacity);
            Assert.True(expected.OpenedState == actual.OpenedState);
        }
    }
}
