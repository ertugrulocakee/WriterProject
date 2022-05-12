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
    public class WriterManager : IWriterService
    {

        IWriterDAL _writerDAL;

        public void TAdd(Writer t)
        {
            _writerDAL.Add(t);
        }

        public void TDelete(Writer t)
        {
            _writerDAL.Delete(t);
        }

        public List<Writer> TFilterList()
        {
            throw new NotImplementedException();
        }

        public Writer TGetByID(int id)
        {
            return _writerDAL.GetByID(id);  
        }

        public List<Writer> TGetList()
        {
            return _writerDAL.List();
        }

        public void TUpdate(Writer t)
        {
            _writerDAL.Update(t);
        }
    }
}
