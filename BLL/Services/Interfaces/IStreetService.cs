using Catalog.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.BLL.Services.Interfaces
{
    public interface IStreetService
    {
        IEnumerable<StreetDTO> GetStreets(int page);
    }
}
