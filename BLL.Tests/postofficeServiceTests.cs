using Catalog.BLL.Services.Impl;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.EF;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using Catalog.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using CCL.Security.Identity;
using CCL.Security;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace BLL.Tests
{
    public class postofficeServiceTests
    {

        [Fact]
        public void Ctor_InputNull_ThrowArgumentNullException()
        {
            // Arrange
            IUnitOfWork nullUnitOfWork = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new postofficeService(nullUnitOfWork));
        }

        [Fact]
        public void Getpostoffices_UserIsAdmin_ThrowMethodAccessException()
        {
            // Arrange
            User user = new Admin(1, "test", 1);
            SecurityContext.SetUser(user);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            IpostofficeService postofficeService = new postofficeService(mockUnitOfWork.Object);

            // Act
            // Assert
            Assert.Throws<MethodAccessException>(() => postofficeService.Getpostoffices(0));
        }

        [Fact]
        public void Getpostoffices_postofficeFromDAL_CorrectMappingTopostofficeDTO()
        {
            // Arrange
            User user = new Admin(1, "test", 1);
            SecurityContext.SetUser(user);
            var postofficeService = GetpostofficeService();

            // Act
            var actualpostofficeDto = postofficeService.Getpostoffices(0).First();

            // Assert
            Assert.True(
                actualpostofficeDto.id == 1
                && actualpostofficeDto.Name == "testN"
                && actualpostofficeDto.Address == "testD"
                );
        }

        IpostofficeService GetpostofficeService()
        {
            var mockContext = new Mock<IUnitOfWork>();
            var expectedpostoffice = new postoffice() { id = 1, Name = "testN", Address = "testD"};
            var mockDbSet = new Mock<IpostofficeRepository>();
            mockDbSet.Setup(z => 
                z.Find(
                    It.IsAny<Func<postoffice,bool>>(), 
                    It.IsAny<int>(), 
                    It.IsAny<int>()))
                  .Returns(
                    new List<postoffice>() { expectedpostoffice }
                    );
            mockContext
                .Setup(context =>
                    context.postoffices)
                .Returns(mockDbSet.Object);

            IpostofficeService postofficeService = new postofficeService(mockContext.Object);

            return postofficeService;
        }
    }
}
