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
    public class postofficeRepositoryInMemoryDBTests
    {
        public postContext Context => SqlLiteInMemoryContext();

        private postContext SqlLiteInMemoryContext()
        {

            var options = new DbContextOptionsBuilder<postContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new postContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public void Create_InputpostofficeWithId0_SetpostofficeId1()
        {
            // Arrange
            int expectedListCount = 1;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            IpostofficeRepository repository = uow.postoffices;

            postoffice postoffice = new postoffice()
            {
                id = 5,
                Name = "test",
                Address = "testD"
            };

            //Act
            repository.Create(postoffice);
            uow.Save();
            var factListCount = context.postoffices.Count();

            // Assert
            Assert.Equal(expectedListCount, factListCount);
        }

        [Fact]
        public void Delete_InputExistpostofficeId_Removed()
        {
            // Arrange
            int expectedListCount = 0;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            IpostofficeRepository repository = uow.postoffices;
            postoffice postoffice = new postoffice()
            {
                //Id = 1,
                id = 5,
                Name = "test",
                Address = "testD"
            };
            context.postoffices.Add(postoffice);
            context.SaveChanges();

            //Act
            repository.Delete(postoffice.id);
            uow.Save();
            var factpostofficeCount = context.postoffices.Count();

            // Assert
            Assert.Equal(expectedListCount, factpostofficeCount);
        }

        [Fact]
        public void Get_InputExistpostofficeId_Returnpostoffice()
        {
            // Arrange
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            IpostofficeRepository repository = uow.postoffices;
            postoffice expectedpostoffice = new postoffice()
            {
                //Id = 1,
                id = 5,
                Name = "test",
                Address = "testD"
            };
            context.postoffices.Add(expectedpostoffice);
            context.SaveChanges();

            //Act
            var factpostoffice = repository.Get(expectedpostoffice.id);

            // Assert
            Assert.Equal(expectedpostoffice, factpostoffice);
        }
    }
}
