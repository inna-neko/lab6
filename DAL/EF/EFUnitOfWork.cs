using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Impl;
using Catalog.DAL.Repositories.Interfaces;
using Catalog.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;

namespace Catalog.DAL.EF
{
    public class EFUnitOfWork
        : IUnitOfWork
    {
        private OSBBContext db;
        private pkgeRepository osbbRepository;
        private postofficeRepository streetRepository;

        public EFUnitOfWork(OSBBContext context)
        {
            db = context;
        }
        public IpkgeRepository OSBBs
        {
            get
            {
                if (osbbRepository == null)
                    osbbRepository = new pkgeRepository(db);
                return osbbRepository;
            }
        }

        public IpostofficeRepository Streets
        {
            get
            {
                if (streetRepository == null)
                    streetRepository = new postofficeRepository(db);
                return streetRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
