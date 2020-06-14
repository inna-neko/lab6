using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.Entities;
using DAL.Entities;

namespace DAL.Entities.pkgeStates
{
    public interface IpkgeState
    {
        void nextState(pkge pkge);
        void prevState(pkge pkge);
    }
}
