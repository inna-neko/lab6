using Catalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities.pkgeStates
{
    public class SSortingCenter : IpkgeState
    {
        public void nextState(pkge pkge)
        {
            pkge.status = new SCompleated();
        }

        public void prevState(pkge pkge)
        {
            pkge.status = new SOnTheWay();
        }
    }
}
