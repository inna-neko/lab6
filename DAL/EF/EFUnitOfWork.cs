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
        private postContext db;
        private pkgeRepository pkgeRepository;
        private postofficeRepository postofficeRepository;
        private workerRepository workerRepository;

        public EFUnitOfWork(postContext context)
        {
            db = context;
        }
        public IpkgeRepository pkges
        {
            get
            {
                if (pkgeRepository == null)
                    pkgeRepository = new pkgeRepository(db);
                return pkgeRepository;
            }
        }

        public IpostofficeRepository postoffices
        {
            get
            {
                if (postofficeRepository == null)
                    postofficeRepository = new postofficeRepository(db);
                return postofficeRepository;
            }
        }

        //public IpkgeRepository pkges => throw new NotImplementedException();

        //public IpostofficeRepository postoffices => throw new NotImplementedException();

        public IworkerRepository workers
        {
            get
            {
                if (workerRepository == null)
                    workerRepository = new workerRepository(db);
                return workerRepository;
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
