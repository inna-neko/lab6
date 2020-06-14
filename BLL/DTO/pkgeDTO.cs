using DAL.Entities.pkgeStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.BLL.DTO
{
    public class pkgeDTO
    {
        public int Number { get; set; }
        public IpkgeState status { get; set; }
        public string Address { get; set; }
    }
}
