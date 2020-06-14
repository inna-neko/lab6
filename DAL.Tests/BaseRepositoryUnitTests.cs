using System;
using Xunit;
using Catalog.DAL.Repositories.Impl;
using Catalog.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using System.Linq;
using Moq;

namespace DAL.Tests
{
    class TestpostoffficeRepository
        : BaseRepository<postoffice>
    {
        public TestpostoffficeRepository(DbContext context) 
            : base(context)
        {
        }
    }

    public class BaseRepositoryUnitTests
    {

        [Fact]
        public void Create_InputpostoffficeInstance_CalledAddMethodOfDBSetWithpostoffficeInstance()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<postContext>()
                .Options;
            var mockContext = new Mock<postContext>(opt);
            var mockDbSet = new Mock<DbSet<postoffice>>();
            mockContext
                .Setup(context => 
                    context.Set<postoffice>(
                        ))
                .Returns(mockDbSet.Object);
            //EFUnitOfWork uow = new EFUnitOfWork(mockContext.Object);
            var repository = new TestpostoffficeRepository(mockContext.Object);

            postoffice expectedpostofffice = new Mock<postoffice>().Object;

            //Act
            repository.Create(expectedpostofffice);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Add(
                    expectedpostofffice
                    ), Times.Once());
        }

        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<postContext>()
                .Options;
            var mockContext = new Mock<postContext>(opt);
            var mockDbSet = new Mock<DbSet<postoffice>>();
            mockContext
                .Setup(context =>
                    context.Set<postoffice>(
                        ))
                .Returns(mockDbSet.Object);
            //EFUnitOfWork uow = new EFUnitOfWork(mockContext.Object);
            //IpostoffficeRepository repository = uow.postofffices;
            var repository = new TestpostoffficeRepository(mockContext.Object);

            postoffice expectedpostofffice = new postoffice() { id = 1};
            mockDbSet.Setup(mock => mock.Find(expectedpostofffice.id)).Returns(expectedpostofffice);

            //Act
            repository.Delete(expectedpostofffice.id);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedpostofffice.id
                    ), Times.Once());
            mockDbSet.Verify(
                dbSet => dbSet.Remove(
                    expectedpostofffice
                    ), Times.Once());
        }

        [Fact]
        public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<postContext>()
                .Options;
            var mockContext = new Mock<postContext>(opt);
            var mockDbSet = new Mock<DbSet<postoffice>>();
            mockContext
                .Setup(context =>
                    context.Set<postoffice>(
                        ))
                .Returns(mockDbSet.Object);

            postoffice expectedpostofffice = new postoffice() { id = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedpostofffice.id))
                    .Returns(expectedpostofffice);
            var repository = new TestpostoffficeRepository(mockContext.Object);

            //Act
            var actualpostofffice = repository.Get(expectedpostofffice.id);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedpostofffice.id
                    ), Times.Once());
            Assert.Equal(expectedpostofffice, actualpostofffice);
        }

      
    }
}
