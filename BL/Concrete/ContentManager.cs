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
    public class ContentManager : IContentService
    {

        IContentDAL _contentDAL;

        public ContentManager(IContentDAL contentDAL)
        {
            _contentDAL = contentDAL;
        }

        public void TAdd(Content t)
        {
            _contentDAL.Add(t); 
        }

        public void TDelete(Content t)
        {
            _contentDAL.Delete(t);
        }

        public List<Content> TFilterList()
        {
            throw new NotImplementedException();
        }

        public Content TGetByID(int id)
        {
            return _contentDAL.GetByID(id);
        }

        public List<Content> TGetList()
        {
            return _contentDAL.List();
        }

        public void TUpdate(Content t)
        {
            _contentDAL.Update(t);
        }
    }
}
