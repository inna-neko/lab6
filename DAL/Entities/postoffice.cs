using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.Entities
{
    public class postoffice
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public List<worker> workers { get; set; }
    }
}
