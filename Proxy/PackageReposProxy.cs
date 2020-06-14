using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using Catalog.DAL.Repositories.Impl;
using Catalog.DAL.EF;

namespace Proxy
{
    public class PackageReposProxy : IpkgeRepository
    {
        public pkgeRepository repos;
        public void Request()
        {
            if(repos == null)
            {
                repos = new pkgeRepository();
            }
        }

        void IRepository<pkge>.Create(pkge item)
        {
            throw new NotImplementedException();
        }

        void IRepository<pkge>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<pkge> IRepository<pkge>.Find(Func<pkge, bool> predicate, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        pkge IRepository<pkge>.Get(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<pkge> IRepository<pkge>.GetAll()
        {
            throw new NotImplementedException();
        }

        void IRepository<pkge>.Update(pkge item)
        {
            throw new NotImplementedException();
        }
    }
}
