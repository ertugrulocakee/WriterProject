using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Abstract
{
    public interface IHeadingService : IGenericService<Heading>
    {
        List<Heading> GetListByWriter(int id);

        List<Heading> GetListByCategory(int id);
        
    }
}
