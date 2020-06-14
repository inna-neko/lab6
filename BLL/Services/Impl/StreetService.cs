using Catalog.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.Entities;
using Catalog.BLL.DTO;
using Catalog.DAL.Repositories.Interfaces;
using AutoMapper;
using Catalog.DAL.UnitOfWork;
using OSBB.Security;
using OSBB.Security.Identity;

namespace Catalog.BLL.Services.Impl
{
    public class StreetService
        : IStreetService
    {
        private readonly IUnitOfWork _database;
        private int pageSize = 10;

        public StreetService( 
            IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(
                    nameof(unitOfWork));
            }
            _database = unitOfWork;
        }

        /// <exception cref="MethodAccessException"></exception>
        public IEnumerable<StreetDTO> GetStreets(int pageNumber)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Director)
                && userType != typeof(Accountant))
            {
                throw new MethodAccessException();
            }
            var osbbId = user.OSBBID;
            var streetsEntities = 
                _database
                    .Streets
                    .Find(z => z.OSBBID == osbbId, pageNumber, pageSize);
            var mapper = 
                new MapperConfiguration(
                    cfg => cfg.CreateMap<postoffice, StreetDTO>()
                    ).CreateMapper();
            var streetsDto = 
                mapper
                    .Map<IEnumerable<postoffice>, List<StreetDTO>>(
                        streetsEntities);
            return streetsDto;
        }

        public void AddStreet(StreetDTO street)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Director)
                || userType != typeof(Accountant))
            {
                throw new MethodAccessException();
            }
            if (street == null)
            {
                throw new ArgumentNullException(nameof(street));
            }

            validate(street);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StreetDTO, postoffice>()).CreateMapper();
            var streetEntity = mapper.Map<StreetDTO, postoffice>(street);
            _database.Streets.Create(streetEntity);
        }

        private void validate(StreetDTO street)
        {
            if (string.IsNullOrEmpty(street.Name))
            {
                throw new ArgumentException("Name повинне містити значення!");
            }
        }
    }
}
