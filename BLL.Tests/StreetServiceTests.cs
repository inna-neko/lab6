using Catalog.BLL.Services.Impl;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.EF;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using Catalog.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using OSBB.Security;
using OSBB.Security.Identity;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace BLL.Tests
{
    public class StreetServiceTests
    {

        [Fact]
        public void Ctor_InputNull_ThrowArgumentNullException()
        {
            // Arrange
            IUnitOfWork nullUnitOfWork = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new StreetService(nullUnitOfWork));
        }

        [Fact]
        public void GetStreets_UserIsAdmin_ThrowMethodAccessException()
        {
            // Arrange
            User user = new Admin(1, "test", 1);
            SecurityContext.SetUser(user);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            IStreetService streetService = new StreetService(mockUnitOfWork.Object);

            // Act
            // Assert
            Assert.Throws<MethodAccessException>(() => streetService.GetStreets(0));
        }

        [Fact]
        public void GetStreets_StreetFromDAL_CorrectMappingToStreetDTO()
        {
            // Arrange
            User user = new Director(1, "test", 1);
            SecurityContext.SetUser(user);
            var streetService = GetStreetService();

            // Act
            var actualStreetDto = streetService.GetStreets(0).First();

            // Assert
            Assert.True(
                actualStreetDto.StreetId == 1
                && actualStreetDto.Name == "testN"
                && actualStreetDto.Description == "testD"
                );
        }

        IStreetService GetStreetService()
        {
            var mockContext = new Mock<IUnitOfWork>();
            var expectedStreet = new postoffice() { StreetId = 1, Name = "testN", Description = "testD", OSBBID = 2};
            var mockDbSet = new Mock<IpostofficeRepository>();
            mockDbSet.Setup(z => 
                z.Find(
                    It.IsAny<Func<postoffice,bool>>(), 
                    It.IsAny<int>(), 
                    It.IsAny<int>()))
                  .Returns(
                    new List<postoffice>() { expectedStreet }
                    );
            mockContext
                .Setup(context =>
                    context.Streets)
                .Returns(mockDbSet.Object);

            IStreetService streetService = new StreetService(mockContext.Object);

            return streetService;
        }
    }
}
