using jwt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.reposirory
{
    public interface IPortifolio
    {
        Portifolio Insert(Portifolio model);
        Portifolio Edit(Portifolio model);
        Portifolio remove(int Id);
        Portifolio GetPortifolioById(int Id);
       IEnumerable<Portifolio> GetAllPortifolio();
    }
}
