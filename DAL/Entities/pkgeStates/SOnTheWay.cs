using Catalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities.pkgeStates
{
    class SOnTheWay:IpkgeState
    {
        public void nextState(pkge pkge)
        {
            pkge.status = new SSortingCenter();  
        }

        public void prevState(pkge pkge)
        {
            Console.WriteLine("this is the first state!");
        }
    }
}
