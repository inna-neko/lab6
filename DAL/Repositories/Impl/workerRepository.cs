using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.EF;
using System.Linq;
using DAL.Entities;

namespace Catalog.DAL.Repositories.Impl
{
    public class workerRepository
        : BaseRepository<worker>, IworkerRepository
    {
        internal workerRepository(postContext context)
            : base(context)
        {
        }
    }
}

