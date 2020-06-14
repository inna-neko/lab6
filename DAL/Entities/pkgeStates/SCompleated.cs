using Catalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities.pkgeStates
{
    public class SCompleated : IpkgeState
    {
        public void nextState(pkge pkge)
        {
            Console.WriteLine("this is the final state!");
        }

        public void prevState(pkge pkge)
        {
            pkge.status = new SSortingCenter();
        }
    }
}
