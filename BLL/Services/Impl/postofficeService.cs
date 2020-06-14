using Catalog.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.Entities;
using Catalog.BLL.DTO;
using Catalog.DAL.Repositories.Interfaces;
using AutoMapper;
using Catalog.DAL.UnitOfWork;
using CCL.Security;
using CCL.Security.Identity;

namespace Catalog.BLL.Services.Impl
{
    public class postofficeService
        : IpostofficeService
    {
        private readonly IUnitOfWork _database;
        private int pageSize = 10;

        public postofficeService( 
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
        public IEnumerable<postofficeDTO> Getpostoffices(int pageNumber)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin)
                && userType != typeof(Accountant))
            {
                throw new MethodAccessException();
            }
            var postofficeId = user.postofficeID;
            var postofficesEntities = 
                _database
                    .postoffices
                    .Find(z => z.id == postofficeId, pageNumber, pageSize);
            var mapper = 
                new MapperConfiguration(
                    cfg => cfg.CreateMap<postoffice, postofficeDTO>()
                    ).CreateMapper();
            var postofficesDto = 
                mapper
                    .Map<IEnumerable<postoffice>, List<postofficeDTO>>(
                        postofficesEntities);
            return postofficesDto;
        }

        public void Addpostoffice(postofficeDTO postoffice)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin)
                || userType != typeof(Accountant))
            {
                throw new MethodAccessException();
            }
            if (postoffice == null)
            {
                throw new ArgumentNullException(nameof(postoffice));
            }

            validate(postoffice);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<postofficeDTO, postoffice>()).CreateMapper();
            var postofficeEntity = mapper.Map<postofficeDTO, postoffice>(postoffice);
            _database.postoffices.Create(postofficeEntity);
        }

        private void validate(postofficeDTO postoffice)
        {
            if (string.IsNullOrEmpty(postoffice.Name))
            {
                throw new ArgumentException("Name повинне містити значення!");
            }
        }
    }
}
