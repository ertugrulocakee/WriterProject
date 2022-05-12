using DAL.Abstract;
using DAL.Repository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class EFHeadingDAL : GenericRepository<Heading>, IHeadingDAL
    {


    }
}
