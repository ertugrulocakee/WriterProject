using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Abstract
{
    public interface IContentService : IGenericService<Content>
    {

        List<Content> GetListByID(int id);

    }
}
