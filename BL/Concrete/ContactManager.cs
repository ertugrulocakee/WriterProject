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
    public class ContactManager : IContactService
    {

        IContactDAL _contactDAL;

        public ContactManager(IContactDAL contactDAL)
        {
            _contactDAL = contactDAL;
        }

        public void TAdd(Contact t)
        {
            _contactDAL.Add(t);
        }

        public void TDelete(Contact t)
        {
            _contactDAL.Delete(t);
        }

        public List<Contact> TFilterList()
        {
            throw new NotImplementedException();
        }

        public Contact TGetByID(int id)
        {
            return _contactDAL.GetByID(id);
        }

        public List<Contact> TGetList()
        {
            return _contactDAL.List();
        }

        public void TUpdate(Contact t)
        {
            _contactDAL.Update(t);
        }
    }
}
