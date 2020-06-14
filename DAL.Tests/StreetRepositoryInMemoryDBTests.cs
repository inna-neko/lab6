using System;
using Xunit;
using Catalog.DAL.Repositories.Impl;
using Catalog.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using System.Linq;

namespace DAL.Tests
{
    public class StreetRepositoryInMemoryDBTests
    {
        public OSBBContext Context => SqlLiteInMemoryContext();

        private OSBBContext SqlLiteInMemoryContext()
        {

            var options = new DbContextOptionsBuilder<OSBBContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new OSBBContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public void Create_InputStreetWithId0_SetStreetId1()
        {
            // Arrange
            int expectedListCount = 1;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            IpostofficeRepository repository = uow.Streets;

            postoffice street = new postoffice()
            {
                OSBBID = 5,
                Name = "test",
                Description = "testD",
                OSBB = new OSBB() { OSBBID = 5}
            };

            //Act
            repository.Create(street);
            uow.Save();
            var factListCount = context.Streets.Count();

            // Assert
            Assert.Equal(expectedListCount, factListCount);
        }

        [Fact]
        public void Delete_InputExistStreetId_Removed()
        {
            // Arrange
            int expectedListCount = 0;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            IpostofficeRepository repository = uow.Streets;
            postoffice street = new postoffice()
            {
                //StreetId = 1,
                OSBBID = 5,
                Name = "test",
                Description = "testD",
                OSBB = new OSBB() { OSBBID = 5 }
            };
            context.Streets.Add(street);
            context.SaveChanges();

            //Act
            repository.Delete(street.StreetId);
            uow.Save();
            var factStreetCount = context.Streets.Count();

            // Assert
            Assert.Equal(expectedListCount, factStreetCount);
        }

        [Fact]
        public void Get_InputExistStreetId_ReturnStreet()
        {
            // Arrange
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            IpostofficeRepository repository = uow.Streets;
            postoffice expectedStreet = new postoffice()
            {
                //StreetId = 1,
                OSBBID = 5,
                Name = "test",
                Description = "testD",
                OSBB = new OSBB() { OSBBID = 5 }
            };
            context.Streets.Add(expectedStreet);
            context.SaveChanges();

            //Act
            var factStreet = repository.Get(expectedStreet.StreetId);

            // Assert
            Assert.Equal(expectedStreet, factStreet);
        }
    }
}
