using BL.Abstract;
using DAL.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Concrete
{
    public class HeadingManager : IHeadingService
    {

        IHeadingDAL _headingDAL;

        public HeadingManager(IHeadingDAL headingDAL)
        {
            _headingDAL = headingDAL;
        }

    
        public List<Heading> GetListByWriter(int id)
        {
            return _headingDAL.FilterList(x => x.WriterID == id);
        }

        public void TAdd(Heading t)
        {
            _headingDAL.Add(t);
        }

        public void TDelete(Heading t)
        {
            _headingDAL.Delete(t);
        }

        public List<Heading> TFilterList()
        {
            throw new NotImplementedException();
        }

        public Heading TGetByID(int id)
        {
            return _headingDAL.GetByID(id); 
        }

        public List<Heading> TGetList()
        {
            return _headingDAL.List();
        }

        public void TUpdate(Heading t)
        {
            _headingDAL.Update(t);
        }
    }
}
