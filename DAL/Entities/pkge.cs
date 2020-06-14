using DAL.Entities.pkgeStates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.Entities
{
    public class pkge
    {
        public IpkgeState status { get; set; }
        public pkge()
        {
            status = new SOnTheWay();
        }
        public int Number { get; set; }
        //public string Status { get; set; }
        public string Address { get; set; }

    }
}
