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
    public class AboutManager : IAboutService
    {

        IAboutDAL _aboutDAL;

        public AboutManager(IAboutDAL aboutDAL)
        {
            _aboutDAL = aboutDAL;
        }

        public void TAdd(About t)
        {

            _aboutDAL.Add(t);
            
        }

        public void TDelete(About t)
        {
            _aboutDAL.Delete(t);
        }

        public List<About> TFilterList()
        {
            throw new NotImplementedException();
        }

        public About TGetByID(int id)
        {
            return _aboutDAL.GetByID(id);   
        }

        public List<About> TGetList()
        {
            return _aboutDAL.List();
        }

        public void TUpdate(About t)
        {
            _aboutDAL.Update(t);
        }
    }
}
